using API_Client.Exceptions;
using API_Client.Model.DTO;
using API_Client.Model.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Client.Controllers;

public class LoginController : ControllerBase
{
    private readonly DataService _dataService;
    private readonly JwtTokenService _jwtTokenService;
    private readonly ILogger<LoginController> _logger;


    public LoginController(JwtTokenService jwtTokenService, DataService dataService, ILogger<LoginController> logger)
    {
        _jwtTokenService = jwtTokenService;
        _dataService = dataService;
        _logger = logger;
    }


    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Login(LoginDto userLogin)
    {
        var user = _dataService.Authenticate(userLogin);
        if (user is null)
        {
            return NotFound("User not found");
        }
        return Ok(_jwtTokenService.GenerateToken(user));
    }


    [HttpPost("me")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Me()
    {
        try
        {
            var token = _jwtTokenService.ParseToken(HttpContext);
            var user = _dataService.GetUserById(int.Parse(_jwtTokenService.GetClaimFromToken(token, "Id"))); // todo better error handling 
            if (user is not null)
            {
                return Ok(user);
            }
            return NotFound("User not found");
        }
        catch (JwtTokenException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    [HttpPost("test")]
    public IActionResult Test()
    {
        return Ok();
    }


    [HttpGet("admin")]
    [Authorize(Roles = "admin")]
    public IActionResult AdminEndpoint()
    {
        // Handle the authorized request for the "admin" role

        return Ok();
    }


    [HttpGet("user")]
    [Authorize(Roles = "user")]
    public IActionResult UserEndpoint()
    {
        // Handle the authorized request for the "user" role

        return Ok();
    }


    [HttpGet("both")]
    [Authorize(Roles = "admin, user")]
    public IActionResult NoRoleEndpoint()
    {
        // Handle the authorized request for users with no specific roles

        return Ok();
    }
}
