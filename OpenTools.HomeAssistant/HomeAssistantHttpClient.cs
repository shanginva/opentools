using System.Net.Http.Headers;

namespace OpenTools.HomeAssistant;

public class HomeAssistantHttpClient : IHomeAssistantHttpClient
{
    private readonly HomeAssistantCredentials credentials;
    private readonly HttpClient httpClient;

    public HomeAssistantHttpClient(HomeAssistantCredentials credentials, HttpClient httpClient)
    {
        this.credentials = credentials;
        this.httpClient = httpClient;
        this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", credentials.AccessToken);
    }

    public async Task<byte[]> GetCameraProxyImage(string entityId)
    {
        var result = await httpClient.GetAsync($"{credentials.Uri}api/camera_proxy/{entityId}");
        result.EnsureSuccessStatusCode();
        using var stream = result.Content.ReadAsStream();
        var buffer = new byte[stream.Length];
        stream.Read(buffer);
        return buffer;
    }
}
