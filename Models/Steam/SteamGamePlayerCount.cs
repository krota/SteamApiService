using System.Text.Json.Serialization;

namespace SteamApiService.Models.Steam;

public class SteamPlayerCountResponse
{
    [JsonPropertyName("response")]
    public SteamPlayerCountData Response { get; init; }
}

public class SteamPlayerCountData
{
    [JsonPropertyName("player_count")]
    public int PlayerCount { get; set; }

    [JsonPropertyName("result")]
    public int Result { get; set; }
}