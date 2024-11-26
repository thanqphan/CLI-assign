using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CLI_assign.Models
{
    public class PatagoniaHotel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("destination")]
        public int Destination { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("lat")]
        public double Lat { get; set; }

        [JsonPropertyName("lng")]
        public double Lng { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("info")]
        public string Info { get; set; }
        [JsonPropertyName("amenities")]

        public List<string> Amenities { get; set; }
        [JsonPropertyName("images")]
        public Images Images { get; set; }
    }

    public class PatagoniaImages
    {
        [JsonPropertyName("rooms")]
        public List<PatagoniaImage> Rooms { get; set; }
        [JsonPropertyName("amenities")]
        public List<PatagoniaImage> Amenities { get; set; }

    }

    public class PatagoniaImage
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
