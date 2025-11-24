namespace WalletBro.UseCases.Invoice.ProcessInvoice;

public class ProcessInvoiceResult
{
    public bool IsSuccess { get; set; }

    public string[] ErrorMessages { get; set; } = [];
}