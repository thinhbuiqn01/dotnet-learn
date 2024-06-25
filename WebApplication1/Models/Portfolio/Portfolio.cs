using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{

  [Table("Portfolios")]
  public class Portfolio
  {
    public string AppUserId { get; set; }

    public Guid CategoryId { get; set; }

    public AppUser AppUser { get; set; }

    public Category category { get; set; }
  }
}