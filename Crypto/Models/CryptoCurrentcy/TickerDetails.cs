using Newtonsoft.Json;

namespace Crypto.Models.CryptoCurrentcy
{
    public class TickerDetails
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string NameId { get; set; }
        public int Rank { get; set; }
        [JsonProperty("price_usd")]
        public decimal PriceUsd { get; set; }
    }
}
