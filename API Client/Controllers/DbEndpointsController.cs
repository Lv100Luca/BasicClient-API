using API_Client.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Client.Controllers;

[ApiController]
[Route("DB")]
public class DbEndpointsController : ControllerBase
{
    private readonly UserDbService _userDbService;


    public DbEndpointsController(UserDbService userDbService)
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
}
