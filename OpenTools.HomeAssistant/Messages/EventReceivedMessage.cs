using OpenTools.HomeAssistant.Events;

namespace OpenTools.HomeAssistant.Messages;
internal class EventReceivedMessage
    : MessageBase
{
    public EventBase Event { get; set; } = null!;
}
