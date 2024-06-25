using System.ComponentModel.DataAnnotations;

namespace api.Models
{

  public class PortfolioRequestCreateDto
  {

    [Required]
    public Guid CategoryId { get; set; }

    public string? UserId { get; set; }
  }
}