using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SteamApiService.Utils.Json;

public class SnakeCaseToCamelCaseResolver : DefaultContractResolver
{
    public SnakeCaseToCamelCaseResolver()
    {
        NamingStrategy = new CamelCaseNamingStrategy();
    }

    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var property = base.CreateProperty(member, memberSerialization);

        var attribute = property.AttributeProvider
            ?.GetAttributes(typeof(JsonPropertyAttribute), true)
            ?.FirstOrDefault() as JsonPropertyAttribute;

        if (!string.IsNullOrEmpty(attribute?.PropertyName) && NamingStrategy is CamelCaseNamingStrategy camelCase)
        {
            property.PropertyName = camelCase.GetPropertyName(member.Name, false);
        }

        return property;
    }
}