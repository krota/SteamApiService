using System.Text.Json;
using Microsoft.Extensions.Options;
using SteamApiService.Models;
using SteamApiService.Settings;

namespace SteamApiService.Services;

public class SteamService(HttpClient httpClient, IOptions<SteamSettings> settings) : ISteamService
{
    public async Task<int> GetCurrentPlayerCountAsync(int steamAppId)
    {
        // https://api.steampowered.com/ISteamUserStats/GetNumberOfCurrentPlayers/v1/?format=json&appid=671860
        var apiUrl = settings.Value.ApiUrlBase + settings.Value.UserStatsApiUri +
                     "/GetNumberOfCurrentPlayers/v1/?format=json&appid=" + steamAppId;
        var response = await httpClient.GetAsync(apiUrl);
        response.EnsureSuccessStatusCode();
        
        var rawJson = await response.Content.ReadAsStringAsync();
        var playerCountResponse = JsonSerializer.Deserialize<SteamPlayerCountResponse>(rawJson);

        return playerCountResponse == null ? 0 : playerCountResponse.Response.PlayerCount;
    }

    public async Task<List<SteamGameNewsItem>?> GetNewsAsync(int steamAppId = 671860)
    {
        // https://api.steampowered.com/ISteamNews/GetNewsForApp/v0002/?appid=671860&count=3&format=json
        var apiUrl = settings.Value.ApiUrlBase + settings.Value.NewsApiUri +
                     "/GetNewsForApp/v0002?format=json&appid=" + steamAppId;
        Console.WriteLine(apiUrl);
        var response = await httpClient.GetAsync(apiUrl);
        response.EnsureSuccessStatusCode();
        
        var rawJson = await response.Content.ReadAsStringAsync();
        var newsResponse = JsonSerializer.Deserialize<SteamGameNewsResponse>(rawJson);

        return newsResponse?.NewsData?.NewsItems ?? [];
    }

    public async Task<SteamGameStats> GetStatsAsync(int steamAppId)
    {
        return null;
    }
}