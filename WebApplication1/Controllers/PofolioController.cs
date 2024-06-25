using api.Extension;
using api.Interface;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller
{

  [Route("api/portfolios")]
  [ApiController]
  public class PortfolioController : ControllerBase
  {

    private readonly UserManager<AppUser> _userManager;
    private readonly ICategoryRepository _categoryRepository;

    private readonly IPortfolioRepository _portfolioRepository;

    public PortfolioController(UserManager<AppUser> userManager, ICategoryRepository categoryRepository,
    IPortfolioRepository portfolioRepository)
    {
      _userManager = userManager;
      _categoryRepository = categoryRepository;
      _portfolioRepository = portfolioRepository;
    }


    [HttpGet]
    [Authorize]
    public async Task<IActionResult> getUserPortfolios()
    {

      var userName = User.GetUserName();
      if (userName == null)
      {
        return Unauthorized();
      }

      var appUser = await _userManager.FindByNameAsync(userName);
      // return Ok(appUser);

      if (appUser != null)
      {
        var portfolios = await _portfolioRepository.GetUserPortfolio(appUser);
        return Ok(portfolios);
      }
      return BadRequest();
    }


    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] PortfolioRequestCreateDto portfolioRequestCreateDto)
    {
      var userName = User.GetUserName();
      if (userName == null)
      {
        return Unauthorized();
      }

      if (!ModelState.IsValid)
      {
        return BadRequest("CategoryId is required");
      }

      var appUser = await _userManager.FindByNameAsync(userName);

      if (appUser == null)
      {
        return Unauthorized();
      }


      var portfolioFind = await _portfolioRepository.GetPortfolioById(portfolioRequestCreateDto.CategoryId, appUser);
      if (portfolioFind != null)
      {
        return StatusCode(500, "Portfolio Already exist");
      }

      var category = await _categoryRepository.getCategoryByIdAsync(portfolioRequestCreateDto.CategoryId);


      var portfolioModel = new Portfolio
      {
        CategoryId = portfolioRequestCreateDto.CategoryId,
        AppUserId = appUser.Id,
        category = category,
      };



      var portfolio = await _portfolioRepository.CreatePortfolio(portfolioModel);

      if (portfolio == null)
      {
        return BadRequest("Cannot create portfolio");
      }

      return StatusCode(200, portfolio);
    }


    [HttpDelete("{CategoryId}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] Guid CategoryId)
    {
      var userName = User.GetUserName();


      if (!ModelState.IsValid)
      {
        return BadRequest("CategoryId is required");
      }

      var appUser = await _userManager.FindByNameAsync(userName);

      if (appUser == null)
      {
        return Unauthorized();
      }


      var portfolioFind = await _portfolioRepository.GetPortfolioById(CategoryId, appUser);


      if (portfolioFind == null)
      {
        return NotFound("Portfolio Not Found");
      }

      var isDelete = await _portfolioRepository.DeletePortfolio(portfolioFind);

      if (isDelete == true)
      {
        return StatusCode(200, "Portfolio has deleted successfully");
      }

      return NotFound("Cannot delete portfolio");
    }
  }
}