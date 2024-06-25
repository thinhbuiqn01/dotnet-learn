
using api.Data;
using api.Interface;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Extension;
using api.DTOs;

namespace api.Controller
{
  [Route("api/product")]
  [ApiController]
  public class ProductController : ControllerBase
  {
    private readonly IProductRepository _productRepository;

    private readonly ApplicationDBContext _context;

    private readonly UserManager<AppUser> _userManager;

    public ProductController(IProductRepository productRepository, ApplicationDBContext context, UserManager<AppUser> userManager)
    {
      _productRepository = productRepository;
      _context = context;
      _userManager = userManager;
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
      try
      {
        var product = await _productRepository.GetProductByIdAsync(id);
        if (product == null)
        {
          var responseError = new ResponseError<string>
          {
            Data = "Cannot fount Product",
          };
          return BadRequest(responseError);
        }

        product.ToProductDto();
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == product.CategoryId);
        product.Category = category;

        var responseSuccess = new ResponseSuccess<Product>
        {
          Data = product,
        };
        return Ok(responseSuccess);
      }
      catch (System.Exception e)
      {
        var responseError = new ResponseError<string>
        {
          Error = e.Message,
        };
        return BadRequest(responseError);
      }

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
      try
      {
        var isDelete = await _productRepository.DeleteProductAsync(id);
        if (!isDelete)
        {
          var responseError = new ResponseError<string>
          {
            Data = "Cannot fount Product",
          };
          return BadRequest(responseError);

        }

        var responseSuccess = new ResponseSuccess<bool>
        {
          Data = true,
        };
        return Ok(responseSuccess);
      }
      catch (System.Exception e)
      {
        var responseError = new ResponseError<string>
        {
          Error = e.Message,
        };
        return BadRequest(responseError);
      }
    }

    [HttpGet]
    [Authorize]

    public async Task<IActionResult> GetAll()
    {
      try
      {
        var userName = User.GetUserName();
        var appUser = await _userManager.FindByNameAsync(userName);

        if (appUser == null)
        {
          return Unauthorized();
        }

        var products = await _productRepository.GetAllProductsAsync(appUser);
        var productsDto = products.Select(x => x.ToProductDto()).ToList();

        var responseSuccess = new ResponseSuccess<List<ProductDto>>
        {
          Data = productsDto,
        };
        return Ok(responseSuccess);

      }
      catch (System.Exception e)
      {
        var responseError = new ResponseError<string>
        {
          Error = e.Message,
        };
        return BadRequest(responseError);
      }
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Update([FromBody] UpdateProductRequestDto dto)
    {

      try
      {
        var userName = User.GetUserName();
        var appUser = await _userManager.FindByNameAsync(userName);

        if (appUser == null)
        {
          return Unauthorized();
        }

        var product = await _productRepository.UpdateProductAsync(new UpdateProductRequestDto
        {
          AppUserId = appUser.Id,
          CategoryId = dto.CategoryId,
          CategoryType = dto.CategoryType,
          Id = dto.Id,
          Name = dto.Name,
          Price = dto.Price,
        });
        if (product == null)
        {
          var responseError = new ResponseError<string>
          {
            Data = "Cannot fount Product",
          };
          return BadRequest(responseError);
        }

        var responseSuccess = new ResponseSuccess<ProductDto>
        {
          Data = product.ToProductDto(),
        };
        return Ok(responseSuccess);
      }
      catch (System.Exception e)
      {
        var responseError = new ResponseError<string>
        {
          Error = e.Message,
        };
        return BadRequest(responseError);
      }

    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateProductRequestDto dto)
    {

      try
      {
        var userName = User.GetUserName();
        var appUser = await _userManager.FindByNameAsync(userName);

        if (appUser == null)
        {
          return Unauthorized();
        }

        var product = await _productRepository.CreateProductAsync(new CreateProductRequestDto
        {
          AppUserId = appUser.Id,
          CategoryId = dto.CategoryId,
          CategoryType = dto.CategoryType,
          Name = dto.Name,
          Price = dto.Price,
        });
        if (product == null)
        {
          var responseError = new ResponseError<string>
          {
            Data = "Cannot fount Product",
          };
          return BadRequest(responseError);
        }

        var responseSuccess = new ResponseSuccess<ProductDto>
        {
          Data = product.ToProductDto(),
        };
        return Ok(responseSuccess);
      }
      catch (System.Exception e)
      {
        var responseError = new ResponseError<string>
        {
          Error = e.Message,
        };
        return BadRequest(responseError);
      }
    }
  }
}