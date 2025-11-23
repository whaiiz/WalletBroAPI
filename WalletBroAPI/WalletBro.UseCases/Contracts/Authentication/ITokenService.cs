namespace WalletBro.UseCases.Contracts.Authentication;

public interface ITokenService
{
    public string GenerateToken(Core.Entities.User user);
}