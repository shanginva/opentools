namespace OpenTools.HomeAssistant;

public interface IHomeAssistantHttpClient
{
    Task<byte[]> GetCameraProxyImage(string entityId);
}