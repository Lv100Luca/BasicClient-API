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

    private string GenerateToken(User user) // generates token somehow 
    // todo move to own class
    {
        // ask -> prevent null reference?
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256); // uhh
        // claims are probably gonna be tested against what the users token claims he is 
        // ie if his claims of username etc matches the token
        var claims = new[] //todo: what are claims?
        {
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.Name, $"I am {user.Username} Crazy Claim"),
        };

        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(60), //token expires in 60 minutes
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


}