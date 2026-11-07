namespace CashBook.Ledger.Domain.Categories;

public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Category>> ListAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifica nome duplicado (sem diferenciar maiúsculas). <paramref name="exceptId"/> ignora a própria categoria numa edição.
    /// </summary>
    Task<bool> NameExistsAsync(string name, Guid? exceptId = null, CancellationToken cancellationToken = default);

    void Add(Category category);
}
