using CashFlow.Communication.Enums.Users;

namespace CashFlow.Communication.Responses.Users;
public record ResponseUserJson
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Role Role { get; set; }
}
