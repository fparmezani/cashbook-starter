using CashBook.Ledger.Domain.Accounts;

namespace CashBook.Ledger.Application.Accounts.ListAccounts;

/// <summary>
/// Listagem não tem regra que possa falhar: devolve a lista direto, sem Result.
/// </summary>
public sealed class ListAccountsUseCase(IAccountRepository accounts)
{
    public async Task<IReadOnlyList<AccountResponse>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var all = await accounts.ListAsync(cancellationToken);
        return [.. all.Select(AccountResponse.From)];
    }
}
