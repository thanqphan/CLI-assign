using CLI_assign.Services;
using System.Text.Json;

namespace CLI_assign
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: dotnet run --project CLI-assign.csproj $1 $2, etc");
                return;
            }

            // Parse input arguments into pairs of (hotel_id, destination_id)
            var parsedPairs = ParseInputArgs(args);

            // Fetch and merge hotel data
            var hotelService = new HotelService();
            var mergedHotels = await hotelService.FetchAndMergeHotelsAsync();

            // Filter data based on parsed pairs
            var filteredHotels = parsedPairs
                .SelectMany(pair => hotelService.FilterHotels(mergedHotels, pair.HotelId, pair.DestinationId))
                .DistinctBy(h => new { h.Id, h.DestinationId })
                .ToList();

            // Convert result to JSON
            var resultJson = JsonSerializer.Serialize(filteredHotels, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            Console.WriteLine(resultJson);
        }
        static List<(string HotelId, string DestinationId)> ParseInputArgs(string[] args)
        {
            // merge input arguments
            var input = string.Join(" ", args);

            // split merged ","
            var segments = input
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(segment => segment.Trim())
                .ToList();

            var pairs = new List<(string HotelId, string DestinationId)>();

            // must have 2 elements
            foreach (var segment in segments)
            {
                var parts = segment.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 1)
                {
                    pairs.Add((parts[0], "none")); // desId="none"
                }
                else if (parts.Length == 2)
                {
                    pairs.Add((parts[0], parts[1])); // nice case
                }
                else
                {
                    Console.WriteLine($"Invalid input segment: {segment}");
                }
            }

            return pairs;
        }


    }

}
