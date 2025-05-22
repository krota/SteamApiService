using Newtonsoft.Json;

namespace SteamApiService.Utils.Json
{
    public class UnixToDateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            if (reader is { TokenType: JsonToken.Integer, Value: long seconds })
            {
                return DateTimeOffset.FromUnixTimeSeconds(seconds).UtcDateTime;
            }

            throw new JsonSerializationException($"Expected integer Unix timestamp, got {reader.TokenType}");
        }

        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            // Serialize as ISO 8601 (default behavior)
            writer.WriteValue(value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ")); // seconds only
        }
    }
}