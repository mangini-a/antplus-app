#region Using directives

using ANT_Managed_Library;
using AntPlus.Profiles.Common;
using AntPlus.Profiles.FitnessEquipment;
using AntPlus.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

#endregion

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        static readonly byte USER_ANT_CHANNEL = 0;      // ANT Channel to use
        static readonly ushort USER_DEVICENUM = 0;      // Allow wildcard matching
        static readonly byte USER_DEVICETYPE = 0x11;    // Search for ANT+ Fitness Equipment
        static readonly byte USER_TRANSTYPE = 1;        // Transmission type

        static readonly byte USER_NETWORK_NUM = 0;          
        static readonly byte[] USER_NETWORK_KEY = { 0xB9, 0xA5, 0x21, 0xFB, 0xBD, 0x72, 0xC3, 0x45 };

        static readonly byte USER_RADIOFREQ = 0x39;     // RF Frequency + 2400 MHz

        static ANT_Device device0;
        static ANT_Channel channel0;

        static FitnessEquipmentDisplay fitnessEquipmentDisplay;
        static Network networkAntPlus;

        private Dictionary<TimeSpan, double> resistanceSchedule;
        private readonly System.Diagnostics.Stopwatch stopwatch;
        private double lastSentResistance = -1;
        private TimeSpan maxDuration;

        private SensorData sensorData;

        public Form1()
        {
            InitializeComponent();
            InitializeANT();
            ConfigureANT();

            // Set up the stopwatch
            stopwatch = new System.Diagnostics.Stopwatch();

            // Load the resistance schedule
            string filePath = "resistance-schedule.csv";
            LoadResistanceSchedule(filePath);
        }

        private void InitializeANT()
        {
            try
            {
                device0 = new ANT_Device();
                device0.deviceResponse += new ANT_Device.dDeviceResponseHandler(DeviceResponse);

                channel0 = device0.getChannel(USER_ANT_CHANNEL);
                channel0.channelResponse += new dChannelResponseHandler(ChannelResponse);
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

            if (!channel0.setChannelID(USER_DEVICENUM, false, USER_DEVICETYPE, USER_TRANSTYPE, 500))  // Not using pairing bit
                throw new Exception("Error configuring Channel ID");

            networkAntPlus = new Network(USER_NETWORK_NUM, USER_NETWORK_KEY, USER_RADIOFREQ);
            fitnessEquipmentDisplay = new FitnessEquipmentDisplay(channel0 , networkAntPlus);

            // Begin searching for the trainer
            fitnessEquipmentDisplay.TurnOn();

            // Process instantaneous speed every time a General FE Data page is received
            fitnessEquipmentDisplay.GeneralFePageReceived += ProcessInstantaneousSpeed;

            // Process instantaneous cadence every time a Specific Trainer Data page is received
            fitnessEquipmentDisplay.SpecificTrainerPageReceived += ProcessInstantaneousCadence;
        }

        private void ProcessInstantaneousSpeed(GeneralFePage page, uint counter)
        {
            // Convert the instantaneous speed being received from m/s to km/h
            double convertedSpeed = page.Speed * 1000 * 3.6;

            // Update the UI to show the result
            speedLabel.Text = $"Current speed: {convertedSpeed} km/h";

            // Store the converted speed with the corresponding timestamp
            // ...
        }

        private void ProcessInstantaneousCadence(SpecificTrainerPage page, uint counter)
        {
            // Ignore the value returned by the trainer when it cannot measure pedaling cadence
            if (page.InstantaneousCadence == 0xFF)
                return;

            // Update the UI to show the pedaling cadence recorded from the trainer
            cadenceLabel.Text = $"Pedaling cadence: {page.InstantaneousCadence} rpm";

            // Store the recorded pedaling cadence with the corresponding timestamp
            // ...
        }

        /// <summary>
        /// Handles the 'Start' button's click.
        /// </summary>
        /// <param name="sender">System.Windows.Forms.Button instance</param>
        /// <param name="e">EventArgs object</param>
        private void StartButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = false;

            stopwatch.Start();
            updateTimer.Start();
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
        /// Shuts down the communication by closing the slave channel and resetting the device.
        /// </summary>
        private void ShutDownCommunication()
        {
            fitnessEquipmentDisplay.TurnOff();
            ANT_Device.shutdownDeviceInstance(ref device0);
        }

        private void LoadResistanceSchedule(string csvPath)
        {
            resistanceSchedule = new Dictionary<TimeSpan, double>();
            maxDuration = TimeSpan.Zero;

            using (var reader = new StreamReader(csvPath))
            {
                reader.ReadLine(); // Skip header
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
                    TimeSpan timestamp = TimeSpan.ParseExact(values[0].Trim(), @"mm\:ss", CultureInfo.InvariantCulture);
                    double resistance = double.Parse(values[1].Trim(), CultureInfo.InvariantCulture);
                    resistanceSchedule[timestamp] = resistance;

                    // Keep track of the longest timestamp
                    if (timestamp >  maxDuration)
                        maxDuration = timestamp;
                }
            }
        }

        /// <summary>
        /// Checks the current elapsed time and finds the appropriate resistance value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            // 1. Check if the stopwatch is running to avoid unnecessary calculations
            if (!stopwatch.IsRunning)
                return;

            TimeSpan currentTime = stopwatch.Elapsed; // Get the current elapsed time

            // 2. End-of-workout check
            if (currentTime.TotalSeconds >= maxDuration.TotalSeconds)
            {
                WorkoutComplete();
                return;
            }

            // 3. Round down to the nearest second to match CSV format
            TimeSpan roundedTime = TimeSpan.FromSeconds(Math.Floor(currentTime.TotalSeconds));

            // 4. Check if the resistance has changed before sending it to the trainer
            if (resistanceSchedule.TryGetValue(roundedTime, out double resistance))
            {
                // Only set the resistance if it's different from the last value (optimization)
                if (resistance != lastSentResistance)
                {
                    SetTrainerResistance(resistance);
                    lastSentResistance = resistance; // Store the last sent value
                }
            }

            // 5. Update the UI with the *unrounded* current time
            timeLabel.Text = string.Format("{0:mm\\:ss}", currentTime);
        }

        private async void WorkoutComplete()
        {
            stopwatch.Stop();
            updateTimer.Stop();
            ShutDownCommunication();

            // Show the message box asynchronously and wait for it to be closed
            await Task.Run(() => MessageBox.Show("Workout complete!")); // Run on a separate thread

            this.Invoke((Action)delegate { this.Close(); }); // Close the form on the UI thread
        }

        /// <summary>
        /// Sets the fitness equipment's resistance by sending Data Page 48.
        /// </summary>
        /// <param name="resistance"></param>
        private void SetTrainerResistance(double resistance)
        {
            ControlBasicResistancePage command = new ControlBasicResistancePage
            {
                TotalResistance = (byte)(resistance * 2) // Units: 0.5%
            };
            fitnessEquipmentDisplay.SendBasicResistance(command);

            // Update the UI with the resistance % being sent
            desiredResistanceLabel.Text = $"Desired resistance: {resistance}%";
        }
    }
}
