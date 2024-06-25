using api.DTOs.Category;
using api.Helpers;
using api.Models;

namespace api.Interface
{
  public interface ICategoryRepository
  {
    public Task<Paginate<Category>> GetCategoriesAsync(QueryParams queryParams);

    public Task<Category?> getCategoryByIdAsync(Guid id);

    public Task<Category?> UpdateCategoryAsync(UpdateCategoryRequestDto updateCategoryRequestDto, Guid id);

    public Task<bool> DeleteCategoryAsync(Guid id);

    public Task<Category> CreateCategoryAsync(CreateCategoryRequestDto createCategoryRequestDto);

    public Task<bool?> CategoryExits(Guid id);


  }
}