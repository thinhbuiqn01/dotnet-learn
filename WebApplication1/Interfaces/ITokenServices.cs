using api.Models;

namespace api.Interface
{
  public interface ITokenServices
  {
    public string CreateToken(AppUser appUser);
  }
}