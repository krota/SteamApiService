using SteamApiService.Models;
using SteamApiService.Models.Steam;

namespace SteamApiService.Services;

public interface ISteamService
{
    Task<int> GetCurrentPlayerCountAsync(int steamAppId);
    Task<List<SteamGameNewsItem>?> GetNewsAsync(int steamAppId);
}