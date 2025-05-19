namespace SteamApiService.Settings;

public class SteamSettings
{
    public string ApiUrlBase { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string BattleBitSteamAppId { get; set; } = string.Empty;
    public string NewsApiUri { get; set; } = string.Empty;
    public string UserStatsApiUri { get; set; } = string.Empty;
    public string UserApiUri { get; set; } = string.Empty;
}