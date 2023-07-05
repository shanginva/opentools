using System.Text.Json.Serialization;

namespace OpenTools.HomeAssistant.Commands;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(SubscribeToEventCommand), typeDiscriminator: "subscribe_events")]
internal abstract class CommandBase
{

    protected CommandBase(int id)
    {
        Id = id;
    }

    [JsonPropertyName("id")]
    public int Id { get; }
}
