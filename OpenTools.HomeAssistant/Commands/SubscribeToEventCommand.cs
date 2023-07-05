using System.Text.Json.Serialization;

namespace OpenTools.HomeAssistant.Commands;

internal class SubscribeToEventCommand
    : CommandBase
{
    public SubscribeToEventCommand(int id): base(id)
    {
    }

    [JsonPropertyName("event_type")]
    public string? EventType { get; set; }
}
