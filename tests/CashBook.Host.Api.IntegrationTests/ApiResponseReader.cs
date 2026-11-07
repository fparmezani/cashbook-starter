using System.Text.Json;

namespace CashBook.Host.Api.IntegrationTests;

internal static class ApiResponseReader
{
    /// <summary>
    /// Lê o campo "code" do ProblemDetails (ex.: Account.NameAlreadyExists).
    /// </summary>
    public static async Task<string?> ReadErrorCodeAsync(this HttpResponseMessage response)
    {
        await using var stream = await response.Content.ReadAsStreamAsync();
        using var document = await JsonDocument.ParseAsync(stream);
        return document.RootElement.TryGetProperty("code", out var code) ? code.GetString() : null;
    }
}
