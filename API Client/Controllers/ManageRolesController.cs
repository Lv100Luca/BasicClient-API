using API_Client.Model.DTO;
using API_Client.Model.services;
using Microsoft.AspNetCore.Mvc;

namespace API_Client.Controllers;

public class ManageRolesController : ControllerBase
{
    private readonly DataService _dataService;
    private ILogger<ManageRolesController> _logger;


    public ManageRolesController(DataService dataService, ILogger<ManageRolesController> logger)
    {
        _dataService = dataService;
        _logger = logger;
    }


    [HttpPost("role")]
    public IActionResult AddRole(RoleDto role)
    {
        var newRole = _dataService.AddRole(role);

        if (newRole is not null)
        {
            return Created(newRole.Id.ToString(), newRole);
        }
        return Conflict($"Role with Name '{role.RoleName}' already exists.");
    }
}
