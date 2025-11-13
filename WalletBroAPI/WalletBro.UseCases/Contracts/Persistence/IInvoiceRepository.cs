namespace WalletBro.UseCases.Contracts.Persistence;

public interface IInvoiceRepository
{
    Task<int> AddAsync(Core.Entities.Invoice invoice);
}