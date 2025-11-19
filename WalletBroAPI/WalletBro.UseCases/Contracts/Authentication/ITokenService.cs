namespace WalletBro.UseCases.Contracts.Authentication;

public interface ITokenService
{
    public string GenerateToken(string email);
}