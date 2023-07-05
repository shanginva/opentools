using System.Text.Json.Serialization;

namespace OpenTools.HomeAssistant.Messages;

public class ResultMessage
    : MessageBase
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("success")]
    public bool Success { get; set;  }

    [JsonPropertyName("result")]
    public dynamic? Result { get; set; }

}
