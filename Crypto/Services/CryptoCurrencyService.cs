using Crypto.Models.CryptoCurrentcy;
using Crypto.Models.DTOs;
using Crypto.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Crypto.Services
{
    public class CryptoCurrencyService : ICryptoCurrencyService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CryptoCurrencyService> _logger;

        public CryptoCurrencyService(IHttpClientFactory httpClientFactory, ILogger<CryptoCurrencyService> logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _logger = logger;
        }

        public async Task<CurrentTickerPriceDTO> GetTickersData(string tickersIds)
        {
            var response = await _httpClient.GetAsync(BuildUrl(tickersIds));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var currentTickersPrice = JsonConvert.DeserializeObject<IEnumerable<CurrentTickerPrice>>(content);

                return new CurrentTickerPriceDTO()
                {
                    CurrentTickerStatus = currentTickersPrice?.ToDictionary(ctp => ctp.Id, ctp => ctp.CurrentCoinPrice)
                };
            }
            else
            {
                var errorMessage = "An error has occured while deserializing tickers data";
                _logger.LogError(errorMessage);
                throw new Exception(errorMessage);
            }
        }

        public async ValueTask<Dictionary<string, TickerDetails>> FilterTickersData(IEnumerable<string> tickers)
        {
            int startFrom = 0;
            int limit = 100;

            var tickersData = await FetchTickersData(startFrom, limit);

            return tickersData.ToList().Where(td => tickers.Contains(td.Symbol))
                .ToDictionary(tickerDetail => tickerDetail.Symbol, tickerDetail => tickerDetail);
        }

        private string BuildUrl(string tickersIds)
        {
            var ids = tickersIds
                            .Split(',', StringSplitOptions.RemoveEmptyEntries)
                            .Select(int.Parse)
                            .ToList();

            var baseUrl = "https://api.coinlore.net/api/ticker/?id=";
            var apiUrl = $"{baseUrl}{string.Join(',', tickersIds)}";
            return apiUrl;
        }

        private async ValueTask<IEnumerable<TickerDetails>> FetchTickersData(int startFrom, int limit)
        {
            var baseUrl = "https://api.coinlore.net/api/tickers/?start=";
            var apiUrl = $"{baseUrl}{startFrom}&limit={limit}";
            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var logMessage = $"Successful responce from call to {apiUrl}";
                _logger.LogInformation(logMessage);

                var content = await response.Content.ReadAsStringAsync();
                TickerApiResponse tickerApiResponse = JsonConvert.DeserializeObject<TickerApiResponse>(content);

                return tickerApiResponse?.Data == null
                    ? new List<TickerDetails>()
                    : tickerApiResponse.Data;
            }
            else
            {
                var errorMessage = "An error has occured while getting tickers data";
                _logger.LogError(errorMessage);
                throw new Exception(errorMessage);
            }
        }
    }
}
