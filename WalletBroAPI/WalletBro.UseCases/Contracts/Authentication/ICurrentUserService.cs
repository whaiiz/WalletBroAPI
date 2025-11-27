namespace WalletBro.UseCases.Contracts.Authentication;

public interface ICurrentUserService
{
    Guid UserId { get; }
}