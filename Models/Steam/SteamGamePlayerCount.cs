namespace SteamApiService.Models.Steam;

public class SteamPlayerCountResponse
{
    public required SteamPlayerCountData Response { get; init; }
}

public class SteamPlayerCountData
{
    public int PlayerCount { get; set; }
    
    public int Result { get; set; }
}