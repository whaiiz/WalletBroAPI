using WalletBroAPI.Dtos.Invoice;

namespace WalletBroAPI.Invoice;

public class GetInvoiceByIdByIdResponse
{
    public InvoiceDto Invoice { get; set; } = new();
}