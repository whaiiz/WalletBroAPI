namespace WalletBro.Infrastructure.Authentication.Config;

public class JwtSettings
{
    public string SecretKey { get; set; }
    
    public string Issuer { get; set; }
    
    public string Audience { get; set; }
    
    public uint ExpiresInHours { get; set; }
}