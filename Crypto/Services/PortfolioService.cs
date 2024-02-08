using Crypto.Models.CryptoCurrentcy;
using Crypto.Models.DTOs;
using Crypto.Models.Portfolio;
using Crypto.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Crypto.Services
{
    public class PortfolioService : IPortfolioService
    {
        private ICryptoCurrencyService _cryptoCurrencyService;
        private readonly ILogger<PortfolioService> _logger;

        public PortfolioService(ICryptoCurrencyService cryptoCurrencyService, ILogger<PortfolioService> logger)
        {
            this._cryptoCurrencyService = cryptoCurrencyService;
            this._logger = logger;
        }

        public async Task<PortfolioDTO> BuildPortfolio(IFile file)
        {
            var fileData = await GetFileData(file);
            Portfolio portfolio = await CalculateChanges(fileData);

            return new PortfolioDTO()
            {
                Assets = portfolio.Assets,
                InitialBalance = portfolio.InitialBalance,
                CurrentBalance = portfolio.CurrentBalance,
            };
        }

        private async ValueTask<Portfolio> CalculateChanges(Dictionary<string, (decimal totalAmount, decimal totalPrice)> fileData)
        {
            int startFrom = 0;
            int limit = 100;

            Portfolio portfolio = new Portfolio();

            while (portfolio.Assets == null || portfolio.Assets.Count != fileData.Count || startFrom % limit == 0)
            {
                var filteredTickersData = await this._cryptoCurrencyService.FilterTickersData(fileData.Keys);

                foreach (var key in filteredTickersData.Keys)
                {
                    CalculateTickerChanges(fileData[key], portfolio, filteredTickersData[key]);
                }

                startFrom += filteredTickersData.Count;
            }

            return portfolio;
        }

        private static void CalculateTickerChanges( (decimal totalAmount, decimal totalPrice) tickerFileData, Portfolio portfolio, TickerDetails filteredTickersData)
        {
            var id = filteredTickersData.Id;
            var currentPrice = filteredTickersData.PriceUsd;
            var newTotalPrice = tickerFileData.totalAmount * currentPrice;

            portfolio.InitialBalance += tickerFileData.totalPrice;
            portfolio.CurrentBalance += newTotalPrice;
            portfolio.Assets.Add(id, new AssetOverview()
            {
                Ticker = filteredTickersData.Symbol,
                Amount = tickerFileData.totalAmount,
                TotalInitialPrice = tickerFileData.totalPrice,
                CurrentCoinPrice = currentPrice,
            });
        }

        private async ValueTask<Dictionary<string, (decimal totalAmount, decimal totalPrice)>> GetFileData(IFile file)
        {
            try
            {
                using (var stream = file.FormFile.OpenReadStream())
                using (var reader = new StreamReader(stream))
                {
                    string content = await reader.ReadToEndAsync();

                    string[] lines = content.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                    return ParseAssets(lines);
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
                throw;
            }
        }

        private Dictionary<string, (decimal totalAmount, decimal totalPrice)> ParseAssets(string[] lines)
        {
            Dictionary<string, (decimal totalAmount, decimal totalPrice)> assetsDictionary = new Dictionary<string, (decimal totalAmount, decimal totalPrice)>();

            foreach (var line in lines)
            {
                var values = line.Split('|');

                if (values.Length == 3)
                {
                    var asset = new Asset
                    {
                        Ticker = values[1],
                        Amount = decimal.Parse(values[0]),
                        InitialPrice = decimal.Parse(values[2])
                    };

                    if (!assetsDictionary.ContainsKey(asset.Ticker))
                    {
                        assetsDictionary[asset.Ticker] = (0, 0);
                    }

                    var assetItem = assetsDictionary[asset.Ticker];
                    assetsDictionary[asset.Ticker] = (assetItem.totalAmount + asset.Amount, assetItem.totalPrice + (asset.Amount * asset.InitialPrice));
                }
            }


            var logMessage = "Successfully parsed file";
            this._logger.LogInformation(logMessage);

            return assetsDictionary;
        }
    }
}
