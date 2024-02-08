using Crypto.Models.DTOs;

namespace Crypto.Services.Interfaces
{
    public interface IPortfolioService
    {
        Task<PortfolioDTO> BuildPortfolio(IFile file);
    }
}