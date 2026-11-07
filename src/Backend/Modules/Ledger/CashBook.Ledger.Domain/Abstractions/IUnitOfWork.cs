namespace CashBook.Ledger.Domain.Abstractions;

/// <summary>
/// Confirma as mudanças feitas nos repositórios em uma única transação.
/// </summary>
public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
