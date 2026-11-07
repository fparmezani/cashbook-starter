namespace CashBook.Ledger.Domain.Accounts;

public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Account>> ListAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifica nome duplicado (sem diferenciar maiúsculas). <paramref name="exceptId"/> ignora a própria conta numa edição.
    /// </summary>
    Task<bool> NameExistsAsync(string name, Guid? exceptId = null, CancellationToken cancellationToken = default);

    void Add(Account account);
}
