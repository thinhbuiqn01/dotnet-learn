using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace api.DTOs
{
  public class RegisterDto
  {

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }

    [Required]
    public string? UserName { get; set; }
  }


  public class RegisterSuccessDto
  {
    public string Email { get; set; } = "";

    public string UserName { get; set; } = "";
    public string Role { get; set; } = "USER";

    public string AccessToken { get; set; }

  }
}