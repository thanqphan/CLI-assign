using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CLI_assign.Models
{
    public class PaperfliesHotel
    {
        [JsonPropertyName("hotel_id")]
        public string HotelId { get; set; }

        [JsonPropertyName("destination_id")]
        public int DestinationId { get; set; }

        [JsonPropertyName("hotel_name")]
        public string HotelName { get; set; }

        [JsonPropertyName("location")]
        public PaperfliesLocation Location { get; set; }

        [JsonPropertyName("details")]
        public string Details { get; set; }

        [JsonPropertyName("amenities")]
        public PaperfliesAmenities Amenities { get; set; }

        [JsonPropertyName("images")]
        public PaperfliesImages Images { get; set; }

        [JsonPropertyName("booking_conditions")]
        public List<string> BookingConditions { get; set; }
    }

    public class PaperfliesLocation
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }
    }
    public class PaperfliesAmenities
    {
        [JsonPropertyName("general")]
        public List<string> General { get; set; }

        [JsonPropertyName("room")]
        public List<string> Room { get; set; }
    }
    public class PaperfliesImages
    {
        [JsonPropertyName("rooms")]
        public List<PaperfliesImage> Rooms { get; set; }

        [JsonPropertyName("site")]
        public List<PaperfliesImage> Site { get; set; }
    }
    public class PaperfliesImage
    {
        [JsonPropertyName("link")]
        public string Link { get; set; }

        [JsonPropertyName("caption")]
        public string Caption { get; set; }
    }

}
