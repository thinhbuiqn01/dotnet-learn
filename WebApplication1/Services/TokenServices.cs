using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.Interface;
using api.Models;
using Microsoft.IdentityModel.Tokens;

namespace api.Services
{
  public class TokenServices : ITokenServices
  {
    private readonly IConfiguration _configuration;

    private readonly SymmetricSecurityKey _key;

    public TokenServices(IConfiguration configuration)
    {
      _configuration = configuration;
      _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]));

    }
    public string CreateToken(AppUser appUser)
    {
      var claims = new List<Claim> {
         new Claim (JwtRegisteredClaimNames.Email, appUser.Email),
         new Claim (JwtRegisteredClaimNames.GivenName, appUser.UserName),
       };

      var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        Audience = _configuration["JWT:Audience"],
        Issuer = _configuration["JWT:Insurer"],
        SigningCredentials = creds,
        Expires = DateTime.Now.AddDays(7),
      };

      var tokenHandler = new JwtSecurityTokenHandler();
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }
  }
}