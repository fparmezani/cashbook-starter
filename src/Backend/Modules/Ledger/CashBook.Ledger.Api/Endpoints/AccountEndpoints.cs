using CashBook.BuildingBlocks.Api.Results;
using CashBook.Ledger.Application.Accounts.CreateAccount;
using CashBook.Ledger.Application.Accounts.GetAccount;
using CashBook.Ledger.Application.Accounts.ListAccounts;
using CashBook.Ledger.Application.Accounts.UpdateAccount;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CashBook.Ledger.Api.Endpoints;

public sealed record CreateAccountRequest(string Name);

public sealed record UpdateAccountRequest(string Name, bool IsActive);

internal static class AccountEndpoints
{
    private const string GetAccountRouteName = "GetAccount";

    public static IEndpointRouteBuilder MapAccountEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/accounts").WithTags("Accounts");

        group.MapGet("/", async (ListAccountsUseCase useCase, CancellationToken cancellationToken) =>
            TypedResults.Ok(await useCase.ExecuteAsync(cancellationToken)))
            .WithName("ListAccounts");

        group.MapGet("/{id:guid}", async (Guid id, GetAccountUseCase useCase, CancellationToken cancellationToken) =>
            (await useCase.ExecuteAsync(new GetAccountInput(id), cancellationToken)).ToHttpResult())
            .WithName(GetAccountRouteName);

        group.MapPost("/", async (CreateAccountRequest request, CreateAccountUseCase useCase, CancellationToken cancellationToken) =>
            (await useCase.ExecuteAsync(new CreateAccountInput(request.Name), cancellationToken))
                .ToHttpResult(account => TypedResults.CreatedAtRoute(account, GetAccountRouteName, new { id = account.Id })))
            .WithName("CreateAccount");

        group.MapPut("/{id:guid}", async (Guid id, UpdateAccountRequest request, UpdateAccountUseCase useCase, CancellationToken cancellationToken) =>
            (await useCase.ExecuteAsync(new UpdateAccountInput(id, request.Name, request.IsActive), cancellationToken)).ToHttpResult())
            .WithName("UpdateAccount");

        return endpoints;
    }
}
