using api.Data;
using api.Interface;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace api.Repository
{
  public class ProductRepository : IProductRepository
  {

    private readonly ApplicationDBContext _context;

    private readonly ICategoryRepository _categoryRepository;

    public ProductRepository(ApplicationDBContext context, ICategoryRepository categoryRepository)
    {
      _context = context;
      _categoryRepository = categoryRepository;
    }

    public async Task<Product?> CreateProductAsync(CreateProductRequestDto createProductRequestDto)
    {

      var category = await _categoryRepository.CategoryExits(createProductRequestDto.CategoryId);

      if (category == null)
      {
        return null;
      }

      var product = createProductRequestDto.ToCreateRequestDto();

      await _context.AddAsync(product);
      await _context.SaveChangesAsync();

      return product;
    }

    public async Task<bool> DeleteProductAsync(Guid id)
    {
      var product = await GetProductByIdAsync(id);

      if (product == null)
      {
        return false;
      }
      _context.Remove(product);
      await _context.SaveChangesAsync();

      return true;

    }

    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
      var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
      if (product == null)
      {
        return null;
      }

      return product;
    }

    public async Task<List<Product>> GetAllProductsAsync(AppUser appUser)
    {
      var products = await _context.Products.Where(p => p.AppUserId == appUser.Id).ToListAsync();
      return products;
    }

    public async Task<Product?> UpdateProductAsync(UpdateProductRequestDto updateProductRequestDto)
    {
      var categoryExist = await _categoryRepository.CategoryExits(updateProductRequestDto.CategoryId);

      if (categoryExist == null)
      {
        return null;
      }


      var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == updateProductRequestDto.Id);

      if (product == null || product.AppUserId != updateProductRequestDto.AppUserId)
      {
        return null;
      }

      product.Name = updateProductRequestDto.Name;
      product.Price = updateProductRequestDto.Price;
      product.CategoryId = updateProductRequestDto.CategoryId;
      product.CategoryType = updateProductRequestDto.CategoryType;

      _context.Update(product);
      await _context.SaveChangesAsync();
      return product;
    }
  }
}