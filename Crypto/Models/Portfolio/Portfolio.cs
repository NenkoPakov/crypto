namespace Crypto.Models.Portfolio
{
    public class Portfolio
    {
        //public List<AssetOverview> Assets { get; init; } = new List<AssetOverview>();
        public Dictionary<string, AssetOverview> Assets { get; init; } = new Dictionary<string, AssetOverview>();
        public decimal InitialBalance { get; set; }
        public decimal CurrentBalance { get; set; }
    }
}
