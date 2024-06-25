using api.Data;
using api.Interface;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
  public class PortfolioRepository : IPortfolioRepository
  {

    private readonly ApplicationDBContext _context;

    private readonly ICategoryRepository _categoryRepository;

    private readonly UserManager<AppUser> _userManager;


    public PortfolioRepository(
      ApplicationDBContext context,
      ICategoryRepository categoryRepository,
      UserManager<AppUser> userManager)
    {
      _context = context;
      _categoryRepository = categoryRepository;
      _userManager = userManager;
    }

    public async Task<Portfolio> CreatePortfolio(Portfolio portfolioRequestCreateDto)
    {
      await _context.AddAsync(portfolioRequestCreateDto);
      await _context.SaveChangesAsync();

      return portfolioRequestCreateDto;
    }

    public async Task<bool> DeletePortfolio(Portfolio portfolio)
    {

      _context.Remove(portfolio);
      await _context.SaveChangesAsync();
      return true;
    }

    public async Task<Portfolio?> GetPortfolioById(Guid CategoryId, AppUser appUser)
    {
      var portfolio = await _context.Portfolios.FirstOrDefaultAsync(p => p.CategoryId == CategoryId && p.AppUserId == appUser.Id);
      return portfolio;
    }

    public async Task<List<Category>> GetUserPortfolio(AppUser appUser)
    {
      return await _context.Portfolios.Where(p => p.AppUserId == appUser.Id)
      .Select(p => new Category
      {
        CategoryName = p.category.CategoryName,
        Id = p.category.Id,
        Products = p.category.Products,
        Type = p.category.Type,
        Portfolios = p.category.Portfolios,
      }).ToListAsync();
    }
  }

}