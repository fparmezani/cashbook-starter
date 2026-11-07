using CashBook.Ledger.Domain.Categories;

namespace CashBook.Ledger.Application.Categories.ListCategories;

/// <summary>
/// Listagem não tem regra que possa falhar: devolve a lista direto, sem Result.
/// </summary>
public sealed class ListCategoriesUseCase(ICategoryRepository categories)
{
    public async Task<IReadOnlyList<CategoryResponse>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var all = await categories.ListAsync(cancellationToken);
        return [.. all.Select(CategoryResponse.From)];
    }
}
