using api.DTOs;
using api.Models;

namespace api.Mappers
{
  public static class UserMappers
  {

    public static RegisterSuccessDto ToRegisterSuccessDto(this AppUser appUser, string token)
    {
      return new RegisterSuccessDto
      {

        Email = appUser.Email ?? "",
        Role = "USER",
        UserName = appUser.UserName ?? "",
        AccessToken = token
      };
    }
  }
}