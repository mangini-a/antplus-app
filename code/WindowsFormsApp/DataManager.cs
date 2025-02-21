#region Using directives

using System.Collections.Generic;
using System.IO;

#endregion

namespace WindowsFormsApp
{
    public class DataManager
    {
        /// <summary>
        /// Key: timestamp (in ms).
        /// Value: a dictionary holding the sensor values for that specific time.
        /// </summary>
        private readonly Dictionary<long, Dictionary<string, object>> data;

        public DataManager()
        {
            data = new Dictionary<long, Dictionary<string, object>>();
        }

        /// <summary>
        /// Stores a value in the dictionary that takes care of it.
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="value"></param>
        public void AddData(string sourceName, object value, long timestamp)
        {
            lock (data) // Use a lock for thread safety
            {
                if (!data.ContainsKey(timestamp))
                    data[timestamp] = new Dictionary<string, object>();

                data[timestamp][sourceName] = value;
            }
        }

        /// <summary>
        /// Iterates through the data dictionary and writes each row to a CSV file.
        /// </summary>
        /// <param name="filePath"></param>
        public void WriteToCsv(string filePath)
        {
            lock (data) // Lock during writing as well
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write header
                    writer.WriteLine("Time (ms),Cadence (rpm),Power (W),Speed (km/h),HR (bpm),Resistance (%)");

                    // Write entries
                    foreach (KeyValuePair<long, Dictionary<string, object>> kvp in data)
                    {
                        long timestamp = kvp.Key;
                        Dictionary<string, object> values = kvp.Value;

                        writer.Write(timestamp + ","); // 'timestamp' is the elapsed time in milliseconds

                        writer.Write(values.ContainsKey("Cadence (rpm)") ? values["Cadence (rpm)"].ToString() : "");
                        writer.Write(",");

                        writer.Write(values.ContainsKey("Power (W)") ? values["Power (W)"].ToString() : "");
                        writer.Write(",");

                        writer.Write(values.ContainsKey("Speed (km/h)") ? values["Speed (km/h)"].ToString() : "");
                        writer.Write(",");

                        writer.Write(values.ContainsKey("HR (bpm)") ? values["HR (bpm)"].ToString() : "");
                        writer.Write(",");

                        writer.WriteLine(values.ContainsKey("Resistance (%)") ? values["Resistance (%)"].ToString() : "");
                    }
                }
            }
        }

        /// <summary>
        /// Clears data collected during an activity.
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
