using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Backend.Dtos
{
    public class GPSDto
    {
        public long TimeStamp { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public static GPSDto ParseJSONGPSDto(string json)
        {
            GPSDto gps = new GPSDto();
            JsonTextReader reader = new JsonTextReader(new StringReader(json));
            while (reader.Read())
            {
                switch (reader.Value)
                {                  
                    case "time":
                        reader.Read();
                        gps.TimeStamp = (long)reader.Value;
                        break;
                    case "lon":
                        reader.Read();
                        gps.Longitude = (double)reader.Value;
                        break;
                    case "lat":
                        reader.Read();
                        gps.Latitude = (double)reader.Value;
                        break;
                    default:
                        break;
                }
            }
            return gps;
        }

        public static string generateJSON(List<GPSDto> list)
        {
            string json = JsonConvert.SerializeObject(list);
            return json;
        }
        public static string generateJSON(GPSDto gps)
        {
            string json = JsonConvert.SerializeObject(gps);
            return json;
        }
    }
}
