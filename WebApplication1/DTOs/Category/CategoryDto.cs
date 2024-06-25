using api.Models;

namespace api.DTOs.Category
{
  public class CategoryDto
  {
    public Guid Id { get; set; }

    public string? CategoryName { get; set; } = null;

    public string? Type { get; set; } = null;

    public List<Product>? Products { get; set; }
  }


  public class CategoryInListDto
  {
    public Guid Id { get; set; }

    public string? CategoryName { get; set; } = null;

    public string? Type { get; set; } = null;

  }
}