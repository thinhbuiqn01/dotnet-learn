using System.ComponentModel.DataAnnotations.Schema;
using api.DTOs;
using api.DTOs.Category;

namespace api.Models
{
  [Table("Products")]
  public class Product
  {
    public Guid Id { get; set; }

    public string? Name { get; set; } = null;

    public float? Price { get; set; } = 0;

    public string? CategoryType { get; set; } = null;

    public Guid CategoryId { get; set; }

    public Category? Category { get; set; }

    public string AppUserId { get; set; }

    public AppUser AppUser { get; set; }
  }
}