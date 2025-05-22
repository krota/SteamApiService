using SteamApiService.Models;
using SteamApiService.Models.Steam;

namespace SteamApiService.Services;

public interface ISteamService
{
    Task<SteamPlayerCountData?> GetCurrentPlayerCountAsync(int steamAppId);
    Task<List<SteamGameNewsItem>?> GetNewsAsync(int steamAppId, int count = 3);
}