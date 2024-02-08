using Crypto.Models.Portfolio;

namespace Crypto.Models.DTOs
{
    public record PortfolioDTO
    {
        public Dictionary<string, AssetOverview> Assets { get; init; } = new Dictionary<string, AssetOverview>(); 
        public decimal InitialBalance { get; set; }
        public decimal CurrentBalance { get; set; }
    }
}
