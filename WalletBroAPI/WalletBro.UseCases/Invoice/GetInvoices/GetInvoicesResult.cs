namespace WalletBro.UseCases.Invoice.GetInvoices;

public class GetInvoicesResult
{
    public List<Core.Entities.Invoice> Invoices { get; set; } = [];
}