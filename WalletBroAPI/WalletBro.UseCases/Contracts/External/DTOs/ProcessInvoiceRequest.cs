namespace WalletBro.UseCases.Contracts.External.DTOs;

public class ProcessInvoiceRequest
{
    public string FileName { get; set; } = string.Empty;

    public string ContentType { get; set; } = string.Empty;
    
    public string Base64Content { get; set; } = string.Empty;
}