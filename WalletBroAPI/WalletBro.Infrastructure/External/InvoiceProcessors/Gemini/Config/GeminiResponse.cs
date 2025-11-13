using System.Text.Json.Serialization;

namespace WalletBro.Infrastructure.External.InvoiceProcessors.Gemini.Config;

public class Content
{
    [JsonPropertyName("parts")] public List<Part> Parts { get; init; } = [];

    [JsonPropertyName("role")] public string Role { get; init; } = string.Empty;
}

public class Part
{
    [JsonPropertyName("text")] public string Text { get; init; } = string.Empty;
}

public class Candidate
{
    [JsonPropertyName("content")] public Content Content { get; init; } = new();

    [JsonPropertyName("finishReason")] public string FinishReason { get; init; } = string.Empty;
}

public class PromptTokensDetail
{
    [JsonPropertyName("modality")] public string Modality { get; set; } = string.Empty;

    [JsonPropertyName("tokenCount")]
    public int TokenCount { get; set; }
}

public class CandidatesTokensDetail
{
    [JsonPropertyName("modality")] public string Modality { get; init; } = string.Empty;

    [JsonPropertyName("tokenCount")]
    public int TokenCount { get; set; }
}

public class UsageMetadata
{
    [JsonPropertyName("promptTokenCount")]
    public int PromptTokenCount { get; set; }

    [JsonPropertyName("candidatesTokenCount")]
    public int CandidatesTokenCount { get; set; }

    [JsonPropertyName("totalTokenCount")]
    public int TotalTokenCount { get; set; }

    [JsonPropertyName("promptTokensDetails")]
    public List<PromptTokensDetail> PromptTokensDetails { get; init; } = [];

    [JsonPropertyName("candidatesTokensDetails")]
    public List<CandidatesTokensDetail> CandidatesTokensDetails { get; init; } = [];
}

public class GeminiResponse
{
    [JsonPropertyName("candidates")] 
    public IReadOnlyList<Candidate> Candidates { get; init; } = [];

    [JsonPropertyName("usageMetadata")] 
    public UsageMetadata UsageMetadata { get; init; } = new();

    [JsonPropertyName("modelVersion")] 
    public string ModelVersion { get; init; } = string.Empty;

    [JsonPropertyName("responseId")]
    public string ResponseId { get; init; } = string.Empty;
}