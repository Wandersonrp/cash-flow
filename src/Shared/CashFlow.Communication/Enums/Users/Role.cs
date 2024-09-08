using System.Text.Json.Serialization;

namespace CashFlow.Communication.Enums.Users;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Role
{
    Admin,
    User
}
