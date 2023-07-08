using API_Client.Model;
using API_Client.Model.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Client.Controllers;

[ApiController]
[Route("jwt")]
public class JwtTokenMagicController : ControllerBase
{
    private readonly IConfiguration _config;
    public JwtTokenMagicController(IConfiguration config)
    {
        _config = config;
    }
    [HttpGet("NoAuthGet")]
    public IActionResult NoAuthGet()
    {
        return Ok(new ResponseMessage("GET", 200, "Success"));
    }


    [HttpPost("Login")]
    [AllowAnonymous]
    public IActionResult Login(UserLoginDto userLogin)
    {
        User? user = MyDbServiceImplementation.Authenticate(userLogin);
        if (user == null)
            return NotFound();

        string token = GenerateToken(user);
        return Ok(token);
    }

    [HttpGet("/admin")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetAllUsers()
    {
        return Ok(MyDbServiceImplementation.GetAllUsers());
    }
    [HttpGet("/test")]
    [Authorize(Roles = "User")]
    public IActionResult GetMessage()
    {
        return Ok("Hello World!");
    }

    private string GenerateToken(User user) // generates token somehow 
    // todo move to own class
    {
        // ask -> prevent null reference?
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256); // uhh
        // claims hold User data
        var claims = new[] //todo: what are claims? -> claims are what is going to be written into the Payload of the token
        {
            new Claim("Username", user.Username),
            new Claim("Role", user.Role),
            new Claim("Claim", $"I am {user.Username} Crazy Claim"),
        };

        var token = new JwtSecurityToken(
            
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(60), //token expires in 60 minutes
            // all the above are Claims
            signingCredentials: credentials // this is the secret that is required to verify the tokens authenticity 
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


}