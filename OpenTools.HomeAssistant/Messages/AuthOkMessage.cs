using System.Text.Json.Serialization;

namespace OpenTools.HomeAssistant.Messages;

public class AuthOkMessage
    : MessageBase
{
    [JsonPropertyName("ha_version")]
    public string HaVersion { get; set; } = null!;
}
