namespace WalletBro.Core.Exceptions;

public class InvoiceNotFoundException : Exception
{
    public string InvoiceId { get; }
    public string? UserId { get; }

    public InvoiceNotFoundException(string invoiceId)
        : base($"Invoice with ID '{invoiceId}' was not found.")
    {
        InvoiceId = invoiceId;
    }

    public InvoiceNotFoundException(string invoiceId, string userId)
        : base($"Invoice with ID '{invoiceId}' was not found for user '{userId}'.")
    {
        InvoiceId = invoiceId;
        UserId = userId;
    }

    public InvoiceNotFoundException(string invoiceId, string message, Exception innerException)
        : base(message, innerException)
    {
        InvoiceId = invoiceId;
    }
}