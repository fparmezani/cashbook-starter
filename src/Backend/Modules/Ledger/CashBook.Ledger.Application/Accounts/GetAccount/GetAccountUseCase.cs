using CashBook.BuildingBlocks.Application;
using CashBook.BuildingBlocks.Results;
using CashBook.Ledger.Domain.Accounts;

namespace CashBook.Ledger.Application.Accounts.GetAccount;

public sealed record GetAccountInput(Guid Id);

public sealed class GetAccountUseCase(IAccountRepository accounts)
    : IUseCase<GetAccountInput, AccountResponse>
{
    public async Task<Result<AccountResponse>> ExecuteAsync(GetAccountInput request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var account = await accounts.GetByIdAsync(request.Id, cancellationToken);
        if (account is null)
        {
            return AccountErrors.NotFound;
        }

        return AccountResponse.From(account);
    }
}
