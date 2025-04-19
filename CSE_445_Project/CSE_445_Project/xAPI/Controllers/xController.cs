using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class xController : ControllerBase
{
    private readonly xServices _xService;

    public xController(xServices xService)
    {
        _xService = xService;
    }


    [HttpGet("search")] //HTTP GET
    public async Task<IActionResult> GetTweets([FromQuery] string query)
    {
        try
        {
            var result = await _xService.GetTweetsAsync(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
