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
        return Ok(new
        {
            name = "luca",
            age = 20,
            password = "123",
        });
    }

    [HttpGet("get/unauthorized")]
    public IActionResult GetDataUnauthorized()
    {
        return Ok(new
        {
            name = "luca",
            age = 20,
            password = "123",
        });
    }

    #endregion
}