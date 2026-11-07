using System.Net;
using System.Net.Http.Json;

namespace CashBook.Host.Api.IntegrationTests;

public sealed class CategoryEndpointsTests(CashBookApiFactory factory) : IClassFixture<CashBookApiFactory>
{
    private static readonly Uri CategoriesUri = new("/api/categories", UriKind.Relative);

    [Fact]
    public async Task Post_Category_Returns201WithTypeAsString()
    {
        using var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync(CategoriesUri, new { name = "Salário", type = "Income" });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var created = await response.Content.ReadFromJsonAsync<CategoryBody>();
        Assert.Equal("Salário", created?.Name);
        Assert.Equal("Income", created?.Type);

        var all = await client.GetFromJsonAsync<List<CategoryBody>>(CategoriesUri);
        Assert.Contains(created!, all!);
    }

    [Fact]
    public async Task Post_DuplicateCategoryName_Returns409WithNameAlreadyExists()
    {
        using var client = factory.CreateClient();
        var first = await client.PostAsJsonAsync(CategoriesUri, new { name = "Mercado", type = "Expense" });
        Assert.Equal(HttpStatusCode.Created, first.StatusCode);

        var duplicate = await client.PostAsJsonAsync(CategoriesUri, new { name = "mercado", type = "Expense" });

        Assert.Equal(HttpStatusCode.Conflict, duplicate.StatusCode);
        Assert.Equal("Category.NameAlreadyExists", await duplicate.ReadErrorCodeAsync());
    }

    [Fact]
    public async Task Post_EmptyCategoryName_Returns400WithNameRequired()
    {
        using var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync(CategoriesUri, new { name = "", type = "Expense" });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("Category.NameRequired", await response.ReadErrorCodeAsync());
    }

    [Fact]
    public async Task Put_Category_RenamesKeepingType()
    {
        using var client = factory.CreateClient();
        var created = await (await client.PostAsJsonAsync(CategoriesUri, new { name = "Aluguel", type = "Expense" }))
            .Content.ReadFromJsonAsync<CategoryBody>();

        var response = await client.PutAsJsonAsync(
            new Uri($"/api/categories/{created!.Id}", UriKind.Relative),
            new { name = "Aluguel e condomínio" });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var updated = await response.Content.ReadFromJsonAsync<CategoryBody>();
        Assert.Equal(new CategoryBody(created.Id, "Aluguel e condomínio", "Expense"), updated);
    }

    private sealed record CategoryBody(Guid Id, string Name, string Type);
}
