using MediatR;

namespace WalletBro.UseCases.Invoice.ProcessInvoice;

public class ProcessInvoiceCommand() : IRequest<ProcessInvoiceResult>
{
    public string FileName { get; init; } = string.Empty;

    public string ContentType { get; init; } = string.Empty;
    
    public string Base64Content { get; init; } = string.Empty;
}