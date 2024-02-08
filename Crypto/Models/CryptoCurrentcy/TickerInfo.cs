using Newtonsoft.Json;

namespace Crypto.Models.CryptoCurrentcy
{
    public class TickerInfo
    {
        [JsonProperty("coins_num")]
        public int CoinsNum { get; set; }
        [JsonProperty("time")]
        public string Time { get; set; }
    }
}
