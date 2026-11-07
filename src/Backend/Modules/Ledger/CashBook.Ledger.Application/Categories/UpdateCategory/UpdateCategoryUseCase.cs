using CashBook.BuildingBlocks.Application;
using CashBook.BuildingBlocks.Results;
using CashBook.Ledger.Domain.Abstractions;
using CashBook.Ledger.Domain.Categories;

namespace CashBook.Ledger.Application.Categories.UpdateCategory;

/// <summary>
/// Só o nome muda. O tipo é fixo após a criação (ver Category).
/// </summary>
public sealed record UpdateCategoryInput(Guid Id, string Name);

public sealed class UpdateCategoryUseCase(ICategoryRepository categories, IUnitOfWork unitOfWork)
    : IUseCase<UpdateCategoryInput, CategoryResponse>
{
    public async Task<Result<CategoryResponse>> ExecuteAsync(UpdateCategoryInput request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var category = await categories.GetByIdAsync(request.Id, cancellationToken);
        if (category is null)
        {
            return CategoryErrors.NotFound;
        }

        var renamed = category.Rename(request.Name);
        if (renamed.IsFailure)
        {
            return renamed.Error;
        }

        if (await categories.NameExistsAsync(category.Name, category.Id, cancellationToken))
        {
            return CategoryErrors.NameAlreadyExists;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return CategoryResponse.From(category);
    }
}
