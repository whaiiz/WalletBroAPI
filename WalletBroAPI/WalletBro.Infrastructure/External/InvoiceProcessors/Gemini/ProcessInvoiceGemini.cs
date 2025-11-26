using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using WalletBro.Infrastructure.External.InvoiceProcessors.Gemini.Config;
using WalletBro.UseCases.Contracts.External;
using WalletBro.UseCases.Contracts.External.DTOs;

namespace WalletBro.Infrastructure.External.InvoiceProcessors.Gemini;

public partial class ProcessInvoiceGemini(IOptions<GeminiApiSettings> settings) : IProcessInvoice
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
    
    public async Task<ProcessInvoiceResponse> ProcessInvoiceAsync(ProcessInvoiceRequest req)
    {
        var response =  new ProcessInvoiceResponse() { IsSuccess = false };
        var jsonContent = GetGeminiRequestContent(req);
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("x-goog-api-key", settings.Value.ApiKey);
        
        try
        {
            var geminiResponse = await httpClient.PostAsync(settings.Value.Url, jsonContent);
            geminiResponse.EnsureSuccessStatusCode();
            var responseBody = await geminiResponse.Content.ReadAsStringAsync();
            var geminiResponseObj = JsonSerializer.Deserialize<GeminiResponse>(responseBody);
            var text = geminiResponseObj?.Candidates.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text;
            
            if (string.IsNullOrEmpty(text)) throw new JsonException("Text is empty!");
            
            var cleanedJson = MyRegex().Replace(text, "").Trim();
            var invoiceData = JsonSerializer.Deserialize<InvoiceData>(cleanedJson, JsonOptions);

            if (invoiceData == null) return response;
            
            response.IsSuccess = true;
            response.InvoiceData = invoiceData;

            return response;
        }
        catch (Exception)
        {
            return response;
        }
    }
    
    private StringContent GetGeminiRequestContent(ProcessInvoiceRequest req)
    {
        var requestBody = new
        {
            contents = new[]
            {
                new
                {
                    parts = new object[]
                    {
                        new { text = settings.Value.PromptProcessInvoice }, 
                        new
                        {
                            inlineData = new
                            {
                                mimeType = req.ContentType,
                                data = req.Base64Content
                            }
                        }
                    }
                }
            }
        };
        
        return new StringContent(
            JsonSerializer.Serialize(requestBody, JsonOptions),
            Encoding.UTF8,
            "application/json"
        );
    }

    [GeneratedRegex(@"^```json|```$", RegexOptions.Multiline)]
    private static partial Regex MyRegex();
}