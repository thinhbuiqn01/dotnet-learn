using api.Models;

namespace api.Interface
{
  public interface IProductRepository
  {
    public Task<List<Product>> GetAllProductsAsync(AppUser appUser);

    public Task<Product?> GetProductByIdAsync(Guid id);

    public Task<Product?> UpdateProductAsync(UpdateProductRequestDto updateProductRequestDto);

    public Task<Product?> CreateProductAsync(CreateProductRequestDto createProductRequestDto);

    public Task<bool> DeleteProductAsync(Guid id);
  }
}