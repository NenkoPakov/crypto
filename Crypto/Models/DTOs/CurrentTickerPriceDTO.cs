using Newtonsoft.Json;

namespace Crypto.Models.DTOs
{
    public record CurrentTickerPriceDTO
    {
        public Dictionary<string, decimal> CurrentTickerStatus {  get; set; }
    }
}
