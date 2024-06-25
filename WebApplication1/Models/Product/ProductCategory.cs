namespace api.Models
{
  public class ProductCategory
  {
    public Guid Id { get; set; }

    public string? Name { get; set; } = null;

    public float? Price { get; set; } = 0;

    public string AppUserId { get; set; }

  }
}