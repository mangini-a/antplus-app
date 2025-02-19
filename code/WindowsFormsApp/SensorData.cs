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
        /// Key: timestamp (in ms).
        /// Value: a dictionary holding the sensor values for that specific time.
        /// </summary>
        private readonly Dictionary<long, Dictionary<string, object>> data;

        public SensorData()
        {
            data = new Dictionary<long, Dictionary<string, object>>();
        }

        /// <summary>
        /// Stores a measurement recorded by a sensor.
        /// </summary>
        /// <param name="sensorName"></param>
        /// <param name="value"></param>
        public void AddData(string sensorName, object value, long timestamp)
        {
            lock (data) // Use a lock for thread safety
            {
                if (!data.ContainsKey(timestamp))
                    data[timestamp] = new Dictionary<string, object>();

                data[timestamp][sensorName] = value;
            }
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
                    writer.WriteLine("Time (ms),Cadence (rpm),Power (W),Speed (km/h),HR (bpm)");

                    // Write entries
                    foreach (KeyValuePair<long, Dictionary<string, object>> kvp in data)
                    {
                        long timestamp = kvp.Key;
                        Dictionary<string, object> sensorValues = kvp.Value;

                        writer.Write(timestamp + ","); // 'timestamp' is the elapsed time in milliseconds

                        writer.Write(sensorValues.ContainsKey("Cadence (rpm)") ? sensorValues["Cadence (rpm)"].ToString() : "");
                        writer.Write(",");

                        writer.Write(sensorValues.ContainsKey("Power (W)") ? sensorValues["Power (W)"].ToString() : "");
                        writer.Write(",");

                        writer.Write(sensorValues.ContainsKey("Speed (km/h)") ? sensorValues["Speed (km/h)"].ToString() : "");
                        writer.Write(",");

                        writer.WriteLine(sensorValues.ContainsKey("HR (bpm)") ? sensorValues["HR (bpm)"].ToString() : "");
                    }
                }
            }
        }

        /// <summary>
        /// Clears the collected sensor measurements.
        /// </summary>
        public void ClearData()
        {
            lock (data)
            {
                data.Clear();
            }
        }
    }
}
