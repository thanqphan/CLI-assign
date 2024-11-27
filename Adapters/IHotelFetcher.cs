using CLI_assign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI_assign.Adapters
{
    public interface IHotelFetcher
    {
        Task<List<StandardizedHotel>> FetchHotelsAsync();
    }
}
