#region Using directives

using ANT_Managed_Library;
using AntPlus.Profiles.Common;
using AntPlus.Profiles.FitnessEquipment;
using AntPlus.Profiles.HeartRate;
using AntPlus.Types;
using System;
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
        private static readonly ushort USER_FE_DEVICENUM = 14991;   // Garmin Tacx NEO 3M Smart Trainer
        private static readonly ushort USER_HR_DEVICENUM = 23660;   // Polar H10
        private static readonly byte USER_FE_DEVICETYPE = 0x11;     // Search for ANT+ Fitness Equipment
        private static readonly byte USER_HR_DEVICETYPE = 0x78;     // Search for ANT+ Heart Rate Monitor
        private static readonly byte USER_TRANSTYPE = 0;            // Allow wildcard matching

        private static readonly byte USER_NETWORK_NUM = 0;
        private static readonly byte[] USER_NETWORK_KEY = { 0xB9, 0xA5, 0x21, 0xFB, 0xBD, 0x72, 0xC3, 0x45 };

        private static readonly byte USER_RADIOFREQ = 0x39;         // RF Frequency + 2400 MHz

        private static ANT_Device antDevice;                        // ANT USB Stick (or ANT+ Antenna)
        private static ANT_Channel monitorChannel;                  // Stick (S) - Monitor (M) link
        private static ANT_Channel trainerChannel;                  // Stick (S) - Trainer (M) link

        private static Network networkAntPlus;
        private static HeartRateDisplay heartRateDisplay;
        private static FitnessEquipmentDisplay fitnessEquipmentDisplay;

        private readonly Stopwatch stopwatch;
        private readonly DataManager dataManager;
        private Controller controller;

        private long startTimeTicks = 0;
        private bool firstEventReceived = false;
        private double lastHeartbeatReceived = 0.0;

        public Form1()
        {
            InitializeComponent();
            InitializeANT();
            ConfigureANT();

            // Set up a stopwatch to track the training session duration
            stopwatch = new Stopwatch();

            // Create a data manager to store collected/computed data
            dataManager = new DataManager();
        }

        /// <summary>
        /// Initializes the ANT dongle and its channels.
        /// </summary>
        private void InitializeANT()
        {
            try
            {
                antDevice = new ANT_Device();
                antDevice.deviceResponse += new ANT_Device.dDeviceResponseHandler(DeviceResponse);

                monitorChannel = antDevice.getChannel(0);
                monitorChannel.channelResponse += new dChannelResponseHandler(MonitorChannelResponse);

                trainerChannel = antDevice.getChannel(1);
                trainerChannel.channelResponse += new dChannelResponseHandler(TrainerChannelResponse);
            }
            catch (Exception ex)
            {
                if (antDevice == null)
                    throw new Exception("Could not connect to any device.\n" + "Details: \n   " + ex.Message);
                else
                    throw new Exception("Error connecting to ANT: " + ex.Message);
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
                                        Console.WriteLine("Channels are already closed");
                                        Console.WriteLine("Unassigning Channels...");
                                        if (monitorChannel.unassignChannel(500) && trainerChannel.unassignChannel(500))
                                        {
                                            Console.WriteLine("Unassigned Channels");
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
                            case ANT_ReferenceLibrary.ANTMessageID.CHANNEL_SEARCH_TIMEOUT_0x44:
                            case ANT_ReferenceLibrary.ANTMessageID.SET_LP_SEARCH_TIMEOUT_0x63:
                            case ANT_ReferenceLibrary.ANTMessageID.PROX_SEARCH_CONFIG_0x71:
                                {
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
        /// Called whenever a channel event is received on the Stick-Monitor link.
        /// </summary>
        /// <param name="response"></param>
        static void MonitorChannelResponse(ANT_Response response)
        {
            try
            {
                switch ((ANT_ReferenceLibrary.ANTMessageID)response.responseID)
                {
                    case ANT_ReferenceLibrary.ANTMessageID.RESPONSE_EVENT_0x40:
                        {
                            Console.Write("Monitor (via Stick): ");

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
                                        if (monitorChannel.unassignChannel(500))
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
        /// Called whenever a channel event is received on the Stick-Trainer link.
        /// </summary>
        /// <param name="response"></param>
        static void TrainerChannelResponse(ANT_Response response)
        {
            try
            {
                switch ((ANT_ReferenceLibrary.ANTMessageID)response.responseID)
                {
                    case ANT_ReferenceLibrary.ANTMessageID.RESPONSE_EVENT_0x40:
                        {
                            Console.Write("Trainer (via Stick): ");

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
                                        if (trainerChannel.unassignChannel(500))
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
        /// Sets up the wireless network and subscribes to sensor-generated events.
        /// </summary>
        private void ConfigureANT()
        {
            antDevice.ResetSystem();    // Soft reset
            Thread.Sleep(500);          // Delay 500ms after a reset

            if (!antDevice.setNetworkKey(USER_NETWORK_NUM, USER_NETWORK_KEY, 500))
                throw new Exception("Error configuring network key");

            if (!monitorChannel.setChannelID(USER_HR_DEVICENUM, false, USER_HR_DEVICETYPE, USER_TRANSTYPE, 500))
                throw new Exception("Error configuring Channel 0 ID");

            monitorChannel.setChannelSearchTimeout(0);          // Disable high priority search mode
            monitorChannel.setLowPrioritySearchTimeout(4);      // 10 seconds
            monitorChannel.setProximitySearch(0);               // Disable proximity search

            if (!trainerChannel.setChannelID(USER_FE_DEVICENUM, false, USER_FE_DEVICETYPE, USER_TRANSTYPE, 500))
                throw new Exception("Error configuring Channel 1 ID");

            trainerChannel.setChannelSearchTimeout(0);          // Disable high priority search mode
            trainerChannel.setLowPrioritySearchTimeout(4);      // 10 seconds
            trainerChannel.setProximitySearch(0);               // Disable proximity search

            networkAntPlus = new Network(USER_NETWORK_NUM, USER_NETWORK_KEY, USER_RADIOFREQ);
            heartRateDisplay = new HeartRateDisplay(monitorChannel, networkAntPlus);
            fitnessEquipmentDisplay = new FitnessEquipmentDisplay(trainerChannel, networkAntPlus);

            // A receive rate of 1.02 Hz may be enough
            heartRateDisplay.ChannelParameters.ChannelPeriod = HeartRate.SlaveChannelPeriod.OneHz;

            // Handle events generated by the heart rate monitor
            heartRateDisplay.HeartRateDataReceived += ProcessHeartRateData;

            // Handle trainer-generated events
            fitnessEquipmentDisplay.GeneralFePageReceived += ProcessSpeedData;
            fitnessEquipmentDisplay.SpecificTrainerPageReceived += ProcessInstantaneousData;
            fitnessEquipmentDisplay.CommandStatusPageReceived += VerifyControlOutcome;

            // Begin searching for the sensors
            heartRateDisplay.TurnOn();
            fitnessEquipmentDisplay.TurnOn();
        }

        /// <summary>
        /// Displays and stores the received heartbeat.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="counter"></param>
        public void ProcessHeartRateData(HeartRateData data, uint counter)
        {
            lastHeartbeatReceived = data.HeartRate;
            UpdateUIWithHeartRateData(lastHeartbeatReceived);

            if (!stopwatch.IsRunning) return;
            StoreData("HR (bpm)", lastHeartbeatReceived);
        }

        /// <summary>
        /// Updates the UI with the last received heartbeat.
        /// </summary>
        /// <param name="heartRate"></param>
        private void UpdateUIWithHeartRateData(double heartRate)
        {
            if (InvokeRequired)
                Invoke(new Action(() => heartRateLabel.Text = $"{heartRate} bpm"));
            else
                heartRateLabel.Text = $"{heartRate} bpm";
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

            UpdateUIWithSpeedData(roundedSpeed);

            if (!stopwatch.IsRunning) return;
            StoreData("Speed (km/h)", roundedSpeed);
        }

        /// <summary>
        /// Updates the UI with the last recorded instantaneous speed.
        /// </summary>
        /// <param name="roundedSpeed"></param>
        private void UpdateUIWithSpeedData(double roundedSpeed)
        {
            if (InvokeRequired)
                Invoke(new Action(() => speedLabel.Text = $"{roundedSpeed} km/h"));
            else
                speedLabel.Text = $"{roundedSpeed} km/h";
        }

        /// <summary>
        /// Displays the instantaneous cadence and power recorded by the trainer and stores them.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="counter"></param>
        private void ProcessInstantaneousData(SpecificTrainerPage page, uint counter)
        {
            UpdateUIWithInstantaneousData(page.InstantaneousCadence, page.InstantaneousPower);

            if (!stopwatch.IsRunning) return;
            StoreData("Cadence (rpm)", page.InstantaneousCadence);
            StoreData("Power (W)", page.InstantaneousPower);
        }

        /// <summary>
        /// Updates the UI with the last recorded instantaneous cadence and power.
        /// </summary>
        /// <param name="instantaneousCadence"></param>
        /// <param name="instantaneousPower"></param>
        private void UpdateUIWithInstantaneousData(byte instantaneousCadence, ushort instantaneousPower)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => cadenceLabel.Text = $"{instantaneousCadence} rpm"));
                Invoke(new Action(() => powerLabel.Text = $"{instantaneousPower} W"));
            }
            else
            {
                cadenceLabel.Text = $"{instantaneousCadence} rpm";
                powerLabel.Text = $"{instantaneousPower} W";
            }
        }

        /// <summary>
        /// Checks whether the trainer has received a command and processed it correctly.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="counter"></param>
        private void VerifyControlOutcome(CommandStatusPage page, uint counter)
        {
            if (page.LastReceivedCommandID == 0x30) // Control Page 48 – Basic Resistance
            {
                switch (page.Status)
                {
                    case Common.CommandStatus.Pass:
                        Console.WriteLine("Command received and processed successfully.");

                        double actualResistance = page.Data[3] / 2;
                        Console.WriteLine($"Currently applied resistance: {actualResistance}%");

                        UpdateUIWithActualResistance(actualResistance);
                        StoreData("Resistance (%)", actualResistance);      // !!!
                        break;

                    case Common.CommandStatus.Fail:
                    case Common.CommandStatus.Rejected:
                        Console.WriteLine("Command failed or rejected.");
                        break;

                    case Common.CommandStatus.Pending:
                        Console.WriteLine("Command still pending processing by trainer.");
                        break;

                    case Common.CommandStatus.NotSupported:
                        Console.WriteLine("Command not supported by this trainer.");
                        break;

                    case Common.CommandStatus.Uninitialized:
                        Console.WriteLine("Trainer reports never receiving a command.");
                        break;
                }
            }
        }

        /// <summary>
        /// Updates the UI with the resistance the trainer is currently applying.
        /// </summary>
        /// <param name="resistance"></param>
        private void UpdateUIWithActualResistance(double resistance)
        {
            if (InvokeRequired)
                Invoke(new Action(() => actualResistanceLabel.Text = $"Actual: {resistance} %"));
            else
                actualResistanceLabel.Text = $"Actual: {resistance} %";
        }

        /// <summary>
        /// Stores acquired or computed data for later analysis and plotting.
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="value"></param>
        private void StoreData(string sourceName, object value)
        {
            long timestamp;

            if (!firstEventReceived)
            {
                startTimeTicks = DateTime.Now.Ticks;                // Capture the time of the very first event
                timestamp = 0;
                firstEventReceived = true;
            }
            else
            {
                timestamp = DateTime.Now.Ticks - startTimeTicks;
                timestamp /= TimeSpan.TicksPerMillisecond;          // Convert to milliseconds
            }

            dataManager.AddData(sourceName, value, timestamp);
        }

        /// <summary>
        /// Starts the data collection process and creates a PI controller configured as desired.
        /// </summary>
        /// <param name="sender">System.Windows.Forms.Button instance</param>
        /// <param name="e">EventArgs object</param>
        private void StartButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = false;
            numericUpDown.Enabled = false;
            stopButton.Enabled = true;

            double setpoint = Decimal.ToDouble(numericUpDown.Value);
            controller = new Controller(setpoint, kp: 1, ki: 0);

            // Reset data collection
            dataManager.ClearData();
            controller.Reset();
            startTimeTicks = 0;
            firstEventReceived = false;
            lastHeartbeatReceived = 0;

            stopwatch.Start();
            updateTimer.Start();
            controlTimer.Start();
        }

        /// <summary>
        /// Stops timing components and makes the training session end.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopButton_Click(object sender, EventArgs e)
        {
            stopwatch.Stop();
            updateTimer.Stop();
            controlTimer.Stop();
            SendTargetResistance(0);

            WorkoutComplete();
        }

        /// <summary>
        /// Generates a CSV file with workout-related data, to then close the app.
        /// </summary>
        private async void WorkoutComplete()
        {
            ShutDownCommunication();

            // Generate a CSV file with all values describing the performed activity
            dataManager.WriteToCsv("test01.csv");

            // Show a message box asynchronously and wait for it to be closed
            await Task.Run(() => MessageBox.Show("Report file generated!"));

            // Close the form on the UI thread
            this.Invoke((Action)delegate { this.Close(); });
        }

        /// <summary>
        /// Terminates communication by closing the channels and resetting the device.
        /// </summary>
        private void ShutDownCommunication()
        {
            heartRateDisplay.TurnOff();
            fitnessEquipmentDisplay.TurnOff();
            ANT_Device.shutdownDeviceInstance(ref antDevice);
        }

        /// <summary>
        /// Calculates and sends control actions to be applied based on the last heartbeat received.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlTimer_Tick(object sender, EventArgs e)
        {
            // Calculate the new target resistance
            double newResistance = Math.Round(controller.ComputeControlAction(lastHeartbeatReceived));

            // Send a command to the trainer to apply it
            SendTargetResistance(newResistance);

            // Send a request to the trainer to verify the outcome
            RequestCommandStatus();
        }

        /// <summary>
        /// Transmits the computed resistance to the trainer by sending Data Page 48.
        /// </summary>
        /// <param name="resistance"></param>
        private void SendTargetResistance(double resistance)
        {
            ControlBasicResistancePage command = new ControlBasicResistancePage
            {
                TotalResistance = (byte)(resistance * 2) // Convert to the appropriate format (units: 0.5%)
            };
            fitnessEquipmentDisplay.SendBasicResistance(command);

            UpdateUIWithTargetResistance(resistance);
        }

        /// <summary>
        /// Updates the UI with the last sent resistance command's value.
        /// </summary>
        /// <param name="resistance"></param>
        private void UpdateUIWithTargetResistance(double resistance)
        {
            if (InvokeRequired)
                Invoke(new Action(() => targetResistanceLabel.Text = $"Target: {resistance} %"));
            else
                targetResistanceLabel.Text = $"Target: {resistance} %";
        }

        /// <summary>
        /// Checks if a control message was successful by requesting the currently applied settings to the trainer.
        /// </summary>
        private void RequestCommandStatus()
        {
            RequestDataPage request = new RequestDataPage
            {
                RequestedPageNumber = 0x47 // Common Page 71 – Command Status
            };
            fitnessEquipmentDisplay.SendDataPageRequest(request);
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
    }
}
