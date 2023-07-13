using API_Client.Database.Entities;
using API_Client.Model.DTO;
using API_Client.Model.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Client.Controllers;

[ApiController]
[Route("DB")]
public class TestUserController : ControllerBase
{
    private readonly UserService _userService;


    public TestUserController(UserService userService)
    {
        _userService = userService;
    }


    [HttpGet("all")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult<List<User>> GetAllUsers()
    {
        try
        {
            var users = _userService.GetAllUsers().Result;

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
    public IActionResult PostUser(UserDTO user)
    {
        var newUser = _userService.AddUser(user);
        if (newUser is not null)
        {
            return Created(newUser.Id.ToString(), newUser);
        }
        return BadRequest("hm");
    }


    [HttpPost("role")]
    public IActionResult AddRole(RoleDTO role)
    {
        var newRole = _userService.AddRole(role);

        if (newRole is not null)
        {
            return Created(newRole.Id.ToString(), newRole);
        }
        return Conflict($"Role with Name '{role.RoleName}' already exists.");
    }


    [HttpGet("user/{id:int}")]
    public IActionResult GetUserById(int id)
    {
        return Ok(_userService.GetUserById(id));
    }


    [HttpDelete("user/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteUser(int id)
    {
        if (_userService.DeleteUser(id))
        {
            return NoContent();
        }
        return NotFound($"User {id} doesnt exist.");
    }
}
