using API_Client.Model.DTO;
using API_Client.Model.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Client.Controllers;

[ApiController]
[Route("users")]
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


    [HttpGet]
    // [Authorize(Roles = "Admin")]
    public IActionResult GetAllUsers()
    {
        return Ok(UserDbService.GetAllUsers());
    }


    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult AddUser(User newUser)
    {
        if (UserDbService.AddUser(newUser))
        {
            // return CreatedAtAction(nameof(AddUser), // ask -> wtf is this?
            //     new
            //     {
            //         name = newUser.Username,
            //     },
            //     newUser);
            return Created("Created user", newUser);
        }
        return Conflict("User already exists");
    }


    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteUser(string name)
    {
        if (UserDbService.DeleteUser(name))
        {
            return Ok();
        }
        return NotFound();
    }


    [HttpGet("me")]
    [Authorize]
    public IActionResult Me() // ask -> Better way of extracting username from token
    {
        var username = _jwtTokenService.GetUsernameFromToken(GetTokenFromHeader(Request.Headers));
        User? user = UserDbService.GetUserByUsername(username);
        if (user == null)
        {
            return NotFound($"no User with {username} found");
        }
        return Ok(new
        {
            username = user.Username,
            role = user.Role,
        }); // return anonymous object of user that doesnt include password
    }


    private string GetTokenFromHeader(IHeaderDictionary headers)
    {
        return Request.Headers["Authorization"].ToString().Split(" ").Last();
    }
}
