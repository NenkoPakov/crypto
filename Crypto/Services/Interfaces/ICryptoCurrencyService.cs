using Crypto.Models.CryptoCurrentcy;
using Crypto.Models.DTOs;

namespace Crypto.Services.Interfaces
{
    public interface ICryptoCurrencyService
    {
        ValueTask<Dictionary<string, TickerDetails>> FilterTickersData(IEnumerable<string> tickers);
        Task<CurrentTickerPriceDTO> GetTickersData(string tickersIds);
    }
}