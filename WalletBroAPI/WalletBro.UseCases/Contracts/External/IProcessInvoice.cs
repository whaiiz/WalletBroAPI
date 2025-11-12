using WalletBro.UseCases.Contracts.External.DTOs;

namespace WalletBro.UseCases.Contracts.External;

public interface IProcessInvoice
{
    Task<ProcessInvoiceResponse> ProcessInvoiceAsync(ProcessInvoiceRequest req);
}