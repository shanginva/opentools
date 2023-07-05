// See https://aka.ms/new-console-template for more information
using OpenTools.HomeAssistant;

Console.WriteLine("Hello, World!");

var homeAssistantClient = new HomeAssistantWebSocketClient();

await homeAssistantClient.Connect(new Uri("ws://homeassistant.local:8123/api/websocket"), default);

using HttpClient httpClient = new HttpClient();
var homeAssistantHttpClient = new HomeAssistantHttpClient(
    new HomeAssistantCredentials(new Uri("http://homeassistant.local:8123"), "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJkYmViN2ZhYjRlYjY0MmY0YTM4MWYxMTUxNDRkNDQ4OSIsImlhdCI6MTY3NDcyMjE3MSwiZXhwIjoxOTkwMDgyMTcxfQ.Bs8sz6xcEYbsaMrWVF87HO5B43QFlN6lLpI127uJigg"),
    httpClient);
var source = await homeAssistantHttpClient.GetCameraProxyImage("camera.192_168_2_101");
await File.WriteAllBytesAsync("C:\\Repos\\image.jpg", source!.ToArray());
Console.ReadKey();