using CashFlow.Communication.Enums.Users;

namespace CashFlow.Communication.Requests.Users;
public record RequestRegisterUserJson
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty; 
    public string Password { get; set; } = string.Empty;    
}
