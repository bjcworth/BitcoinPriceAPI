using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;
using BitcoinPriceAPI.Model;
using System.Threading.Tasks;

namespace BitcoinPriceAPI
{
    public class BitcoinPriceIndexRepository : IBitcoinPriceIndexRepository
    {
        private static BitcoinPriceIndex Prices;
        private readonly IConfiguration Configuration;


        public BitcoinPriceIndexRepository(IConfiguration configuration)
        {
            Prices = new BitcoinPriceIndex { };
            Configuration = configuration;
        }

        public async Task<BitcoinPriceIndex> GetBitcoinPriceIndexAsync()
        {
            HttpClient client = new HttpClient();
            string url = Configuration["CoinDeskApiUrl"];

            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                using HttpContent content = response.Content;

                string pricesString = await content.ReadAsStringAsync();

                JObject pricesJson = JObject.Parse(pricesString);

                Prices.USD = pricesJson["bpi"]["USD"]["rate"].ToString();
                Prices.GBP = pricesJson["bpi"]["GBP"]["rate"].ToString();
                Prices.EUR = pricesJson["bpi"]["EUR"]["rate"].ToString();
                Prices.LastUpdated = pricesJson["time"]["updated"].ToString();
                Prices.Disclaimer = pricesJson["disclaimer"].ToString();
            }

            return Prices;
        }
    }
}
