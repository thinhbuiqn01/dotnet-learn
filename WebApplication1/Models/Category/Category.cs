using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
  [Table("Categories")]
  public class Category
  {
    public Guid Id { get; set; }

    public string? CategoryName { get; set; } = null;

    public string? Type { get; set; } = null;

    public List<Product>? Products { get; set; } = [];

    public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();

  }

}