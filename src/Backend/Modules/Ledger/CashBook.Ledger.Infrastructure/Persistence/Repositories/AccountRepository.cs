using CashBook.Ledger.Domain.Accounts;
using Microsoft.EntityFrameworkCore;

namespace CashBook.Ledger.Infrastructure.Persistence.Repositories;

internal sealed class AccountRepository(LedgerDbContext dbContext) : IAccountRepository
{
    public Task<Account?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        dbContext.Accounts.FirstOrDefaultAsync(account => account.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Account>> ListAsync(CancellationToken cancellationToken = default) =>
        await dbContext.Accounts
            .AsNoTracking()
            .OrderBy(account => account.Name)
            .ToListAsync(cancellationToken);

    // A coluna usa collation NOCASE, então a igualdade já ignora maiúsculas no SQLite.
    public Task<bool> NameExistsAsync(string name, Guid? exceptId = null, CancellationToken cancellationToken = default) =>
        dbContext.Accounts.AnyAsync(
            account => account.Name == name && (exceptId == null || account.Id != exceptId),
            cancellationToken);

    public void Add(Account account) => dbContext.Accounts.Add(account);
}
