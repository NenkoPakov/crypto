using Newtonsoft.Json;

namespace Crypto.Models.CryptoCurrentcy
{
    public class CurrentTickerPrice
    {
        public string Id { get; set; }
        [JsonProperty("price_usd")]
        public decimal CurrentCoinPrice { get; set; }
    }
}
