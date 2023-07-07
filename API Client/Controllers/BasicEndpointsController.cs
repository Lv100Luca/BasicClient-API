using API_Client.Model;
using Microsoft.AspNetCore.Mvc;

namespace API_Client.Controllers;

[ApiController]
[Route("[controller]")] //todo controller/action
public class BasicEndpointsController : ControllerBase
{
    private readonly ILogger<BasicEndpointsController> _logger;

    public BasicEndpointsController(ILogger<BasicEndpointsController> logger)
    {
        _logger = logger;
    }
    #region Get-Requsts

    [HttpGet("get/success")]
    public IActionResult GetDataSuccess()
    {
        return Ok(new ResponseMessage("GET", 200, "Success"));

    }

    [HttpGet("get/unauthorized")]
    public IActionResult GetDataUnauthorized()
    {
        return Unauthorized(new ResponseMessage("GET", 401, "Unauthorized"));

    }    [HttpGet("get/nocontent")]
    public IActionResult GetDataNoContent()
    {
        return NoContent(); //no content requires empty response

    }

    #endregion
}