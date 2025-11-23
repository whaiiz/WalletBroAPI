namespace WalletBro.UseCases.Contracts.Authentication;

public interface ICurrentUserService
{
    string? Email { get; }
    Guid UserId { get; }
}