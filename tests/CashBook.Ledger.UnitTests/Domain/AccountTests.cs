using CashBook.Ledger.Domain.Accounts;

namespace CashBook.Ledger.UnitTests.Domain;

public sealed class AccountTests
{
    [Fact]
    public void Create_WithValidName_ReturnsActiveAccountWithTrimmedName()
    {
        var result = Account.Create("  Carteira  ");

        Assert.True(result.IsSuccess);
        Assert.Equal("Carteira", result.Value.Name);
        Assert.True(result.Value.IsActive);
        Assert.NotEqual(Guid.Empty, result.Value.Id);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Create_WithEmptyName_FailsWithNameRequired(string? name)
    {
        var result = Account.Create(name!);

        Assert.True(result.IsFailure);
        Assert.Equal("Account.NameRequired", result.Error.Code);
    }

    [Fact]
    public void Create_WithNameAboveMaxLength_FailsWithNameTooLong()
    {
        var result = Account.Create(new string('a', Account.NameMaxLength + 1));

        Assert.True(result.IsFailure);
        Assert.Equal("Account.NameTooLong", result.Error.Code);
    }

    [Fact]
    public void Update_WithValidData_ChangesNameAndStatus()
    {
        var account = Account.Create("Carteira").Value;

        var result = account.Update(" Conta corrente ", isActive: false);

        Assert.True(result.IsSuccess);
        Assert.Equal("Conta corrente", account.Name);
        Assert.False(account.IsActive);
    }

    [Fact]
    public void Update_WithEmptyName_FailsAndKeepsPreviousState()
    {
        var account = Account.Create("Carteira").Value;

        var result = account.Update(" ", isActive: false);

        Assert.Equal("Account.NameRequired", result.Error.Code);
        Assert.Equal("Carteira", account.Name);
        Assert.True(account.IsActive);
    }
}
