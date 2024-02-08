using Newtonsoft.Json;

namespace Crypto.Models.CryptoCurrentcy
{
    public class TickerDetails
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        [JsonProperty("price_usd")]
        public decimal PriceUsd { get; set; }
    }
}
