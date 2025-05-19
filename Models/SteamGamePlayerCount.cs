using System.Text.Json.Serialization;

namespace SteamApiService.Models;

public class SteamPlayerCountResponse
{
    [JsonPropertyName("response")]
    public SteamPlayerCountData Response { get; set; }
}

public class SteamPlayerCountData
{
    [JsonPropertyName("player_count")]
    public int PlayerCount { get; set; }

    [JsonPropertyName("result")]
    public int Result { get; set; }
}