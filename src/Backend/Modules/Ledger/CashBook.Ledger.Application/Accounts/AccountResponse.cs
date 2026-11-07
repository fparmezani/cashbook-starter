using CashBook.Ledger.Domain.Accounts;

namespace CashBook.Ledger.Application.Accounts;

public sealed record AccountResponse(Guid Id, string Name, bool IsActive)
{
    public static AccountResponse From(Account account)
    {
        ArgumentNullException.ThrowIfNull(account);
        return new(account.Id, account.Name, account.IsActive);
    }
}
