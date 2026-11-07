using System.Net;
using System.Net.Http.Json;

namespace CashBook.Host.Api.IntegrationTests;

public sealed class HealthEndpointTests(CashBookApiFactory factory)
    : IClassFixture<CashBookApiFactory>
{
    [Fact]
    public async Task Get_Health_Returns200WithHealthyStatus()
    {
        using var client = factory.CreateClient();

        var response = await client.GetAsync(new Uri("/health", UriKind.Relative));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var body = await response.Content.ReadFromJsonAsync<HealthBody>();
        Assert.Equal("Healthy", body?.Status);
    }

    [Fact]
    public async Task Get_Health_FromAngularOrigin_ReturnsCorsHeader()
    {
        using var client = factory.CreateClient();
        using var request = new HttpRequestMessage(HttpMethod.Get, new Uri("/health", UriKind.Relative));
        request.Headers.Add("Origin", "http://localhost:4200");

        var response = await client.SendAsync(request);

        Assert.True(response.Headers.TryGetValues("Access-Control-Allow-Origin", out var origins));
        Assert.Contains("http://localhost:4200", origins);
    }

    private sealed record HealthBody(string Status);
}
