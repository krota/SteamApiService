using System.Text.Json.Serialization;

namespace SteamApiService.Models;

public class SteamGameNewsResponse
{
    [JsonPropertyName("appnews")]
    public SteamGameNewsData? NewsData { get; set; }
}

public class SteamGameNewsData
{
    [JsonPropertyName("appid")]
    public int AppId { get; set; }
    [JsonPropertyName("newsitems")]
    public List<SteamGameNewsItem>? NewsItems { get; set; }
}

public class SteamGameNewsItem
{
    [JsonPropertyName("gid")]
    public string? Gid { get; set; }
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    [JsonPropertyName("url")]
    public string? Url { get; set; }
    [JsonPropertyName("is_external_url")]
    public bool IsExternalUrl { get; set; }
    [JsonPropertyName("author")]
    public string? Author { get; set; }
    [JsonPropertyName("contents")]
    public string? Contents { get; set; }
    [JsonPropertyName("feedlabel")]
    public string? FeedLabel { get; set; }
    [JsonPropertyName("date")]
    public long DateStringUnix { get; set; }
    [JsonPropertyName("feedname")]
    public string? Feedname { get; set; }
    [JsonPropertyName("feed_type")]
    public int FeedType { get; set; }
    [JsonPropertyName("appid")]
    public int AppId { get; set; }
    public DateTime DatePosted => DateTimeOffset.FromUnixTimeSeconds(DateStringUnix).UtcDateTime;
}