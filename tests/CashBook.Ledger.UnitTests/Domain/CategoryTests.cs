using CashBook.Ledger.Domain.Categories;

namespace CashBook.Ledger.UnitTests.Domain;

public sealed class CategoryTests
{
    [Theory]
    [InlineData(CategoryType.Income)]
    [InlineData(CategoryType.Expense)]
    public void Create_WithValidData_ReturnsCategory(CategoryType type)
    {
        var result = Category.Create(" Mercado ", type);

        Assert.True(result.IsSuccess);
        Assert.Equal("Mercado", result.Value.Name);
        Assert.Equal(type, result.Value.Type);
    }

    [Fact]
    public void Create_WithEmptyName_FailsWithNameRequired()
    {
        var result = Category.Create("  ", CategoryType.Expense);

        Assert.Equal("Category.NameRequired", result.Error.Code);
    }

    [Fact]
    public void Create_WithNameAboveMaxLength_FailsWithNameTooLong()
    {
        var result = Category.Create(new string('a', Category.NameMaxLength + 1), CategoryType.Income);

        Assert.Equal("Category.NameTooLong", result.Error.Code);
    }

    [Fact]
    public void Create_WithUndefinedType_FailsWithTypeInvalid()
    {
        var result = Category.Create("Mercado", (CategoryType)42);

        Assert.Equal("Category.TypeInvalid", result.Error.Code);
    }

    [Fact]
    public void Rename_WithValidName_ChangesNameAndKeepsType()
    {
        var category = Category.Create("Mercado", CategoryType.Expense).Value;

        var result = category.Rename("Supermercado");

        Assert.True(result.IsSuccess);
        Assert.Equal("Supermercado", category.Name);
        Assert.Equal(CategoryType.Expense, category.Type);
    }

    [Fact]
    public void Rename_WithEmptyName_FailsWithNameRequired()
    {
        var category = Category.Create("Mercado", CategoryType.Expense).Value;

        var result = category.Rename("");

        Assert.Equal("Category.NameRequired", result.Error.Code);
        Assert.Equal("Mercado", category.Name);
    }
}
