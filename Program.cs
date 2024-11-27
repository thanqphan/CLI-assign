using CLI_assign.Services;
using System.Text.Json;

namespace CLI_assign
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: my_hotel_merger <hotel_ids> <destination_ids>");
                return;
            }

            var hotelIds = args[0];
            var destinationIds = args[1];

            // Fetch data from all suppliers
            var hotelService = new HotelService();
            var mergedHotels = await hotelService.FetchAndMergeHotelsAsync();

            // Filter data based on input arguments
            var filteredHotels = hotelService.FilterHotels(mergedHotels, hotelIds, destinationIds);

            // Convert to JSON and print output
            var resultJson = JsonSerializer.Serialize(filteredHotels, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            Console.WriteLine(resultJson);
        }

    }
}
