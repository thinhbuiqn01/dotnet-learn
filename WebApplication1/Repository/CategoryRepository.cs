using api.Data;
using api.Helpers;
using api.Interface;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace api.Repository
{
  public class CategoryRepository : ICategoryRepository
  {

    private readonly ApplicationDBContext _context;

    public CategoryRepository(ApplicationDBContext context)
    {
      _context = context;
    }

    public async Task<Category> CreateCategoryAsync(CreateCategoryRequestDto createCategoryRequestDto)
    {
      var category = createCategoryRequestDto.ToCategoryFromCreateDto();

      await _context.Categories.AddAsync(category);
      await _context.SaveChangesAsync();

      return category;
    }

    public async Task<bool> DeleteCategoryAsync(Guid id)
    {
      var category = await getCategoryByIdAsync(id);

      if (category != null)
      {
        _context.Remove(category);
        await _context.SaveChangesAsync();
        return true;
      }

      return false;
    }

    public async Task<Paginate<Category>> GetCategoriesAsync(QueryParams queryParams)
    {

      var categories = await _context.Categories
                .Skip(queryParams.PageIndex * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .Include(c => c.Products).ToListAsync();

      var categoriesDto = categories.Select(c => c.ToCategoryInListDto());
      return new Paginate<Category>
      {
        Items = categories,
        PageIndex = queryParams.PageIndex,
        PageSize = queryParams.PageSize,
        TotalCount = categories.Count(),
      };
    }

    public async Task<Category?> getCategoryByIdAsync(Guid id)
    {
      return await _context.Categories.Include(c => c.Products)
                          .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Category?> UpdateCategoryAsync(UpdateCategoryRequestDto updateCategoryRequestDto, Guid id)
    {

      var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

      if (category == null)
      {
        return null;
      }

      category.CategoryName = updateCategoryRequestDto.CategoryName;
      category.Type = updateCategoryRequestDto.Type;

      // category = updateCategoryRequestDto.ToCategoryUpdateDto();
      _context.Update(category);
      await _context.SaveChangesAsync();

      return category;
    }

    public async Task<bool?> CategoryExits(Guid id)
    {
      var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
      if (category != null)
      {
        return true;

      }
      return null;
    }
  }
}