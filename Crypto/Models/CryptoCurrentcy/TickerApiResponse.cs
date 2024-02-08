using Newtonsoft.Json;

namespace Crypto.Models.CryptoCurrentcy
{
    public class TickerApiResponse
    {
        [JsonProperty("data")]
        public TickerDetails[] Data { get; set; }
        [JsonProperty("info")]
        public TickerInfo Info { get; set; }
    }
}
