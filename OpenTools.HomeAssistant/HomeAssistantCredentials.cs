namespace OpenTools.HomeAssistant;

public class HomeAssistantCredentials
{
    public HomeAssistantCredentials(Uri uri, string accessToken)
    {
        Uri = uri;
        AccessToken = accessToken;
    }

    public Uri Uri { get; }
    public string AccessToken { get; }
}
