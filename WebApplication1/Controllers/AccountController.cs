using api.DTOs;
using api.Interface;
using api.Mappers;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controller
{

  [Route("api/user")]
  [ApiController]
  public class AccountController : ControllerBase
  {
    private readonly UserManager<AppUser> _userManager;

    private readonly ITokenServices _tokenServices;

    public readonly SignInManager<AppUser> _signInManager;

    public AccountController(UserManager<AppUser> userManager, ITokenServices tokenServices, SignInManager<AppUser> signInManager)
    {
      _userManager = userManager;
      _tokenServices = tokenServices;
      _signInManager = signInManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
      try
      {

        if (!ModelState.IsValid)
          return BadRequest(ModelState);

        var appUser = new AppUser
        {
          Email = registerDto.Email,
          UserName = registerDto.UserName,
        };

        var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

        if (createdUser.Succeeded)
        {
          var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
          if (roleResult.Succeeded)
          {
            var token = _tokenServices.CreateToken(appUser);
            return Ok(appUser.ToRegisterSuccessDto(token));
          }
          else
          {
            return StatusCode(500, roleResult.Errors);
          }
        }
        else
        {
          return StatusCode(500, createdUser.Errors);
        }

      }
      catch (Exception ex)
      {
        return StatusCode(500, ex);
      }
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {

      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.UserName);

      if (user == null)
      {
        return Unauthorized("Username Invalid");
      }

      var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

      if (!result.Succeeded) return Unauthorized("Username not found and/or password invalid");

      var userLogged = new RegisterSuccessDto
      {
        AccessToken = _tokenServices.CreateToken(user),
        Email = user.Email,
        UserName = user.UserName,
        Role = "USER"
      };

      return Ok(userLogged);
    }

  }
}