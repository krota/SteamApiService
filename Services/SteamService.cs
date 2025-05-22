using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using SteamApiService.Models.Steam;
using SteamApiService.Settings;

namespace SteamApiService.Services;

public class SteamService(HttpClient httpClient, IOptions<SteamSettings> settings) : ISteamService
{
    public async Task<int> GetCurrentPlayerCountAsync(int steamAppId)
    {
        var apiUrl = $"{settings.Value.ApiUrlBase}{settings.Value.UserStatsApiUri}/GetNumberOfCurrentPlayers/v1/?format=json&appid={steamAppId}";
        var response = await httpClient.GetAsync(apiUrl);
        response.EnsureSuccessStatusCode();
        
        var rawJson = await response.Content.ReadAsStringAsync();
        var playerCountResponse = JsonConvert.DeserializeObject<SteamPlayerCountResponse>(rawJson);
        
        return playerCountResponse == null ? 0 : playerCountResponse.Response.PlayerCount;
    }

    public async Task<List<SteamGameNewsItem>?> GetNewsAsync(int steamAppId, int count = 3)
    {
        var apiUrl = $"{settings.Value.ApiUrlBase}{settings.Value.NewsApiUri}/GetNewsForApp/v0002?format=json&appid={steamAppId}&count={count}";
        Console.WriteLine(apiUrl);
        var response = await httpClient.GetAsync(apiUrl);
        response.EnsureSuccessStatusCode();
        
        var rawJson = await response.Content.ReadAsStringAsync();
        var newsResponse = JsonConvert.DeserializeObject<SteamGameNewsResponse>(rawJson);

        return newsResponse?.AppNews?.NewsItems ?? [];
    }
}