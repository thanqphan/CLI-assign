using CLI_assign.Constants;
using CLI_assign.Models;
using System.Text.Json;

namespace CLI_assign.Adapters
{
    public class PaperfliesHotelAdapter : IHotelFetcher
    {
        public async Task<List<StandardizedHotel>> FetchHotelsAsync()
        {
            using var client = new HttpClient();
            var response = await client.GetStringAsync(HotelFetcherConst.PaperfliesEndpoint);
            var hotels = JsonSerializer.Deserialize<List<PaperfliesHotel>>(response);

            return hotels.Select(hotel => new StandardizedHotel
            {
                Id = hotel.HotelId,
                DestinationId = hotel.DestinationId,
                Name = hotel.HotelName,
                Location = new Location
                {
                    Address = hotel.Location?.Address,
                    City = null,
                    Country = hotel.Location?.Country,
                    Lat = null, // No latitude or longitude
                    Lng = null
                },
                Description = hotel.Details,
                Amenities = new Amenities
                {
                    General = hotel.Amenities?.General ?? new List<string>(),
                    Room = hotel.Amenities?.Room ?? new List<string>()
                },
                Images = new Images
                {
                    Rooms = hotel.Images?.Rooms?.Select(img => new HotelImage
                    {
                        Link = img.Link,
                        Description = img.Caption
                    }).ToList(),
                    Site = hotel.Images?.Site?.Select(img => new HotelImage
                    {
                        Link = img.Link,
                        Description = img.Caption
                    }).ToList(),
                    Amenities = new List<HotelImage>() // No amenities images 
                },
                BookingConditions = hotel.BookingConditions ?? new List<string>(),
                Source = "Paperflies" // top priority
            }).ToList();
        }
    }
}
