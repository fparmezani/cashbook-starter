using CashBook.Ledger.Domain.Abstractions;
using CashBook.Ledger.Domain.Accounts;
using CashBook.Ledger.Domain.Categories;

namespace CashBook.Ledger.UnitTests.Fakes;

internal sealed class FakeUnitOfWork : IUnitOfWork
{
    public int SaveCount { get; private set; }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SaveCount++;
        return Task.CompletedTask;
    }
}

internal sealed class InMemoryAccountRepository : IAccountRepository
{
    public List<Account> Items { get; } = [];

    public Task<Account?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        Task.FromResult(Items.FirstOrDefault(account => account.Id == id));

    public Task<IReadOnlyList<Account>> ListAsync(CancellationToken cancellationToken = default) =>
        Task.FromResult<IReadOnlyList<Account>>([.. Items.OrderBy(account => account.Name, StringComparer.OrdinalIgnoreCase)]);

    public Task<bool> NameExistsAsync(string name, Guid? exceptId = null, CancellationToken cancellationToken = default) =>
        Task.FromResult(Items.Exists(account =>
            string.Equals(account.Name, name, StringComparison.OrdinalIgnoreCase) && account.Id != exceptId));

    public void Add(Account account) => Items.Add(account);
}

internal sealed class InMemoryCategoryRepository : ICategoryRepository
{
    public List<Category> Items { get; } = [];

    public Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        Task.FromResult(Items.FirstOrDefault(category => category.Id == id));

    public Task<IReadOnlyList<Category>> ListAsync(CancellationToken cancellationToken = default) =>
        Task.FromResult<IReadOnlyList<Category>>([.. Items.OrderBy(category => category.Name, StringComparer.OrdinalIgnoreCase)]);

    public Task<bool> NameExistsAsync(string name, Guid? exceptId = null, CancellationToken cancellationToken = default) =>
        Task.FromResult(Items.Exists(category =>
            string.Equals(category.Name, name, StringComparison.OrdinalIgnoreCase) && category.Id != exceptId));

    public void Add(Category category) => Items.Add(category);
}
