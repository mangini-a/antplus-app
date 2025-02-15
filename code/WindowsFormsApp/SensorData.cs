#region Using directives

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace WindowsFormsApp
{
    public class SensorData
    {
        /// <summary>
        /// Dictionary of Dictionaries.
        /// The key is the timestamp (in milliseconds).
        /// The value is a dictionary holding the sensor values for that specific time.
        /// </summary>
        private Dictionary<long, Dictionary<string, object>> data = new Dictionary<long, Dictionary<string, object>>();
        
        private Stopwatch stopwatch;
        private long baseTimestamp = 0;

        public SensorData(Stopwatch existingStopwatch)
        {
            this.stopwatch = existingStopwatch; // Use the provided stopwatch
        }

        public void SetBaseTimestamp()
        {
            baseTimestamp = DateTime.Now.Ticks;
        }

        /// <summary>
        /// Stores a measurement recorded by a sensor.
        /// Invoked when a page of interest is received.
        /// Does the 'double' data type match the actual data received from the sensors?!
        /// </summary>
        /// <param name="sensorName"></param>
        /// <param name="value"></param>
        public void AddData(string sensorName, double value)
        {
            long timestamp = GetCurrentTimestamp();

            // Use a lock for thread safety
            lock (data)
            {
                if (!data.ContainsKey(timestamp))
                    data[timestamp] = new Dictionary<string, object>();

                data[timestamp][sensorName] = value;
            }
        }

        private long GetCurrentTimestamp()
        {
            // Convert elapsed time to milliseconds and add to base timestamp
            return (long)(stopwatch.Elapsed.TotalMilliseconds + (baseTimestamp / TimeSpan.TicksPerMillisecond));
        }

        /// <summary>
        /// Iterates through the data dictionary, sorted by timestamp, and writes each row to a CSV file.
        /// </summary>
        /// <param name="filePath"></param>
        public void WriteToCsv(string filePath)
        {
            lock (data) // Lock during writing as well
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write header
                    //writer.WriteLine("Time (ms),HR (bpm),Speed (km/h),Cadence (rpm)");
                    writer.WriteLine("Time (ms),HR (bpm),Speed (km/h)");

                    // Write entries
                    foreach (KeyValuePair<long, Dictionary<string, object>> kvp in data)
                    {
                        long timestamp = kvp.Key;
                        Dictionary<string, object> sensorValues = kvp.Value;

                        writer.Write(timestamp + ",");
                        writer.Write(sensorValues.ContainsKey("HR (bpm)") ? sensorValues["HR (bpm)"].ToString() : "" + ",");
                        writer.WriteLine(sensorValues.ContainsKey("Speed (km/h)") ? sensorValues["Speed (km/h)"].ToString() : "");
                        //writer.WriteLine(sensorValues.ContainsKey("Cadence (rpm)") ? sensorValues["Cadence (rpm)"].ToString() : "");
                    }
                }
            }
        }

        public void Reset()
        {
            lock (data)
            {
                data.Clear();
                baseTimestamp = 0;
            }
        }
    }
}
