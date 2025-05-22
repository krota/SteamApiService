using System.Net;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using SteamApiService.Models.Steam;
using SteamApiService.Settings;

namespace SteamApiService.Services;

public class SteamService(HttpClient httpClient, IOptions<SteamSettings> settings) : ISteamService
{
    public async Task<SteamPlayerCountData?> GetCurrentPlayerCountAsync(int steamAppId)
    {
        var apiUrl =
            $"{settings.Value.ApiUrlBase}{settings.Value.UserStatsApiUri}/GetNumberOfCurrentPlayers/v1/?format=json&appid={steamAppId}";
        var response = await httpClient.GetAsync(apiUrl);
        
        if (response.StatusCode is HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();

        var rawJson = await response.Content.ReadAsStringAsync();
        var playerCountResponse = JsonConvert.DeserializeObject<SteamPlayerCountResponse>(rawJson);

        return playerCountResponse?.Response;
    }

    public async Task<List<SteamGameNewsItem>?> GetNewsAsync(int steamAppId, int count = 3)
    {
        var apiUrl = $"{settings.Value.ApiUrlBase}{settings.Value.NewsApiUri}/GetNewsForApp/v0002?format=json&appid={steamAppId}&count={count}";
        var response = await httpClient.GetAsync(apiUrl);
        
        // This endpoint sends a 403 on invalid app ID when it should really be a 404. There's no auth.
        if (response.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.Forbidden)
            return null;
        
        response.EnsureSuccessStatusCode();
        
        var rawJson = await response.Content.ReadAsStringAsync();
        var newsResponse = JsonConvert.DeserializeObject<SteamGameNewsResponse>(rawJson);

        return newsResponse?.AppNews?.NewsItems ?? [];
    }
}