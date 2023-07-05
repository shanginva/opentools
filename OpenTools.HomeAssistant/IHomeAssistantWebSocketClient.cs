namespace OpenTools.HomeAssistant;

public interface IHomeAssistantWebSocketClient
{
    Task Connect(Uri uri, CancellationToken cancellationToken);
}