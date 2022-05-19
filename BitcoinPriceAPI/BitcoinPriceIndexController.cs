using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using BitcoinPriceAPI.Model;

namespace BitcoinPriceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BitcoinPriceIndexController : ControllerBase
    {
        private IBitcoinPriceIndexRepository bitcoinPriceIndexRepository;

        public BitcoinPriceIndexController(IConfiguration configuration)
        {
            this.bitcoinPriceIndexRepository = new BitcoinPriceIndexRepository(configuration);
        }

        [HttpGet]
        public async Task<BitcoinPriceIndex> GetPricesAsync()
        {
            return await bitcoinPriceIndexRepository.GetBitcoinPriceIndexAsync();
        }
    }
}
