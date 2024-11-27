using CLI_assign.Adapters;
using CLI_assign.Models;
using CLI_assign.Utils;

namespace CLI_assign.Services
{
    public class HotelService
    {
        private readonly List<IHotelFetcher> _suppliers;

        public HotelService()
        {
            _suppliers = new List<IHotelFetcher>
            {
                new PaperfliesHotelAdapter(),
                new PatagoniaHotelAdapter(),
                new AcmeHotelAdapter()
            };
        }
        public async Task<List<StandardizedHotel>> FetchAndMergeHotelsAsync()
        {
            var allHotels = new List<StandardizedHotel>();

            foreach (var supplier in _suppliers)
            {
                var supplierHotels = await supplier.FetchHotelsAsync();
                allHotels.AddRange(supplierHotels);
            }

            // Group by hotel_id and destination_id, then merge data
            var mergedHotels = allHotels
                .GroupBy(h => new { h.Id, h.DestinationId })
                .Select(group => MergeHotelData(group.ToList()))
                .ToList();

            return mergedHotels;
        }

        public List<StandardizedHotel> FilterHotels(List<StandardizedHotel> hotels, string hotelIds, string destinationIds)
        {
            // Parse input arguments
            var hotelIdList = hotelIds == "none" ? null : hotelIds.Split(',').ToList();
            var destinationIdList = destinationIds == "none" ? null : destinationIds.Split(',').ToList();

            return hotels.Where(hotel =>
                (hotelIdList == null || hotelIdList.Contains(hotel.Id)) &&
                (destinationIdList == null || destinationIdList.Contains(hotel.DestinationId.ToString()))
            ).ToList();
        }

        #region
        private StandardizedHotel MergeHotelData(List<StandardizedHotel> hotels)
        {
            // top priority
            var paperfliesHotel = hotels.FirstOrDefault(h => h.Source == "Paperflies");

            // if not ^
            var baseHotel = paperfliesHotel ?? hotels.First();

            var mergedHotel = new StandardizedHotel
            {
                Id = baseHotel.Id,
                DestinationId = baseHotel.DestinationId,
                Name = baseHotel.Name ?? hotels.FirstOrDefault(h => !string.IsNullOrWhiteSpace(h.Name))?.Name,
                Description = baseHotel.Description ?? hotels.FirstOrDefault
                                                (h => !string.IsNullOrWhiteSpace(h.Description))?.Description,
                Location = new Location
                {
                    Lat = baseHotel.Location?.Lat ?? hotels.FirstOrDefault(h => h.Location?.Lat != null)?.Location?.Lat,
                    Lng = baseHotel.Location?.Lng ?? hotels.FirstOrDefault(h => h.Location?.Lng != null)?.Location?.Lng,
                    Address = baseHotel.Location?.Address ?? hotels.FirstOrDefault
                                                (h => !string.IsNullOrWhiteSpace(h.Location?.Address))?.Location?.Address,
                    City = baseHotel.Location?.City ?? hotels.FirstOrDefault
                                                (h => !string.IsNullOrWhiteSpace(h.Location?.City))?.Location?.City,
                    Country = baseHotel.Location?.Country ?? hotels.FirstOrDefault
                                                (h => !string.IsNullOrWhiteSpace(h.Location?.Country))?.Location?.Country
                },
                Amenities = MergeAmenities(hotels),
                Images = MergeImages(hotels),
                BookingConditions = MergeUtils.MergeUnique(
                    hotels.SelectMany(h => h.BookingConditions ?? new List<string>()).ToList(),
                    new List<string>(),
                    condition => condition
                )
            };

            return mergedHotel;
        }
        // Merge amenities with additional checks for overlap between "general" and "room" :D
        private Amenities MergeAmenities(List<StandardizedHotel> hotels)
        {
            var generalAmenities = hotels
                .SelectMany(h => h.Amenities?.General ?? new List<string>())
                .Select(a => a.ToLowerInvariant()) // Convert to lowercase
                .Distinct()
                .ToList();

            var roomAmenities = hotels
                .SelectMany(h => h.Amenities?.Room ?? new List<string>())
                .Select(a => a.ToLowerInvariant()) // Convert to lowercase
                .Distinct()
                .ToList();

            // Remove items from "general" that already exist in "room"
            generalAmenities = generalAmenities.Where(a => !roomAmenities.Contains(a)).ToList();

            return new Amenities
            {
                General = generalAmenities,
                Room = roomAmenities
            };
        }

        // Merge images, ensuring no duplicates based on "link" and "description"
        private Images MergeImages(List<StandardizedHotel> hotels)
        {
            return new Images
            {
                Rooms = MergeUtils.MergeUnique(
                    hotels.SelectMany(h => h.Images?.Rooms ?? new List<HotelImage>()).ToList(),
                    new List<HotelImage>(), // default empty
                    image => $"{image.Link?.ToLowerInvariant() ?? string.Empty}|{image.Description?.ToLowerInvariant() ?? string.Empty}"
                ),

                Site = MergeUtils.MergeUnique(
                    hotels.SelectMany(h => h.Images?.Site ?? new List<HotelImage>()).ToList(),
                    new List<HotelImage>(),
                    image => $"{image.Link?.ToLowerInvariant() ?? string.Empty}|{image.Description?.ToLowerInvariant() ?? string.Empty}"
                ),

                Amenities = MergeUtils.MergeUnique(
                    hotels.SelectMany(h => h.Images?.Amenities ?? new List<HotelImage>()).ToList(),
                    new List<HotelImage>(), 
                    image => $"{image.Link?.ToLowerInvariant() ?? string.Empty}|{image.Description?.ToLowerInvariant() ?? string.Empty}"
                )
            };
        }
        #endregion

    }
}
