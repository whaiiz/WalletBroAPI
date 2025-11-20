namespace WalletBro.UseCases.Invoice.Process;

public class ProcessInvoiceResult
{
    public bool IsSuccess { get; set; } = false;

    public string[] ErrorMessages { get; set; } = [];
}