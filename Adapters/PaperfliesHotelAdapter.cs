using CLI_assign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CLI_assign.Adapters
{
    public class PaperfliesHotelAdapter : IHotelFetcher
    {
        private const string Endpoint = "https://5f2be0b4ffc88500167b85a0.mockapi.io/suppliers/paperflies";
        public async Task<List<StandardizedHotel>> FetchHotelsAsync()
        {
            using var client = new HttpClient();
            var response = await client.GetStringAsync(Endpoint);
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
                Source= "Paperflies" // top priority
            }).ToList();
        }
    }
}
