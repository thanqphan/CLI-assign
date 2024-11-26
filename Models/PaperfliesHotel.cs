using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI_assign.Models
{
    public class PaperfliesHotel
    {
        public string HotelId { get; set; }
        public int DestinationId { get; set; }
        public string HotelName { get; set; }
        public PaperfliesLocation Location { get; set; }
        public string Details { get; set; }
        public PaperfliesAmenities Amenities { get; set; }
        public PaperfliesImages Images { get; set; }
        public List<string> BookingConditions { get; set; }
    }

    public class PaperfliesLocation
    {
        public string Address { get; set; }
        public string Country { get; set; }
    }

    public class PaperfliesAmenities
    {
        public List<string> General { get; set; }
        public List<string> Room { get; set; }
    }

    public class PaperfliesImages
    {
        public List<PaperfliesImage> Rooms { get; set; }
        public List<PaperfliesImage> Site { get; set; }
    }

    public class PaperfliesImage
    {
        public string Link { get; set; }
        public string Caption { get; set; }
    }
}
