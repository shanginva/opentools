using System.Text.Json.Serialization;

namespace OpenTools.HomeAssistant.Messages;


public class AuthRequiredMessage
    : MessageBase
{


    [JsonPropertyName("ha_version")]
    public string HaVersion { get; set; } = null!;
}