using Crypto.Models.DTOs;
using Crypto.Models.Portfolio;
using Crypto.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
public class PortfolioController : ControllerBase
{
    private IPortfolioService _portfolioService;
    private readonly ILogger<PortfolioController> _logger;

    public PortfolioController(IPortfolioService portfolioService, ILogger<PortfolioController> logger)
    {
        this._portfolioService = portfolioService;
        this._logger = logger;
    }

    [HttpPost("upload")]
    public async Task<ActionResult<PortfolioDTO>> Post([FromForm] Crypto.Models.Portfolio.File file)
    {
        this._logger.LogInformation("Beginning of portfolio/upload endpoint");

        if (file == null || file.FormFile.Length == 0)
        {
            var errorMessage = "Invalid file";
            this._logger.LogError(errorMessage);
            return BadRequest(errorMessage);
        }

        try
        {
            return Ok(await this._portfolioService.BuildPortfolio(file));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error reading file: {ex.Message}");
        }
    }
}