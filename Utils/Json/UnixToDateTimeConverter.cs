using System.Text.Json;
using System.Text.Json.Serialization;

namespace SteamApiService.Utils.Json;

public class UnixToDateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var seconds = reader.GetInt64();
        return DateTimeOffset.FromUnixTimeSeconds(seconds).UtcDateTime;
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        // Don't write as Unix time — preserve ISO 8601
        JsonSerializer.Serialize(writer, value, options);
    }
}