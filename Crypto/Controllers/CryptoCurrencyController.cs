using Crypto.Models.DTOs;
using Crypto.Services;
using Crypto.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Crypto.Controllers
{
    [Route("crypto-currency")]
    [ApiController]
    public class CryptoCurrencyController : ControllerBase
    {
        private readonly ICryptoCurrencyService _cryptoCurrencyService;
        private readonly ILogger<CryptoCurrencyController> _logger;

        public CryptoCurrencyController(ICryptoCurrencyService cryptoCurrencyService, ILogger<CryptoCurrencyController> logger)
        {
            _cryptoCurrencyService = cryptoCurrencyService;
            _logger = logger;
        }

        [HttpGet("refresh")]
        public async Task<ActionResult<CurrentTickerPriceDTO>> Refresh([FromQuery] string tickersIds)
        {
            this._logger.LogInformation("Beginning of crypto-currency/refresh edpoint");

            try
            {
                return Ok(await _cryptoCurrencyService.GetTickersData(tickersIds));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error reading file: {ex.Message}");
            }
        }
    }
}
