namespace Crypto.Models.Portfolio
{
    public class AssetOverview
    {
        public string Ticker { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalInitialPrice { get; set; }
        public decimal CurrentCoinPrice { get; set; }

    }
}
