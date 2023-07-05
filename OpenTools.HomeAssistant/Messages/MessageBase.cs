using System.Text.Json.Serialization;

namespace OpenTools.HomeAssistant.Messages;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(AuthMessage), typeDiscriminator: "auth")]
[JsonDerivedType(typeof(AuthRequiredMessage), typeDiscriminator: "auth_required")]
[JsonDerivedType(typeof(AuthOkMessage), typeDiscriminator: "auth_ok")]
[JsonDerivedType(typeof(AuthInvalidMessage), typeDiscriminator: "auth_invalid")]
[JsonDerivedType(typeof(ResultMessage), typeDiscriminator: "result")]
[JsonDerivedType(typeof(EventReceivedMessage), typeDiscriminator: "event")]
public abstract class MessageBase
{
}
