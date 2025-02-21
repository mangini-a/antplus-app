#region Using directives

using ANT_Managed_Library;
using AntPlus.Profiles.FitnessEquipment;
using AntPlus.Profiles.HeartRate;
using AntPlus.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        private static readonly byte USER_ANT_FE_CHANNEL = 0;       // ANT Channel to use for FE
        private static readonly byte USER_ANT_HR_CHANNEL = 1;       // ANT Channel to use for HR
        private static readonly ushort USER_DEVICENUM = 0;          // Allow wildcard matching
        private static readonly byte USER_FE_DEVICETYPE = 0x11;     // Search for ANT+ Fitness Equipment
        private static readonly byte USER_HR_DEVICETYPE = 0x78;     // Search for ANT+ Heart Rate Monitor
        private static readonly byte USER_TRANSTYPE = 1;            // Transmission type

        private static readonly byte USER_NETWORK_NUM = 0;
        private static readonly byte[] USER_NETWORK_KEY = { 0xB9, 0xA5, 0x21, 0xFB, 0xBD, 0x72, 0xC3, 0x45 };

        private static readonly byte USER_RADIOFREQ = 0x39;         // RF Frequency + 2400 MHz

        private static ANT_Device device0;                          // ANT USB Stick (Hub)
        private static ANT_Channel channel0;                        // Hub (S) - Trainer (M) Link
        private static ANT_Channel channel1;                        // Hub (S) - HR Monitor (M) Link

        private static FitnessEquipmentDisplay fitnessEquipmentDisplay;
        private static HeartRateDisplay heartRateDisplay;
        private static Network networkAntPlus;

        private readonly Stopwatch stopwatch;
        private readonly DataManager dataManager;
        private Controller controller;

        private long startTimeTicks = 0;
        private bool firstEventReceived = false;
        private byte pulseSamples = 0;

        public Form1()
        {
            InitializeComponent();
            InitializeANT();
            ConfigureANT();

            // Set up a stopwatch to measure activity time
            stopwatch = new Stopwatch();

            // Create a data manager to store collected/computed data
            dataManager = new DataManager();
        }

        private void InitializeANT()
        {
            try
            {
                device0 = new ANT_Device();
                device0.deviceResponse += new ANT_Device.dDeviceResponseHandler(DeviceResponse);

                channel0 = device0.getChannel(USER_ANT_FE_CHANNEL);
                channel0.channelResponse += new dChannelResponseHandler(ChannelResponse);

                channel1 = device0.getChannel(USER_ANT_HR_CHANNEL);
                channel1.channelResponse += new dChannelResponseHandler(ChannelResponse);
            }
            catch (Exception ex)
            {
                if (device0 == null)    
                    throw new Exception("Could not connect to any device.\n" + "Details: \n   " + ex.Message);
                else
                    throw new Exception("Error connecting to ANT: " + ex.Message);
            }
        }

        private void ConfigureANT()
        {
            device0.ResetSystem();     // Soft reset
            Thread.Sleep(500);         // Delay 500ms after a reset

            if (!device0.setNetworkKey(USER_NETWORK_NUM, USER_NETWORK_KEY, 500))
                throw new Exception("Error configuring network key");

            if (!channel0.setChannelID(USER_DEVICENUM, false, USER_FE_DEVICETYPE, USER_TRANSTYPE, 500)) // Not using pairing bit
                throw new Exception("Error configuring Channel ID");

            if (!channel1.setChannelID(USER_DEVICENUM, false, USER_HR_DEVICETYPE, USER_TRANSTYPE, 500)) // Not using pairing bit
                throw new Exception("Error configuring Channel ID");

            networkAntPlus = new Network(USER_NETWORK_NUM, USER_NETWORK_KEY, USER_RADIOFREQ);
            fitnessEquipmentDisplay = new FitnessEquipmentDisplay(channel0, networkAntPlus);
            heartRateDisplay = new HeartRateDisplay(channel1, networkAntPlus);

            // Process instantaneous speed every time a General FE Data page is received
            fitnessEquipmentDisplay.GeneralFePageReceived += ProcessSpeedData;

            // Process instantaneous cadence and power every time a Specific Trainer Data page is received
            fitnessEquipmentDisplay.SpecificTrainerPageReceived += ProcessInstantaneousData;

            // Receiving HR data at a rate of 1 Hz is enough
            heartRateDisplay.ChannelParameters.ChannelPeriod = HeartRate.SlaveChannelPeriod.OneHz;

            // Process HR data every time it is received
            heartRateDisplay.HeartRateDataReceived += ProcessHeartRateData;

            // Begin searching for the trainer and the HR monitor
            fitnessEquipmentDisplay.TurnOn();
            heartRateDisplay.TurnOn();
        }

        /// <summary>
        /// Starts the data collection process and enables the PI controller.
        /// </summary>
        /// <param name="sender">System.Windows.Forms.Button instance</param>
        /// <param name="e">EventArgs object</param>
        private void StartButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = false;
            numericUpDown.Enabled = false;

            // Create a controller and assign it the setpoint selected through the UI
            controller = new Controller(Decimal.ToByte(numericUpDown.Value));

            // Start a new workout
            dataManager.ClearData();
            startTimeTicks = 0;
            firstEventReceived = false;
            pulseSamples = 0;

            stopwatch.Start();
            updateTimer.Start();
        }

        /// <summary>
        /// Displays the decoded heart rate data, stores it, and uses it as a process variable.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="counter"></param>
        private void ProcessHeartRateData(HeartRateData data, uint counter)
        {
            // Update the UI (asynchronously if needed)
            if (InvokeRequired)
                Invoke(new Action(() => heartRateLabel.Text = $"{data.HeartRate} bpm"));
            else
                heartRateLabel.Text = $"{data.HeartRate} bpm";

            // If training has been started, enable the controller and store the received data
            if (stopwatch.IsRunning)
            {
                HandleEvent("HR (bpm)", data.HeartRate);

                double computedResistance = controller.ComputeControlAction(data.HeartRate);
                computedResistance = FitToSaturationLimits(computedResistance);

                // Round the adapted output to the nearest integer value
                double roundedResistance = Math.Round(computedResistance);

                HandleEvent("Resistance (%)", roundedResistance);
                SetTrainerResistance(roundedResistance);

                pulseSamples++;

                // End the session once 100 heartbeat samples have been collected
                if (pulseSamples == 100)
                {
                    pulseSamples = 0;
                    WorkoutComplete();
                }
            }
        }

        /// <summary>
        /// Adapts the PI controller's output to saturation boundaries (if needed).
        /// </summary>
        /// <param name="computedResistance"></param>
        /// <returns></returns>
        private double FitToSaturationLimits(double computedResistance)
        {
            if (computedResistance < 0)                 // Lower boundary (0%) broken
                return 0;
            else if (computedResistance > 100)          // Upper boundary (100%) broken
                return 100;
            else                                        // Acceptable range (0-100%)
                return computedResistance;
        }

        /// <summary>
        /// Displays the instantaneous cadence and power recorded by the trainer and stores them.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="counter"></param>
        private void ProcessInstantaneousData(SpecificTrainerPage page, uint counter)
        {
            // Update the UI (asynchronously if needed)
            if (InvokeRequired)
            {
                Invoke(new Action(() => cadenceLabel.Text = $"{page.InstantaneousCadence} rpm"));
                Invoke(new Action(() => powerLabel.Text = $"{page.InstantaneousPower} W"));
            }
            else
            {
                cadenceLabel.Text = $"{page.InstantaneousCadence} rpm";
                powerLabel.Text = $"{page.InstantaneousPower} W";
            }

            // If training has been started, store the received data
            if (stopwatch.IsRunning)
            {
                HandleEvent("Cadence (rpm)", page.InstantaneousCadence);
                HandleEvent("Power (W)", page.InstantaneousPower);
            }
        }

        /// <summary>
        /// Displays the instantaneous speed recorded by the trainer and stores it.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="counter"></param>
        private void ProcessSpeedData(GeneralFePage page, uint counter)
        {
            // Convert the received instantaneous speed from m/s to km/h
            double speedKmph = (double)(page.Speed) / 1000.0 * 3.6;

            // Limit the speed value to a decimal place
            double roundedSpeed = Math.Round(speedKmph, 1);

            // Update the UI to show the result (asynchronously if needed)
            if (InvokeRequired)
                Invoke(new Action(() => speedLabel.Text = $"{roundedSpeed} km/h"));
            else
                speedLabel.Text = $"{roundedSpeed} km/h";

            // If training has been started, store the received data
            if (stopwatch.IsRunning)
                HandleEvent("Speed (km/h)", roundedSpeed);
        }

        /// <summary>
        /// Called by all the individual event handlers to properly store recorded data.
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="value"></param>
        private void HandleEvent(string sourceName, object value)
        {
            long timestamp;

            if (!firstEventReceived)
            {
                startTimeTicks = DateTime.Now.Ticks; // Capture the time of the very first event
                timestamp = 0;
                firstEventReceived = true;
            }
            else
            {
                timestamp = DateTime.Now.Ticks - startTimeTicks;
                timestamp /= TimeSpan.TicksPerMillisecond; // Convert to milliseconds
            }

            dataManager.AddData(sourceName, value, timestamp);
        }

        /// <summary>
        /// Called whenever a channel event is received.
        /// </summary>
        /// <param name="response"></param>
        static void ChannelResponse(ANT_Response response)
        {
            try
            {
                switch ((ANT_ReferenceLibrary.ANTMessageID)response.responseID)
                {
                    case ANT_ReferenceLibrary.ANTMessageID.RESPONSE_EVENT_0x40:
                        {
                            switch (response.getChannelEventCode())
                            {
                                // This event indicates that a message has just been
                                // sent over the air. We take advantage of this event to set
                                // up the data for the next message period.
                                case ANT_ReferenceLibrary.ANTEventID.EVENT_TX_0x03:
                                    {
                                        break;
                                    }
                                case ANT_ReferenceLibrary.ANTEventID.EVENT_RX_SEARCH_TIMEOUT_0x01:
                                    {
                                        Console.WriteLine("Search Timeout");
                                        break;
                                    }
                                case ANT_ReferenceLibrary.ANTEventID.EVENT_RX_FAIL_0x02:
                                    {
                                        Console.WriteLine("Rx Fail");
                                        break;
                                    }
                                case ANT_ReferenceLibrary.ANTEventID.EVENT_TRANSFER_RX_FAILED_0x04:
                                    {
                                        Console.WriteLine("Burst receive has failed");
                                        break;
                                    }
                                case ANT_ReferenceLibrary.ANTEventID.EVENT_TRANSFER_TX_COMPLETED_0x05:
                                    {
                                        Console.WriteLine("Transfer Completed");
                                        break;
                                    }
                                case ANT_ReferenceLibrary.ANTEventID.EVENT_TRANSFER_TX_FAILED_0x06:
                                    {
                                        Console.WriteLine("Transfer Failed");
                                        break;
                                    }
                                case ANT_ReferenceLibrary.ANTEventID.EVENT_CHANNEL_CLOSED_0x07:
                                    {
                                        // This event should be used to determine that the channel is closed.
                                        Console.WriteLine("Channel Closed");
                                        Console.WriteLine("Unassigning Channel...");
                                        if (channel0.unassignChannel(500))
                                        {
                                            Console.WriteLine("Unassigned Channel");
                                        }
                                        break;
                                    }
                                case ANT_ReferenceLibrary.ANTEventID.EVENT_RX_FAIL_GO_TO_SEARCH_0x08:
                                    {
                                        Console.WriteLine("Go to Search");
                                        break;
                                    }
                                case ANT_ReferenceLibrary.ANTEventID.EVENT_CHANNEL_COLLISION_0x09:
                                    {
                                        Console.WriteLine("Channel Collision");
                                        break;
                                    }
                                case ANT_ReferenceLibrary.ANTEventID.EVENT_TRANSFER_TX_START_0x0A:
                                    {
                                        Console.WriteLine("Burst Started");
                                        break;
                                    }
                                default:
                                    {
                                        Console.WriteLine("Unhandled Channel Event " + response.getChannelEventCode());
                                        break;
                                    }
                            }
                            break;
                        }
                    case ANT_ReferenceLibrary.ANTMessageID.BROADCAST_DATA_0x4E:
                    case ANT_ReferenceLibrary.ANTMessageID.ACKNOWLEDGED_DATA_0x4F:
                    case ANT_ReferenceLibrary.ANTMessageID.BURST_DATA_0x50:
                    case ANT_ReferenceLibrary.ANTMessageID.EXT_BROADCAST_DATA_0x5D:
                    case ANT_ReferenceLibrary.ANTMessageID.EXT_ACKNOWLEDGED_DATA_0x5E:
                    case ANT_ReferenceLibrary.ANTMessageID.EXT_BURST_DATA_0x5F:
                        {
                            // Process received messages here
                        }
                        break;
                    default:
                        {
                            Console.WriteLine("Unknown Message " + response.responseID);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Channel response processing failed with exception: " + ex.Message);
            }
        }

        /// <summary>
        /// Called whenever a message is received from ANT unless it is a channel event message.
        /// </summary>
        /// <param name="response"></param>
        static void DeviceResponse(ANT_Response response)
        {
            switch ((ANT_ReferenceLibrary.ANTMessageID)response.responseID)
            {
                case ANT_ReferenceLibrary.ANTMessageID.STARTUP_MESG_0x6F:
                    {
                        Console.Write("RESET Complete, reason: ");

                        byte ucReason = response.messageContents[0];

                        if (ucReason == (byte)ANT_ReferenceLibrary.StartupMessage.RESET_POR_0x00)
                            Console.WriteLine("RESET_POR");
                        if (ucReason == (byte)ANT_ReferenceLibrary.StartupMessage.RESET_RST_0x01)
                            Console.WriteLine("RESET_RST");
                        if (ucReason == (byte)ANT_ReferenceLibrary.StartupMessage.RESET_WDT_0x02)
                            Console.WriteLine("RESET_WDT");
                        if (ucReason == (byte)ANT_ReferenceLibrary.StartupMessage.RESET_CMD_0x20)
                            Console.WriteLine("RESET_CMD");
                        if (ucReason == (byte)ANT_ReferenceLibrary.StartupMessage.RESET_SYNC_0x40)
                            Console.WriteLine("RESET_SYNC");
                        if (ucReason == (byte)ANT_ReferenceLibrary.StartupMessage.RESET_SUSPEND_0x80)
                            Console.WriteLine("RESET_SUSPEND");
                        break;
                    }
                case ANT_ReferenceLibrary.ANTMessageID.VERSION_0x3E:
                    {
                        Console.WriteLine("VERSION: " + new ASCIIEncoding().GetString(response.messageContents));
                        break;
                    }
                case ANT_ReferenceLibrary.ANTMessageID.RESPONSE_EVENT_0x40:
                    {
                        switch (response.getMessageID())
                        {
                            case ANT_ReferenceLibrary.ANTMessageID.CLOSE_CHANNEL_0x4C:
                                {
                                    if (response.getChannelEventCode() == ANT_ReferenceLibrary.ANTEventID.CHANNEL_IN_WRONG_STATE_0x15)
                                    {
                                        Console.WriteLine("Channel is already closed");
                                        Console.WriteLine("Unassigning Channel...");
                                        if (channel0.unassignChannel(500))
                                        {
                                            Console.WriteLine("Unassigned Channel");
                                        }
                                    }
                                    break;
                                }
                            case ANT_ReferenceLibrary.ANTMessageID.NETWORK_KEY_0x46:
                            case ANT_ReferenceLibrary.ANTMessageID.ASSIGN_CHANNEL_0x42:
                            case ANT_ReferenceLibrary.ANTMessageID.CHANNEL_ID_0x51:
                            case ANT_ReferenceLibrary.ANTMessageID.CHANNEL_RADIO_FREQ_0x45:
                            case ANT_ReferenceLibrary.ANTMessageID.CHANNEL_MESG_PERIOD_0x43:
                            case ANT_ReferenceLibrary.ANTMessageID.OPEN_CHANNEL_0x4B:
                            case ANT_ReferenceLibrary.ANTMessageID.UNASSIGN_CHANNEL_0x41:
                                {
                                    if (response.getChannelEventCode() != ANT_ReferenceLibrary.ANTEventID.RESPONSE_NO_ERROR_0x00)
                                    {
                                        Console.WriteLine(String.Format("Error {0} configuring {1}", response.getChannelEventCode(), response.getMessageID()));
                                    }
                                    break;
                                }
                            case ANT_ReferenceLibrary.ANTMessageID.RX_EXT_MESGS_ENABLE_0x66:
                                {
                                    if (response.getChannelEventCode() == ANT_ReferenceLibrary.ANTEventID.INVALID_MESSAGE_0x28)
                                    {
                                        Console.WriteLine("Extended messages not supported in this ANT product");
                                        break;
                                    }
                                    else if (response.getChannelEventCode() != ANT_ReferenceLibrary.ANTEventID.RESPONSE_NO_ERROR_0x00)
                                    {
                                        Console.WriteLine(String.Format("Error {0} configuring {1}", response.getChannelEventCode(), response.getMessageID()));
                                        break;
                                    }
                                    Console.WriteLine("Extended messages enabled");
                                    break;
                                }
                            case ANT_ReferenceLibrary.ANTMessageID.REQUEST_0x4D:
                                {
                                    if (response.getChannelEventCode() == ANT_ReferenceLibrary.ANTEventID.INVALID_MESSAGE_0x28)
                                    {
                                        Console.WriteLine("Requested message not supported in this ANT product");
                                        break;
                                    }
                                    break;
                                }
                            default:
                                {
                                    Console.WriteLine("Unhandled response " + response.getChannelEventCode() + " to message " + response.getMessageID()); break;
                                }
                        }
                        break;
                    }
            }
        }

        /// <summary>
        /// Updates the UI stopwatch with the current time.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            // Get the total elapsed time
            TimeSpan currentTime = stopwatch.Elapsed;

            // Update the UI with the current time
            timeLabel.Text = string.Format("{0:mm\\:ss}", currentTime);
        }

        /// <summary>
        /// Sets the fitness equipment's resistance by sending Data Page 48.
        /// </summary>
        /// <param name="resistance"></param>
        private void SetTrainerResistance(double roundedResistance)
        {
            ControlBasicResistancePage command = new ControlBasicResistancePage
            {
                TotalResistance = Convert.ToByte(roundedResistance * 2) // Units: 0.5%
            };
            fitnessEquipmentDisplay.SendBasicResistance(command);

            // Update the UI with the resistance % being sent to the trainer
            appliedResistanceLabel.Text = $"{roundedResistance} %";
        }

        /// <summary>
        /// Generates a CSV file with data characterizing the workout, to then close the app.
        /// </summary>
        private async void WorkoutComplete()
        {
            stopwatch.Stop();
            updateTimer.Stop();
            ShutDownCommunication();

            // Generate a CSV file with all values describing the activity
            dataManager.WriteToCsv("sensor-data.csv");

            // Show a message box asynchronously and wait for it to be closed
            await Task.Run(() => MessageBox.Show("Output file generated!"));

            // Close the form on the UI thread
            this.Invoke((Action)delegate { this.Close(); });
        }

        /// <summary>
        /// Terminates communication by closing the channels and resetting the device.
        /// </summary>
        private void ShutDownCommunication()
        {
            fitnessEquipmentDisplay.TurnOff();
            heartRateDisplay.TurnOff();
            ANT_Device.shutdownDeviceInstance(ref device0);
        }
    }
}
