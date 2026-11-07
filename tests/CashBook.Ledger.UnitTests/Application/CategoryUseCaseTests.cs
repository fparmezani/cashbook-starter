using CashBook.Ledger.Application.Categories.CreateCategory;
using CashBook.Ledger.Application.Categories.ListCategories;
using CashBook.Ledger.Application.Categories.UpdateCategory;
using CashBook.Ledger.Domain.Categories;
using CashBook.Ledger.UnitTests.Fakes;

namespace CashBook.Ledger.UnitTests.Application;

public sealed class CategoryUseCaseTests
{
    private readonly InMemoryCategoryRepository _categories = new();
    private readonly FakeUnitOfWork _unitOfWork = new();

    [Fact]
    public async Task CreateCategory_WithNewName_PersistsAndReturnsCategory()
    {
        var useCase = new CreateCategoryUseCase(_categories, _unitOfWork);

        var result = await useCase.ExecuteAsync(new CreateCategoryInput("Salário", CategoryType.Income));

        Assert.True(result.IsSuccess);
        Assert.Equal("Salário", result.Value.Name);
        Assert.Equal(CategoryType.Income, result.Value.Type);
        Assert.Equal(1, _unitOfWork.SaveCount);
    }

    [Fact]
    public async Task CreateCategory_WithDuplicateName_FailsWithNameAlreadyExists()
    {
        _categories.Add(Category.Create("Mercado", CategoryType.Expense).Value);
        var useCase = new CreateCategoryUseCase(_categories, _unitOfWork);

        var result = await useCase.ExecuteAsync(new CreateCategoryInput("MERCADO", CategoryType.Expense));

        Assert.Equal("Category.NameAlreadyExists", result.Error.Code);
        Assert.Equal(0, _unitOfWork.SaveCount);
    }

    [Fact]
    public async Task CreateCategory_WithUndefinedType_FailsWithTypeInvalid()
    {
        var useCase = new CreateCategoryUseCase(_categories, _unitOfWork);

        var result = await useCase.ExecuteAsync(new CreateCategoryInput("Mercado", (CategoryType)7));

        Assert.Equal("Category.TypeInvalid", result.Error.Code);
        Assert.Empty(_categories.Items);
    }

    [Fact]
    public async Task ListCategories_ReturnsCategoriesOrderedByName()
    {
        _categories.Add(Category.Create("Salário", CategoryType.Income).Value);
        _categories.Add(Category.Create("Aluguel", CategoryType.Expense).Value);
        var useCase = new ListCategoriesUseCase(_categories);

        var result = await useCase.ExecuteAsync();

        Assert.Equal(["Aluguel", "Salário"], result.Select(category => category.Name));
    }

    [Fact]
    public async Task UpdateCategory_WithValidName_RenamesAndSaves()
    {
        var category = Category.Create("Mercado", CategoryType.Expense).Value;
        _categories.Add(category);
        var useCase = new UpdateCategoryUseCase(_categories, _unitOfWork);

        var result = await useCase.ExecuteAsync(new UpdateCategoryInput(category.Id, "Supermercado"));

        Assert.True(result.IsSuccess);
        Assert.Equal("Supermercado", result.Value.Name);
        Assert.Equal(CategoryType.Expense, result.Value.Type);
        Assert.Equal(1, _unitOfWork.SaveCount);
    }

    [Fact]
    public async Task UpdateCategory_WithUnknownId_FailsWithNotFound()
    {
        var useCase = new UpdateCategoryUseCase(_categories, _unitOfWork);

        var result = await useCase.ExecuteAsync(new UpdateCategoryInput(Guid.NewGuid(), "Mercado"));

        Assert.Equal("Category.NotFound", result.Error.Code);
    }
}
