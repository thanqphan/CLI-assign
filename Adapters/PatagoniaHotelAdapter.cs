using CLI_assign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CLI_assign.Adapters
{
    public class PatagoniaHotelAdapter : IHotelFetcher
    {
        private const string Endpoint = "https://5f2be0b4ffc88500167b85a0.mockapi.io/suppliers/patagonia";

        public async Task<List<StandardizedHotel>> FetchHotelsAsync()
        {
            using var client = new HttpClient();
            var response = await client.GetStringAsync(Endpoint);
            var hotels = JsonSerializer.Deserialize<List<PatagoniaHotel>>(response);

            return hotels.Select(hotel => new StandardizedHotel
            {
                Id = hotel.Id,
                DestinationId = hotel.Destination,
                Name = hotel.Name,
                Location = new Location
                {
                    Address = hotel.Address,
                    City = null,
                    Country = null,
                    Lat = hotel.Lat,
                    Lng = hotel.Lng
                },
                Description = hotel.Info,
                Amenities = new Amenities
                {
                    General = hotel.Amenities,
                    Room = new List<string>() // No room amenities directly
                },
                Images = new Images
                {
                    Rooms = hotel.Images?.Rooms?.Select(img => new HotelImage
                    {
                        Link = img.Url,
                        Description = img.Description
                    }).ToList(),
                    Site = new List<HotelImage>(),  // No site images 
                    Amenities = hotel.Images?.Amenities?.Select(img => new HotelImage
                    {
                        Link = img.Url,
                        Description = img.Description
                    }).ToList(),
                },
                BookingConditions = new List<string>(), // No booking conditions 
                Source = "Patagonia"
            }).ToList();
        }
    }
}
