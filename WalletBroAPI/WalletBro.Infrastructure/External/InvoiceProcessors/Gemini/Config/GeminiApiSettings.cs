using System.ComponentModel.DataAnnotations;

namespace WalletBro.Infrastructure.External.InvoiceProcessors.Gemini.Config;

public class GeminiApiSettings
{
    public required string Url { get; init; }
    public required string PromptProcessInvoice { get; init; }
    public required string ApiKey { get; init; }
}