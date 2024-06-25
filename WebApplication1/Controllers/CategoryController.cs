
using api.DTOs.Category;
using api.Helpers;
using api.Interface;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller
{

  [Route("api/category")]
  [ApiController]

  public class CategoryController : ControllerBase
  {
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(ICategoryRepository categoryRepository)
    {
      _categoryRepository = categoryRepository;
    }

    [HttpGet("{PageIndex}/{PageSize}")]
    [Authorize]
    public async Task<IActionResult> GetAll([FromRoute] int PageIndex, [FromRoute] int PageSize, [FromQuery] string SearchKey = "")
    {
      try
      {


        var queryParams = new QueryParams
        {
          PageIndex = PageIndex,
          PageSize = PageSize,
          SearchKey = SearchKey,
        };

        var categoryPaginate = await _categoryRepository.GetCategoriesAsync(queryParams);

        var items = categoryPaginate.Items.Select(c => c.ToCategoryInListDto()).ToList();
        if (categoryPaginate == null)
        {

          var responseError = new ResponseError<string>
          {
            Error = "Cannot get categories"
          };
          return BadRequest(responseError);

        }

        var result = new Paginate<CategoryInListDto>
        {

          Items = items,
          PageIndex = categoryPaginate.PageIndex,
          PageSize = categoryPaginate.PageSize,
          TotalCount = categoryPaginate.TotalCount,
        };

        var responseSuccess = new ResponseSuccess<Paginate<CategoryInListDto>>
        {
          Data = result,
        };
        return Ok(responseSuccess);
      }
      catch (Exception e)
      {
        var responseError = new ResponseError<string>
        {
          Error = e.Message,
        };
        return BadRequest(responseError);
      }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
      try
      {

        var category = await _categoryRepository.getCategoryByIdAsync(id);

        if (category == null)
        {

          var responseError = new ResponseError<string>
          {
            Error = "Cannot found category",
          };
          return BadRequest(responseError);
        }

        var ResponseSuccess = new ResponseSuccess<CategoryDetails>
        {
          Data = category.ToCategoryDetailsDto()
        };

        return Ok(ResponseSuccess);

      }
      catch (Exception e)
      {
        var responseError = new ResponseError<string>
        {
          Error = e.Message,
        };
        return BadRequest(responseError);
      }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequestDto categoryDto)
    {
      try
      {
        var category = await _categoryRepository.CreateCategoryAsync(categoryDto);
        var ResponseSuccess = new ResponseSuccess<Category>
        {
          Data = category
        };

        return Ok(ResponseSuccess);

      }
      catch (Exception e)
      {
        var responseError = new ResponseError<string>
        {
          Error = e.Message,
        };
        return BadRequest(responseError);
      }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] UpdateCategoryRequestDto categoryDto, [FromRoute] Guid id)
    {
      try
      {
        var category = await _categoryRepository.UpdateCategoryAsync(categoryDto, id);

        if (category != null)
        {
          var ResponseSuccess = new ResponseSuccess<Category>
          {
            Data = category
          };

          return Ok(ResponseSuccess);
        }
        var responseError = new ResponseError<string>
        {
          Error = "Cannot find category",
        };
        return BadRequest(responseError);

      }
      catch (Exception e)
      {
        var responseError = new ResponseError<string>
        {
          Error = e.Message,
        };
        return BadRequest(responseError);
      }
    }

    /// <summary>
    /// Deletes a category by its ID.
    /// </summary>
    /// <param name="id">The ID of the category to delete.</param>
    /// <returns>An IActionResult containing a ResponseSuccess<bool> if the deletion was successful, or a ResponseError<string> if the deletion failed.</returns>
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
      try
      {

        var isDelete = await _categoryRepository.DeleteCategoryAsync(id);

        if (isDelete)
        {
          var ResponseSuccess = new ResponseSuccess<bool>
          {
            Data = true
          };

          return Ok(ResponseSuccess);
        }
        var ResponseError = new ResponseError<string>
        {
          Error = "Cannot delete this category"
        };

        return BadRequest(ResponseError);

      }
      catch (Exception e)
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