
using api.DTOs.Category;
using api.Models;


namespace api.DTOs
{
  public class ProductDto
  {
    public Guid Id { get; set; }

    public string? Name { get; set; } = null;

    public float? Price { get; set; } = 0;

    public string? CategoryType { get; set; } = null;

    public Guid CategoryId { get; set; }
    public string AppUserId { get; set; }


  }

}