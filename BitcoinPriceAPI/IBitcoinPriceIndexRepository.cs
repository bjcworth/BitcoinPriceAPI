using System;
using System.Threading.Tasks;
using BitcoinPriceAPI.Model;

namespace BitcoinPriceAPI
{
    public interface IBitcoinPriceIndexRepository
    {
        Task<BitcoinPriceIndex> GetBitcoinPriceIndexAsync();
    }
}
