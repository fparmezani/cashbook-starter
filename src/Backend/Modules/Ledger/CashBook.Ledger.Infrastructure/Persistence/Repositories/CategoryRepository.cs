using CashBook.Ledger.Domain.Categories;
using Microsoft.EntityFrameworkCore;

namespace CashBook.Ledger.Infrastructure.Persistence.Repositories;

internal sealed class CategoryRepository(LedgerDbContext dbContext) : ICategoryRepository
{
    public Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        dbContext.Categories.FirstOrDefaultAsync(category => category.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Category>> ListAsync(CancellationToken cancellationToken = default) =>
        await dbContext.Categories
            .AsNoTracking()
            .OrderBy(category => category.Name)
            .ToListAsync(cancellationToken);

    // A coluna usa collation NOCASE, então a igualdade já ignora maiúsculas no SQLite.
    public Task<bool> NameExistsAsync(string name, Guid? exceptId = null, CancellationToken cancellationToken = default) =>
        dbContext.Categories.AnyAsync(
            category => category.Name == name && (exceptId == null || category.Id != exceptId),
            cancellationToken);

    public void Add(Category category) => dbContext.Categories.Add(category);
}
