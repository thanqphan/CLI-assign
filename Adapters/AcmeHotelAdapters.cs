using CLI_assign.Constants;
using CLI_assign.Models;
using System.Text.Json;

namespace CLI_assign.Adapters
{
    public class AcmeHotelAdapter : IHotelFetcher
    {
        public async Task<List<StandardizedHotel>> FetchHotelsAsync()
        {
            using var client = new HttpClient();
            var response = await client.GetStringAsync(HotelFetcherConst.AcmeEndpoint);
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
                Source = "Acme", // for Top priority 
            }).ToList();
        }
    }
}
