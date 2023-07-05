using System.Text.Json.Serialization;

namespace OpenTools.HomeAssistant.Messages;

public class AuthInvalidMessage
    : MessageBase
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = null!;
}
