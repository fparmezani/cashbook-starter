using CashBook.BuildingBlocks.Application;
using CashBook.BuildingBlocks.Results;
using CashBook.Ledger.Domain.Abstractions;
using CashBook.Ledger.Domain.Categories;

namespace CashBook.Ledger.Application.Categories.CreateCategory;

public sealed record CreateCategoryInput(string Name, CategoryType Type);

public sealed class CreateCategoryUseCase(ICategoryRepository categories, IUnitOfWork unitOfWork)
    : IUseCase<CreateCategoryInput, CategoryResponse>
{
    public async Task<Result<CategoryResponse>> ExecuteAsync(CreateCategoryInput request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var created = Category.Create(request.Name, request.Type);
        if (created.IsFailure)
        {
            return created.Error;
        }

        var category = created.Value;
        if (await categories.NameExistsAsync(category.Name, cancellationToken: cancellationToken))
        {
            return CategoryErrors.NameAlreadyExists;
        }

        categories.Add(category);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return CategoryResponse.From(category);
    }
}
