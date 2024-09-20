using CashFlow.Domain.Entities;
using CashFlow.Domain.Enums;
using CashFlow.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CashFlow.Infrastructure.Security.Tokens;
public class JwtTokenGenerator : IAccessTokenGenerator
{
    private readonly string _signingKey;
    private readonly uint _expirationTimeMinutes;

    public JwtTokenGenerator(string signingKey, uint expirationTimeMinutes)
    {
        _signingKey = signingKey;
        _expirationTimeMinutes = expirationTimeMinutes;
    }

    public string Generate(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Sid, user.UserIdentifier.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, nameof(user.Role))
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_signingKey)), SecurityAlgorithms.HmacSha256Signature),
            Subject = new ClaimsIdentity(claims)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken =  tokenHandler.CreateToken(tokenDescriptor);

        var token = tokenHandler.WriteToken(securityToken);

        return token;
    }
}
