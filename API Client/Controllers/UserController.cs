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
    [Authorize(Roles = "Admin")]
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
    [AllowAnonymous]
    public IActionResult Me()
    {
        if (User.Identity == null)
        {
            return NotFound("User.Identity is null");
        }
        Console.Out.WriteLine(User.Identity.Name);
        User? user = UserDbService.GetUserByUsername(User.Identity.Name);
        if (user == null)
        {
            return NotFound($"no User with {User.Identity.Name} found");
        }
        return Ok(new
        {
            username = user.Username,
            role = user.Role
        }); // return anonymous object of user that doesnt include password
    }


    private string GetTokenFromHeader(IHeaderDictionary? headers)
    {
        return headers?.Authorization.ToString().Split(" ").Last() ?? "";
    }
}
