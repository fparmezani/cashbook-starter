using CashBook.Ledger.Domain.Abstractions;

namespace CashBook.Ledger.Infrastructure.Persistence;

internal sealed class UnitOfWork(LedgerDbContext dbContext) : IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        dbContext.SaveChangesAsync(cancellationToken);
}
