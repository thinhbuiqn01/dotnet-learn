using api.DTOs;
using api.Models;

namespace api.Mappers
{
  public static class ProductMappers
  {

    public static ProductDto ToProductDto(this Product product)
    {
      return new ProductDto
      {
        CategoryType = product.CategoryType,
        Id = product.Id,
        Name = product.Name,
        Price = product.Price,
        CategoryId = product.CategoryId,
        AppUserId = product.AppUserId,
      };
    }

    public static Product ToCreateRequestDto(this CreateProductRequestDto product)
    {
      return new Product
      {
        CategoryType = product.CategoryType,
        Name = product.Name,
        Price = product.Price,
        Id = Guid.NewGuid(),
        CategoryId = product.CategoryId,
        AppUserId = product.AppUserId,
      };
    }

    public static Product ToUpdateRequestDto(this UpdateProductRequestDto product)
    {
      return new Product
      {
        CategoryType = product.CategoryType,
        Name = product.Name,
        Price = product.Price,
        Id = product.Id,
        CategoryId = product.CategoryId,
        AppUserId = product.AppUserId,
      };
    }


    public static ProductCategory ToProductCategoryDto(this Product product)
    {
      return new ProductCategory
      {
        Name = product.Name,
        Price = product.Price,
        Id = product.Id,
        AppUserId = product.AppUserId,
      };
    }
  }
}
