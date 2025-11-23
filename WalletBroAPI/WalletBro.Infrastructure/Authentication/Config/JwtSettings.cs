namespace WalletBro.Infrastructure.Authentication.Config;

public class JwtSettings
{
    public required string SecretKey { get; init; }
    
    public required string Issuer { get; init; }
    
    public required string Audience { get; init; }
    
    public uint ExpiresInHours { get; init; }
}