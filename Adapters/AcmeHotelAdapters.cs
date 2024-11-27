using CLI_assign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CLI_assign.Adapters
{
    public class AcmeHotelAdapter : IHotelFetcher
    {
        private const string Endpoint = "https://5f2be0b4ffc88500167b85a0.mockapi.io/suppliers/acme";

        public async Task<List<StandardizedHotel>> FetchHotelsAsync()
        {
            using var client = new HttpClient();
            var response = await client.GetStringAsync(Endpoint);
            var hotels = JsonSerializer.Deserialize<List<AcmeHotel>>(response);

            return hotels.Select(hotel => new StandardizedHotel
            {
                Id = hotel.Id,
                DestinationId = hotel.DestinationId,
                Name = hotel.Name,
                Location = new Location
                {
                    Address = hotel.Address,
                    City = hotel.City,
                    Country = hotel.Country,
                    Lat = hotel.Latitude,
                    Lng = hotel.Longitude
                },
                Description = hotel.Description,
                Amenities = new Amenities
                {
                    General = hotel.Facilities,
                    Room = new List<string>() // No room amenities 
                },
                Images = new Images // No images
                {
                    Rooms = new List<HotelImage>(), 
                    Site = new List<HotelImage>(), 
                    Amenities = new List<HotelImage>() 
                },
                BookingConditions = new List<string>(), // No booking conditions 
                Source= "Acme", // for Top priority 
            }).ToList();
        }
    }
}
