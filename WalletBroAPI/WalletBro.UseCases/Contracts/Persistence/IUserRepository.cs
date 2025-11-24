namespace WalletBro.UseCases.Contracts.Persistence;

public interface IUserRepository
{
    Task<Core.Entities.User?> GetByEmailAsync(string email);
    
    Task<Core.Entities.User?> GetById(string id);
    
    Task<bool> AddAsync(Core.Entities.User user);
}