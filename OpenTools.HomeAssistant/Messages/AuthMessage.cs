using System.Text.Json.Serialization;

namespace OpenTools.HomeAssistant.Messages;
public class AuthMessage
    : MessageBase
{
    public AuthMessage(string accessToken)
    {
        AccessToken = accessToken;
    }

    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = null!;

}
