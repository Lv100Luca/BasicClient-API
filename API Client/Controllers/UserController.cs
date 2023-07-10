using API_Client.Model.services;
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


    // [HttpPost("Login")]
    // [AllowAnonymous]
    // public IActionResult Login(UserLoginDto userLogin)
    // {
    //     var user = UserDbServiceList.Authenticate(userLogin);
    //     if (user == null)
    //     {
    //         return NotFound();
    //     }
    //
    //     var token = _jwtTokenService.GenerateToken(user);
    //     return Ok(token);
    // }
    //
    //
    // [HttpGet]
    // [Authorize(Roles = "Admin")]
    // //todo handle Roles internally
    // public IActionResult GetAllUsers()
    // {
    //     return Ok(UserDbServiceList.GetAllUsers());
    // }
    //
    //
    // [HttpPost]
    // [Authorize(Roles = "Admin")]
    // public IActionResult AddUser(User newUser)
    // {
    //     if (UserDbServiceList.AddUser(newUser))
    //     {
    //         return Created("Created user", newUser);
    //     }
    //     return Conflict("User already exists");
    // }
    //
    //
    // [HttpDelete] // ask -> fall admin wird gelöscht aber Token ist noch beim user
    // [Authorize(Roles = "Admin")]
    // public IActionResult DeleteUser(string name)
    // {
    //     if (UserDbServiceList.DeleteUser(name))
    //     {
    //         return NoContent();
    //     }
    //     return NotFound("User not found");
    // }
    //
    //
    // [HttpGet("me")]
    // [Authorize]
    // public IActionResult Me() // ask -> Better way of extracting username from token
    // {
    //     var username = _jwtTokenService.GetUsernameFromToken(GetTokenFromHeader(Request.Headers));
    //     User? user = UserDbServiceList.GetUserByUsername(username);
    //     if (user == null)
    //     {
    //         return NotFound($"no User with {username} found");
    //     }
    //     return Ok(new
    //     {
    //         username = user.Username,
    //         role = user.Role,
    //     }); // return anonymous object of user that doesnt include password
    // }
    //
    //
    // [HttpGet("Claims")]
    // [Authorize]
    // public IActionResult GetClaims()
    // {
    //     var username = (HttpContext.User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.NameIdentifier).Value;
    //     Console.Out.WriteLine(username);
    //     if (HttpContext.User.Identity is ClaimsIdentity identity)
    //     {
    //         IEnumerable<Claim> claims = identity.Claims;
    //         foreach (var claim in claims)
    //         {
    //             Console.Out.WriteLine(claim.Value);
    //         }
    //     }
    //     return Ok();
    // }
    //
    //
    // private string GetTokenFromHeader(IHeaderDictionary headers)
    // {
    //     return Request.Headers["Authorization"].ToString().Split(" ").Last();
    // }
}
