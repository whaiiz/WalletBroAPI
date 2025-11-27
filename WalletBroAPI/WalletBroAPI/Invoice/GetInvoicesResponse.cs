using WalletBroAPI.Dtos.Invoice;

namespace WalletBroAPI.Invoice;

public class GetInvoicesResponse
{
    public List<InvoiceDto> Invoices { get; set; } = [];
}