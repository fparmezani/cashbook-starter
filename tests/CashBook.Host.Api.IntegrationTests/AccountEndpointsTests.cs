using System.Net;
using System.Net.Http.Json;

namespace CashBook.Host.Api.IntegrationTests;

public sealed class AccountEndpointsTests(CashBookApiFactory factory) : IClassFixture<CashBookApiFactory>
{
    private static readonly Uri AccountsUri = new("/api/accounts", UriKind.Relative);

    [Fact]
    public async Task Post_Account_Returns201WithLocationAndPersists()
    {
        using var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync(AccountsUri, new { name = "Conta corrente" });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var created = await response.Content.ReadFromJsonAsync<AccountBody>();
        Assert.NotNull(created);
        Assert.Equal("Conta corrente", created.Name);
        Assert.True(created.IsActive);
        Assert.NotNull(response.Headers.Location);

        var fetched = await client.GetFromJsonAsync<AccountBody>(response.Headers.Location);
        Assert.Equal(created, fetched);

        var all = await client.GetFromJsonAsync<List<AccountBody>>(AccountsUri);
        Assert.Contains(created, all!);
    }

    [Fact]
    public async Task Post_DuplicateAccountName_Returns409WithNameAlreadyExists()
    {
        using var client = factory.CreateClient();
        var first = await client.PostAsJsonAsync(AccountsUri, new { name = "Carteira" });
        Assert.Equal(HttpStatusCode.Created, first.StatusCode);

        var duplicate = await client.PostAsJsonAsync(AccountsUri, new { name = "  CARTEIRA " });

        Assert.Equal(HttpStatusCode.Conflict, duplicate.StatusCode);
        Assert.Equal("Account.NameAlreadyExists", await duplicate.ReadErrorCodeAsync());
    }

    [Fact]
    public async Task Post_EmptyAccountName_Returns400WithNameRequired()
    {
        using var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync(AccountsUri, new { name = "   " });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("Account.NameRequired", await response.ReadErrorCodeAsync());
    }

    [Fact]
    public async Task Put_Account_UpdatesNameAndStatus()
    {
        using var client = factory.CreateClient();
        var created = await (await client.PostAsJsonAsync(AccountsUri, new { name = "Poupança" }))
            .Content.ReadFromJsonAsync<AccountBody>();

        var response = await client.PutAsJsonAsync(
            new Uri($"/api/accounts/{created!.Id}", UriKind.Relative),
            new { name = "Poupança antiga", isActive = false });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var updated = await response.Content.ReadFromJsonAsync<AccountBody>();
        Assert.Equal(new AccountBody(created.Id, "Poupança antiga", false), updated);
    }

    [Fact]
    public async Task Get_UnknownAccount_Returns404WithNotFound()
    {
        using var client = factory.CreateClient();

        var response = await client.GetAsync(new Uri($"/api/accounts/{Guid.NewGuid()}", UriKind.Relative));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Equal("Account.NotFound", await response.ReadErrorCodeAsync());
    }

    private sealed record AccountBody(Guid Id, string Name, bool IsActive);
}
