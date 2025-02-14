using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsFormsApp
{
    public class SensorData
    {
        /// <summary>
        /// Dictionary of Dictionaries.
        /// The key is the timestamp (in milliseconds).
        /// The value is a dictionary holding the sensor values for that specific time.
        /// </summary>
        private Dictionary<long, Dictionary<string, double>> data = new Dictionary<long, Dictionary<string, double>>();
        
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

        // Make sure the data types match the actual data you're receiving from the sensors!
        public void AddData(string sensorName, double value)
        {
            long timestamp = GetCurrentTimestamp();

            // Use a lock for thread safety
            lock (data)
            {
                if (!data.ContainsKey(timestamp))
                    data[timestamp] = new Dictionary<string, double>();

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
                    // Write header row
                    writer.WriteLine("Time (ms),HR,Speed,Cadence");

                    foreach (var kvp in data.OrderBy(x => x.Key))
                    {
                        long timestamp = kvp.Key;
                        Dictionary<string, double> sensorValues = kvp.Value;

                        writer.Write(timestamp + ",");
                        writer.Write(sensorValues.ContainsKey("HR") ? sensorValues["HR"].ToString() : "" + ",");
                        writer.Write(sensorValues.ContainsKey("Speed") ? sensorValues["Speed"].ToString() : "" + ",");
                        writer.Write(sensorValues.ContainsKey("Cadence") ? sensorValues["Cadence"].ToString() : "");
                    }
                }
            }
        }

        public void Reset()
        {
            lock (data)
            {
                data.Clear();
                stopwatch.Restart();
                baseTimestamp = DateTime.Now.Ticks;
            }
        }
    }
}
