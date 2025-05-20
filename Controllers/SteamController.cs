using Microsoft.AspNetCore.Mvc;
using SteamApiService.Models;
using SteamApiService.Services;

namespace SteamApiService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SteamController(ISteamService service) : ControllerBase
{
    [HttpGet("news/{steamAppId:int}")]
    public async Task<IActionResult> GetNewsAsync(int steamAppId, [FromQuery] int count = 3)
    {
        var news = await service.GetNewsAsync(steamAppId, count);
        return Ok(news);
    }
    
    [HttpGet("players/{steamAppId:int}")]
    public async Task<IActionResult> GetCurrentPlayerCountAsync(int steamAppId)
    {
        var count = await service.GetCurrentPlayerCountAsync(steamAppId);
        return Ok(count);
    }
}