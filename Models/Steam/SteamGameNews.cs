using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using SteamApiService.Utils.Json;

namespace SteamApiService.Models.Steam;

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
    [JsonPropertyName("contentsHtml")]
    public string? SanitizedContents =>
        Contents == null
            ? null
            : ConvertSteamBbCodeToHtml(Contents);
    [JsonPropertyName("feedlabel")]
    public string? FeedLabel { get; set; }
    [JsonPropertyName("date")]
    [JsonConverter(typeof(UnixToDateTimeConverter))]
    public DateTime Date { get; set; }
    [JsonPropertyName("feedname")]
    public string? Feedname { get; set; }
    [JsonPropertyName("feed_type")]
    public int FeedType { get; set; }
    [JsonPropertyName("appid")]
    public int AppId { get; set; }
    
    private static string ConvertSteamBbCodeToHtml(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw)) return string.Empty;

        var sanitized = System.Net.WebUtility.HtmlDecode(raw);

        // Convert escaped \n to actual newlines
        sanitized = sanitized.Replace("\\n", "\n");

        // Convert BBCode-style tags to HTML
        sanitized = Regex.Replace(
            sanitized,
            @"\[url=(.*?)\](.*?)\[/url\]",
            "<a href=\"$1\" target=\"_blank\" rel=\"noopener\">$2</a>",
            RegexOptions.IgnoreCase
        );
        sanitized = Regex.Replace(sanitized, @"\[h2\](.*?)\[/h2\]", "<h2>$1</h2>", RegexOptions.IgnoreCase);
        sanitized = Regex.Replace(sanitized, @"\[list\]", "<ul>", RegexOptions.IgnoreCase);
        sanitized = Regex.Replace(sanitized, @"\[/list\]", "</ul>", RegexOptions.IgnoreCase);
        sanitized = Regex.Replace(sanitized, @"\[\*\]", "<li>", RegexOptions.IgnoreCase);

        // Replace Steam image tag
        sanitized = Regex.Replace(
            sanitized,
            @"\{STEAM_CLAN_IMAGE\}\/(\S+)",
            "<img src=\"https://clan.cloudflare.steamstatic.com/images/$1\" alt=\"Steam Image\" style=\"max-width:100%; height:auto;\">"
        );

        // Paragraph and line breaks
        sanitized = Regex.Replace(sanitized, @"\n\s*\n", "<br><br>");
        sanitized = Regex.Replace(sanitized, @"\n", "<br>");

        // Strip remaining BBCode tags
        sanitized = Regex.Replace(sanitized, @"\[(\/?)(b|i|u|img|list|\*|h[1-6])\]", "", RegexOptions.IgnoreCase);

        return sanitized.Trim();
    }
}

