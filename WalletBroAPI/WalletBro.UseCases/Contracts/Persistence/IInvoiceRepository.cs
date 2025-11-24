namespace WalletBro.UseCases.Contracts.Persistence;

public interface IInvoiceRepository
{
    Task<string> AddAsync(Core.Entities.Invoice invoice);
    
    Task<Core.Entities.Invoice> GetByIdAsync(string id, Guid userId);
    
    Task<IEnumerable<Core.Entities.Invoice>> GetAllAsync(Guid userId);
}