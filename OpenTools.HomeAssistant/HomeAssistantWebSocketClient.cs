using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using OpenTools.HomeAssistant.Commands;
using OpenTools.HomeAssistant.Messages;

namespace OpenTools.HomeAssistant;

public class HomeAssistantWebSocketClient
    : IHomeAssistantWebSocketClient
{
    private ClientWebSocket _clientWebSocket;

    public HomeAssistantWebSocketClient()
    {
        _clientWebSocket = new ClientWebSocket();
    }

    public async Task Connect(Uri uri, CancellationToken cancellationToken)
    {
        await _clientWebSocket.ConnectAsync(uri, cancellationToken);

        await Task.Factory.StartNew(async () =>
        {
            while (true)
            {
                await ReadMessage(cancellationToken);
            }
        });
    }

    private Task Authenticate(AuthMessage authMessage, CancellationToken cancellationToken)
        => SendMessageAsync(authMessage, cancellationToken);

    private Task Subscribe(SubscribeToEventCommand subscribeToEventCommand, CancellationToken cancellationToken)
        => SendCommandAsync(subscribeToEventCommand, cancellationToken);

    private async Task SendCommandAsync(CommandBase command, CancellationToken cancellationToken)
    {
        var serialized = JsonSerializer.Serialize(command, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
        var byteMessage = Encoding.UTF8.GetBytes(serialized);
        var segmnet = new ArraySegment<byte>(byteMessage);

        await _clientWebSocket.SendAsync(segmnet, WebSocketMessageType.Text, true, cancellationToken);
    }

    private async Task SendMessageAsync(MessageBase message, CancellationToken cancellationToken)
    {
        var serialized = JsonSerializer.Serialize(message);
        var byteMessage = Encoding.UTF8.GetBytes(serialized);
        var segmnet = new ArraySegment<byte>(byteMessage);

        await _clientWebSocket.SendAsync(segmnet, WebSocketMessageType.Text, true, cancellationToken);
    }

    private async Task ReadMessage(CancellationToken cancellationToken)
    {
        WebSocketReceiveResult socketReceiveResult;
        var message = new ArraySegment<byte>(new byte[4096]);
        do
        {
            socketReceiveResult = await _clientWebSocket.ReceiveAsync(message, cancellationToken);
            if (socketReceiveResult.MessageType != WebSocketMessageType.Text)
                break;
            var messageBytes = message.Skip(message.Offset).Take(socketReceiveResult.Count).ToArray();
            var messageString = Encoding.UTF8.GetString(messageBytes);
            MessageBase? response = null;
            try
            {
                response = JsonSerializer
                    .Deserialize<MessageBase>(AdjustDiscriminator(messageString, "type"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            switch (response)
            {
                case AuthRequiredMessage _:
                    await Authenticate(new AuthMessage("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJkYmViN2ZhYjRlYjY0MmY0YTM4MWYxMTUxNDRkNDQ4OSIsImlhdCI6MTY3NDcyMjE3MSwiZXhwIjoxOTkwMDgyMTcxfQ.Bs8sz6xcEYbsaMrWVF87HO5B43QFlN6lLpI127uJigg"), cancellationToken);
                    break;
                case AuthOkMessage _:
                    Console.WriteLine("You are logged in");
                    await Subscribe(new SubscribeToEventCommand(1), cancellationToken);
                    break;
                case AuthInvalidMessage authInvalid:
                    Console.WriteLine(authInvalid.Message);
                    break;
                case ResultMessage result:
                    Console.WriteLine($"You have successfully subscribed for an event {result.Id}");
                    break;
            }
        }
        while (!socketReceiveResult.EndOfMessage);
    }

    private static string AdjustDiscriminator(string input, string discriminatorName)
    {
        var regex = new Regex($"\"{discriminatorName}\":\"(.*)\",");
        var match = regex.Match(input);
        return input
            .Replace(match.Value, "")
            .Insert(1, match.Value);
    }
}