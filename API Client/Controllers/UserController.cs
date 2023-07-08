using API_Client.Model.DTO;
using API_Client.Model.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Client.Controllers;

[ApiController]
[Route("jwt")]
public class UserController : ControllerBase
{
    private readonly JwtTokenService _jwtTokenService;


    public UserController(JwtTokenService jwtTokenService)
    {
        _jwtTokenService = jwtTokenService;
    }


    [HttpPost("Login")]
    [AllowAnonymous]
    public IActionResult Login(UserLoginDto userLogin)
    {
        var user = UserDbService.Authenticate(userLogin);
        if (user == null)
        {
            return NotFound();
        }

        var token = _jwtTokenService.GenerateToken(user);
        return Ok(token);
    }


    [HttpGet("/admin")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetAllUsers()
    {
        return Ok(UserDbService.GetAllUsers());
    }


    [HttpGet("/test")]
    [Authorize(Roles = "User")]
    public IActionResult GetMessage()
    {
        return Ok("Hello World!");
    }


    [HttpDelete("/delete")]
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteUser(string name)
    {
        if (UserDbService.DeleteUser(name))
        {
            return Ok();
        }
        return NotFound();
    }
}
