using Newtonsoft.Json;

namespace SteamApiService.Models.Steam;

public class SteamPlayerCountResponse
{
    public required SteamPlayerCountData? Response { get; init; }
}

public class SteamPlayerCountData
{
    [JsonProperty("player_count")]
    public int PlayerCount { get; set; }
    [JsonIgnore]
    public int Result { get; set; }
}