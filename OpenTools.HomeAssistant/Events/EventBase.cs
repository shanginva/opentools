using System.Text.Json.Serialization;

namespace OpenTools.HomeAssistant.Events;

[JsonDerivedType(typeof(StateChangedEvent))]
[JsonDerivedType(typeof(AuhtenticatedEvent))]
public abstract class EventBase
{
}
