using Microsoft.AspNetCore.Mvc;
using SteamApiService.Models;
using SteamApiService.Services;

namespace SteamApiService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SteamController(ISteamService service) : ControllerBase
{
    [HttpGet("news")]
    public async Task<IActionResult> GetNewsAsync()
    {
        var news = await service.GetNewsAsync();
        return Ok(news);
    }
    
    [HttpGet("stats")]
    public async Task<IActionResult> GetStatsAsync()
    {
        var stats = await service.GetStatsAsync();
        return Ok(stats);
    }
    
    [HttpGet("players")]
    public async Task<IActionResult> GetCurrentPlayerCountAsync()
    {
        var count = await service.GetCurrentPlayerCountAsync();
        return Ok(count);
    }
}