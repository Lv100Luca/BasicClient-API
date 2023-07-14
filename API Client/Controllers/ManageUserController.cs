using API_Client.Database.Entities;
using API_Client.Model.DTO;
using API_Client.Model.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Client.Controllers;

[Authorize(Roles = "admin")]
public class ManageUserController : ControllerBase
{
    private readonly DataService _dataService;
    private readonly ILogger<ManageUserController> _logger;


    public ManageUserController(DataService dataService, ILogger<ManageUserController> logger)
    {
        _dataService = dataService;
        _logger = logger;
    }


    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult<List<User>> GetAllUsers()
    {
        try
        {
            var users = _dataService.GetAllUsers().Result;

            if (users.Count == 0)
            {
                return NoContent();
            }
            return Ok(users);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    [HttpPost("user")]
    [AllowAnonymous]
    public IActionResult PostUser(UserDto user)
    {
        var newUser = _dataService.AddUser(user);
        if (newUser is null)
        {
            return BadRequest("hm"); // todo look at error handling
        }
        return Created(newUser.Id.ToString(), newUser);
    }


    [HttpGet("user/{id:int}")]
    public IActionResult GetUserById(int id)
    {
        return Ok(_dataService.GetUserById(id));
    }


    [HttpDelete("user/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteUser(int id)
    {
        if (_dataService.DeleteUser(id))
        {
            return NoContent();
        }
        return NotFound($"User {id} doesnt exist.");
    }


    [HttpPatch("user")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult ChangeUser(UserDto userDto)
    {
        if (_dataService.ChangeUserInformation(userDto))
        {
            return NoContent();
        }
        return NotFound($"User {userDto.id} doesnt exist.");
    }


    [HttpPatch("user/changePassword")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult ChangeUserPassword(UserDto userDto) // todo improve logic 
    {
        if (_dataService.ChangePassword(userDto))
        {
            return NoContent();
        }
        return NotFound($"User {userDto.id} doesnt exist.");
    }
}
