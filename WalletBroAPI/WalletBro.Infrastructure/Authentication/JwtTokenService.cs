using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WalletBro.Infrastructure.Authentication.Config;
using WalletBro.UseCases.Contracts.Authentication;

namespace WalletBro.Infrastructure.Authentication;

public class JwtTokenService(IOptions<JwtSettings> settings) : ITokenService
{
    public string GenerateToken(Core.Entities.User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Value.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Name, user.Id.ToString()),
        };

        var token = new JwtSecurityToken(
            issuer: settings.Value.Issuer,
            audience: settings.Value.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(settings.Value.ExpiresInHours),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}