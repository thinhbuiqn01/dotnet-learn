using System.ComponentModel.DataAnnotations;

namespace api.Models
{
  public class CreateCategoryRequestDto
  {
    [Required]
    public string CategoryName { get; set; }

    [Required]
    public string Type { get; set; }
  }

  public class UpdateCategoryRequestDto
  {
    [Required]
    public string CategoryName { get; set; }

    [Required]
    public string Type { get; set; }

    [Required]
    public Guid Id { get; set; }
  }

}