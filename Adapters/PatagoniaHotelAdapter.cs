using CLI_assign.Constants;
using CLI_assign.Models;
using System.Text.Json;

namespace CLI_assign.Adapters
{
    public class PatagoniaHotelAdapter : IHotelFetcher
    {
        public async Task<List<StandardizedHotel>> FetchHotelsAsync()
        {
            using var client = new HttpClient();
            var response = await client.GetStringAsync(HotelFetcherConst.PatagoniaEndpoint);
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
