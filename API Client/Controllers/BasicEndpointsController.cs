using API_Client.Model;
using Microsoft.AspNetCore.Mvc;

namespace API_Client.Controllers;

[ApiController]
[Route("[controller]")]
public class BasicEndpointsController : ControllerBase
{
    private readonly ILogger<BasicEndpointsController> _logger;


    public BasicEndpointsController(ILogger<BasicEndpointsController> logger)
    {
        _logger = logger;
    }


    #region Get-Requests

    [HttpGet("get/success")]
    public IActionResult GetSuccess()
    {
        return Ok(new ResponseMessage("GET", 200, "Success"));
    }


    [HttpGet("get/unauthorized")]
    public IActionResult GetUnauthorized()
    {
        return Unauthorized(new ResponseMessage("GET", 401, "Unauthorized"));
    }


    [HttpGet("get/nocontent")]
    public IActionResult GetNoContent()
    {
        return NoContent(); //no content requires empty response
    }

    #endregion

    #region Post-Requests

    [HttpPost("post/success")]
    public IActionResult PostSuccess(string secret)
    {
        return Ok(new ResponseMessage("POST", 200, $"Success: {secret}"));
    }


    [HttpPost("post/unauthorized")]
    public IActionResult PostUnauthorized(string secret)
    {
        return Unauthorized(new ResponseMessage("POST", 401, $"Unauthorized: {secret}"));
    }


    [HttpPost("post/nocontent")]
    public IActionResult PostNoContent(string secret)
    {
        _logger.LogTrace(secret);
        return NoContent(); //no content requires empty response
    }


    [HttpPost("post/nobody")]
    public IActionResult PostNoBody()
    {
        return Ok(new ResponseMessage("POST", 200, "Success"));
    }


    [HttpPost("post/empty")]
    public IActionResult PostEmpty()
    {
        return Ok();
    }


    [HttpPost("post/badrequest")]
    public IActionResult PostBadRequest()
    {
        return BadRequest(new ResponseMessage("POST", 400, "Error"));
    }


    [HttpPost("post/error")]
    public IActionResult PostInternalSeverError(string secret)
    {
        throw new Exception($"Internal Server Error {secret}");
        // return BadRequest(new ResponseMessage("POST", 400, "Error")); unreachable (on purpose)
    }

    #endregion
}
