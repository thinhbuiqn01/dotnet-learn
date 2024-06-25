namespace api.Models
{
  public class CategoryDetails
  {
    public Guid Id { get; set; }

    public string? CategoryName { get; set; } = null;

    public string? Type { get; set; } = null;

    public List<ProductCategory>? Products { get; set; } = [];

    public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();

  }
}