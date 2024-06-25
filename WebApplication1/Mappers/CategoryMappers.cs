using api.DTOs.Category;
using api.Models;

namespace api.Mappers
{
  public static class CategoryMappers
  {

    public static CategoryDto ToCategoryDto(this Category categoryModel)
    {
      return new CategoryDto
      {
        CategoryName = categoryModel.CategoryName,
        Id = categoryModel.Id,
        Type = categoryModel.Type,
        Products = categoryModel.Products,
      };
    }

    public static CategoryInListDto ToCategoryInListDto(this Category categoryModel)
    {
      return new CategoryInListDto
      {
        CategoryName = categoryModel.CategoryName,
        Id = categoryModel.Id,
        Type = categoryModel.Type,
      };
    }



    public static Category ToCategoryFromCreateDto(this CreateCategoryRequestDto categoryRequestDto)
    {
      return new Category
      {
        CategoryName = categoryRequestDto.CategoryName,
        Type = categoryRequestDto.Type,
        Id = Guid.NewGuid(),
      };
    }

    public static Category ToCategoryUpdateDto(this UpdateCategoryRequestDto updateCategoryRequestDto)
    {
      return new Category
      {
        CategoryName = updateCategoryRequestDto.CategoryName,
        Type = updateCategoryRequestDto.Type,
      };
    }

    public static CategoryDetails ToCategoryDetailsDto(this Category category)
    {
      var products = category.Products;
      var newProduct = new List<ProductCategory> { };
      if (products != null)
      {
        for (int i = 0; i < products.Count; i++)
        {
          newProduct.Add(products[i].ToProductCategoryDto());
        }
      }

      return new CategoryDetails
      {
        CategoryName = category.CategoryName,
        Id = category.Id,
        Portfolios = category.Portfolios,
        Products = newProduct,
        Type = category.Type,
      };
    }
  }
}