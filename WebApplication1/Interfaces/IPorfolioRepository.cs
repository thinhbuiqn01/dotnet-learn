using api.Models;

namespace api.Interface
{
  public interface IPortfolioRepository
  {

    public Task<List<Category>> GetUserPortfolio(AppUser appUser);

    public Task<Portfolio> CreatePortfolio(Portfolio portfolio);

    public Task<Portfolio?> GetPortfolioById(Guid CategoryId, AppUser appUser);

    public Task<bool> DeletePortfolio(Portfolio portfolio);

  }
}