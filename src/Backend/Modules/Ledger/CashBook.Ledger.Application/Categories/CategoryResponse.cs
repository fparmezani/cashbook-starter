using CashBook.Ledger.Domain.Categories;

namespace CashBook.Ledger.Application.Categories;

public sealed record CategoryResponse(Guid Id, string Name, CategoryType Type)
{
    public static CategoryResponse From(Category category)
    {
        ArgumentNullException.ThrowIfNull(category);
        return new(category.Id, category.Name, category.Type);
    }
}
