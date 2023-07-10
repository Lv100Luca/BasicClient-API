using API_Client.Model.DTO;
using API_Client.Model.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Client.Controllers;

[ApiController]
[Route("DB")]
public class TestUserController : ControllerBase
{
    private readonly UserDbService _userDbService;


    public TestUserController(UserDbService userDbService)
    {
        _userDbService = userDbService;
    }


    [HttpGet("all")]
    [AllowAnonymous]
    public IActionResult GetAllUsers()
    {
        try
        {
            var users = _userDbService.GetAllUsers();

            return Ok(users);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    [HttpPost("user")]
    public IActionResult PostUser(User2 user)
    {
        try
        {
            _userDbService.AddUser(user);
            return Created("New User", "User Created");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpPost("role")]
    public IActionResult AddRole(Database.Entities.Role role)
    {
        var entity = _userDbService.AddRole(role);
        return Ok();
    }


    [HttpPost("user/single")]
    public IActionResult User2()
    {
        _userDbService.AddTmpUser();
        return Ok();
    }
}
