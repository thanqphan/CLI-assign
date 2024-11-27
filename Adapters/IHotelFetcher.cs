using CLI_assign.Models;

namespace CLI_assign.Adapters
{
    public interface IHotelFetcher
    {
        Task<List<StandardizedHotel>> FetchHotelsAsync();
    }
}
