using SteamApiService.Models;

namespace SteamApiService.Services;

public interface ISteamService
{
    Task<int> GetCurrentPlayerCountAsync(int steamAppId = 671860);
    Task<List<SteamGameNewsItem>?> GetNewsAsync(int steamAppId = 671860);
    Task<SteamGameStats> GetStatsAsync(int steamAppId = 671860);
}