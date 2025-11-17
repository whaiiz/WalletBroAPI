using WalletBro.Core.Entities;

namespace WalletBro.UseCases.Contracts.Persistence;

public interface IUserRepository
{
    Task<Core.Entities.User?> GetByEmailAsync(string email);
    
    Task<bool> AddAsync(Core.Entities.User user);
}