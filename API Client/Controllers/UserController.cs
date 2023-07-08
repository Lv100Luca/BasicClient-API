using API_Client.Model;
using API_Client.Model.DTO;
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
        var user = UserDb.Authenticate(userLogin);
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
        return Ok(UserDb.GetAllUsers());
    }


    [HttpGet("/test")]
    [Authorize(Roles = "User")]
    public IActionResult GetMessage()
    {
        return Ok("Hello World!");
    }
}
