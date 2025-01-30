#region Using directives

using ANT_Managed_Library;
using AntPlus.Profiles.HeartRate;
using AntPlus.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        static readonly byte USER_ANT_CHANNEL = 0;      // ANT Channel to use
        static readonly ushort USER_DEVICENUM = 0;      // Allow wildcard matching
        static readonly byte USER_DEVICETYPE = 0x78;    // Search for an ANT+ HR Monitor
        static readonly byte USER_TRANSTYPE = 1;        // Transmission type

        static readonly byte USER_NETWORK_NUM = 0;          
        static readonly byte[] USER_NETWORK_KEY = { 0xB9, 0xA5, 0x21, 0xFB, 0xBD, 0x72, 0xC3, 0x45 };

        static readonly byte USER_RADIOFREQ = 0x39;     // RF Frequency + 2400 MHz

        static ANT_Device device0;
        static ANT_Channel channel0;

        static HeartRateDisplay heartRateDisplay;
        static Network networkAntPlus;

        private readonly Dictionary<uint, byte> heartRateData;

        public Form1()
        {
            InitializeComponent();
            InitializeANT();
            ConfigureANT();
            heartRateData = new Dictionary<uint, byte>();
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
            device0.ResetSystem();                      // Soft reset
            System.Threading.Thread.Sleep(500);         // Delay 500ms after a reset

            if (!device0.setNetworkKey(USER_NETWORK_NUM, USER_NETWORK_KEY, 500))
                throw new Exception("Error configuring network key");

            if (!channel0.setChannelID(USER_DEVICENUM, false, USER_DEVICETYPE, USER_TRANSTYPE, 500))  // Not using pairing bit
                throw new Exception("Error configuring Channel ID");

            networkAntPlus = new Network(USER_NETWORK_NUM, USER_NETWORK_KEY, USER_RADIOFREQ);
            heartRateDisplay = new HeartRateDisplay(channel0, networkAntPlus);

            // Process HR data every time it is received
            heartRateDisplay.HeartRateDataReceived += ProcessHeartRateData;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = false;
            stopButton.Enabled = true;

            // Clear previous data (if any)
            if (heartRateData.Count != 0)
                heartRateData.Clear();

            // Begin searching for a sensor
            heartRateDisplay.TurnOn();          
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            stopButton.Enabled = false;

            StopFetchingData();
            SaveDataToCSV();

            heartRateLabel.Text = "...";
        }

        private void ProcessHeartRateData(HeartRateData data, uint counter)
        {
            // Update the UI with the incoming HR data
            if (InvokeRequired)
                Invoke(new Action(() => UpdateHeartRate(data.HeartRate)));
            else
                UpdateHeartRate(data.HeartRate);

            // Store the incoming HR data with the corresponding counter
            if (!heartRateData.ContainsKey(counter))
                heartRateData.Add(counter, data.HeartRate);
        }

        private void UpdateHeartRate(byte heartRate)
        {
            heartRateLabel.Text = $"{heartRate} bpm";
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
        /// Shuts down all communication by closing the slave channel and resetting the device.
        /// </summary>
        private void StopFetchingData()
        {
            heartRateDisplay.TurnOff();
            ANT_Device.shutdownDeviceInstance(ref device0);
        }

        private void SaveDataToCSV()
        {
            using (StreamWriter writer = new StreamWriter("hr_data.csv"))
            {
                // Write header
                writer.WriteLine("Rx Event,Heart Rate (BPM)");

                // Write entries
                foreach (KeyValuePair<uint, byte> kvp in heartRateData)
                    writer.WriteLine($"{kvp.Key},{kvp.Value}");             
            }
            MessageBox.Show("Data saved to hr_data.csv");
        }
    }
}
