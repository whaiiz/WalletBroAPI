using MediatR;

namespace WalletBro.UseCases.Invoice.Process;

public class ProcessInvoiceCommand() : IRequest<ProcessInvoiceResult>
{
    public string FileName { get; set; } = string.Empty;

    public string ContentType { get; set; } = string.Empty;
    
    public string Base64Content { get; set; } = string.Empty;
}