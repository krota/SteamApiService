using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using SteamApiService.Utils.Json;
using static System.Text.RegularExpressions.Regex;

namespace SteamApiService.Models.Steam;

public class SteamGameNewsResponse
{
    [JsonPropertyName("appnews")]
    public required SteamGameNewsData NewsData { get; init; }
}

public class SteamGameNewsData
{
    public int AppId { get; set; }
    public List<SteamGameNewsItem>? NewsItems { get; set; }
}

public class SteamGameNewsItem
{
    public string? Gid { get; set; }
    public string? Title { get; set; }
    public string? Url { get; set; }
    public bool IsExternalUrl { get; set; }
    public string? Author { get; set; }
    public string? Contents { get; set; }
    public string? ContentsHtml =>
        Contents == null
            ? null
            : ConvertSteamBbCodeToHtml(Contents);
    public string? FeedLabel { get; set; }
    [JsonConverter(typeof(UnixToDateTimeConverter))]
    public DateTime Date { get; set; }
    public string? Feedname { get; set; }
    public int FeedType { get; set; }
    public int AppId { get; set; }
    
    private static string ConvertSteamBbCodeToHtml(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw)) return string.Empty;

        var sanitized = System.Net.WebUtility.HtmlDecode(raw);

        // Convert escaped \n to actual newlines
        sanitized = sanitized.Replace("\\n", "\n");

        // Convert BBCode-style tags to HTML
        sanitized = Replace(
            sanitized,
            @"\[url=(.*?)\](.*?)\[/url\]",
            "<a href=\"$1\" target=\"_blank\" rel=\"noopener\">$2</a>",
            RegexOptions.IgnoreCase
        );
        sanitized = Replace(sanitized, @"\[h2\](.*?)\[/h2\]", "<h2>$1</h2>", RegexOptions.IgnoreCase);
        sanitized = Replace(sanitized, @"\[list\]", "<ul>", RegexOptions.IgnoreCase);
        sanitized = Replace(sanitized, @"\[/list\]", "</ul>", RegexOptions.IgnoreCase);
        sanitized = Replace(sanitized, @"\[\*\]", "<li>", RegexOptions.IgnoreCase);

        // Replace Steam image tag
        sanitized = Replace(
            sanitized,
            @"\{STEAM_CLAN_IMAGE\}\/(\S+)",
            "<img src=\"https://clan.cloudflare.steamstatic.com/images/$1\" alt=\"Steam Image\" style=\"max-width:100%; height:auto;\">"
        );

        // Paragraph and line breaks
        sanitized = Replace(sanitized, @"\n\s*\n", "<br><br>");
        sanitized = Replace(sanitized, @"\n", "<br>");

        // Strip remaining BBCode tags
        sanitized = Replace(sanitized, @"\[(\/?)(b|i|u|img|list|\*|h[1-6])\]", "", RegexOptions.IgnoreCase);

        return sanitized.Trim();
    }
}

