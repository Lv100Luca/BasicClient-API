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
    public IActionResult PostSuccess(IncomingDataDto incomingData)
    {
        return Ok(new ResponseMessage("POST", 200, $"Success: {incomingData.secret}"));
    }


    [HttpPost("post/unauthorized")]
    public IActionResult PostUnauthorized(IncomingDataDto incomingData)
    {
        return Unauthorized(new ResponseMessage("POST", 401, $"Unauthorized: {incomingData.secret}"));
    }


    [HttpPost("post/nocontent")]
    public IActionResult PostNoContent(IncomingDataDto incomingData)
    {
        _logger.LogTrace(incomingData.secret.ToString());
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
    public IActionResult PostInternalSeverError(IncomingDataDto incomingData)
    {
        throw new Exception($"Internal Server Error {incomingData.secret}");
        // ReSharper disable once HeuristicUnreachableCode
        // return BadRequest(new ResponseMessage("POST", 400, "Error")); unreachable (on purpose)
    }

    #endregion
}
