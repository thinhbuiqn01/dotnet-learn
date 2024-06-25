using System.ComponentModel.DataAnnotations;

namespace api.Models
{
  public class CreateProductRequestDto
  {

    [Required]
    public string Name { get; set; }

    [Required]
    public float Price { get; set; }


    [Required]
    public string CategoryType { get; set; }

    [Required]
    public Guid CategoryId { get; set; }

    public string? AppUserId { get; set; }

  }


  public class UpdateProductRequestDto
  {
    [Required]
    public string Name { get; set; }

    [Required]
    public float Price { get; set; }

    [Required]
    public string CategoryType { get; set; }

    [Required]
    public Guid Id { get; set; }

    [Required]
    public Guid CategoryId { get; set; }

    public string? AppUserId { get; set; } = null;
  }
}