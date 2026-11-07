using CashBook.BuildingBlocks.Api.Results;
using CashBook.Ledger.Application.Categories.CreateCategory;
using CashBook.Ledger.Application.Categories.ListCategories;
using CashBook.Ledger.Application.Categories.UpdateCategory;
using CashBook.Ledger.Domain.Categories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CashBook.Ledger.Api.Endpoints;

public sealed record CreateCategoryRequest(string Name, CategoryType Type);

public sealed record UpdateCategoryRequest(string Name);

internal static class CategoryEndpoints
{
    public static IEndpointRouteBuilder MapCategoryEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/categories").WithTags("Categories");

        group.MapGet("/", async (ListCategoriesUseCase useCase, CancellationToken cancellationToken) =>
            TypedResults.Ok(await useCase.ExecuteAsync(cancellationToken)))
            .WithName("ListCategories");

        group.MapPost("/", async (CreateCategoryRequest request, CreateCategoryUseCase useCase, CancellationToken cancellationToken) =>
            (await useCase.ExecuteAsync(new CreateCategoryInput(request.Name, request.Type), cancellationToken))
                .ToHttpResult(category => TypedResults.Created($"/api/categories/{category.Id}", category)))
            .WithName("CreateCategory");

        group.MapPut("/{id:guid}", async (Guid id, UpdateCategoryRequest request, UpdateCategoryUseCase useCase, CancellationToken cancellationToken) =>
            (await useCase.ExecuteAsync(new UpdateCategoryInput(id, request.Name), cancellationToken)).ToHttpResult())
            .WithName("UpdateCategory");

        return endpoints;
    }
}
