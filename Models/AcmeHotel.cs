using CLI_assign.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CLI_assign.Models
{
    public class AcmeHotel
    {
        [JsonPropertyName("Id")]
        public string Id { get; set; }

        [JsonPropertyName("DestinationId")]
        public int DestinationId { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Latitude")]
        [JsonConverter(typeof(NullableConverter))]
        public double? Latitude { get; set; }

        [JsonPropertyName("Longitude")]
        [JsonConverter(typeof(NullableConverter))]
        public double? Longitude { get; set; }

        [JsonPropertyName("Address")]
        public string Address { get; set; }

        [JsonPropertyName("City")]
        public string City { get; set; }

        [JsonPropertyName("Country")]
        public string Country { get; set; }

        [JsonPropertyName("PostalCode")]
        public string PostalCode { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }
        [JsonPropertyName("Facilities")]
        public List<string> Facilities { get; set; }
    }
}
