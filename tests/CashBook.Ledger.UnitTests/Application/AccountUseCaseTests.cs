using CashBook.Ledger.Application.Accounts.CreateAccount;
using CashBook.Ledger.Application.Accounts.GetAccount;
using CashBook.Ledger.Application.Accounts.ListAccounts;
using CashBook.Ledger.Application.Accounts.UpdateAccount;
using CashBook.Ledger.Domain.Accounts;
using CashBook.Ledger.UnitTests.Fakes;

namespace CashBook.Ledger.UnitTests.Application;

public sealed class AccountUseCaseTests
{
    private readonly InMemoryAccountRepository _accounts = new();
    private readonly FakeUnitOfWork _unitOfWork = new();

    [Fact]
    public async Task CreateAccount_WithNewName_PersistsAndReturnsAccount()
    {
        var useCase = new CreateAccountUseCase(_accounts, _unitOfWork);

        var result = await useCase.ExecuteAsync(new CreateAccountInput("Carteira"));

        Assert.True(result.IsSuccess);
        Assert.Equal("Carteira", result.Value.Name);
        Assert.True(result.Value.IsActive);
        Assert.Single(_accounts.Items);
        Assert.Equal(1, _unitOfWork.SaveCount);
    }

    [Fact]
    public async Task CreateAccount_WithDuplicateNameIgnoringCase_FailsWithNameAlreadyExists()
    {
        _accounts.Add(Account.Create("Carteira").Value);
        var useCase = new CreateAccountUseCase(_accounts, _unitOfWork);

        var result = await useCase.ExecuteAsync(new CreateAccountInput(" carteira "));

        Assert.Equal("Account.NameAlreadyExists", result.Error.Code);
        Assert.Single(_accounts.Items);
        Assert.Equal(0, _unitOfWork.SaveCount);
    }

    [Fact]
    public async Task CreateAccount_WithEmptyName_FailsWithDomainError()
    {
        var useCase = new CreateAccountUseCase(_accounts, _unitOfWork);

        var result = await useCase.ExecuteAsync(new CreateAccountInput(""));

        Assert.Equal("Account.NameRequired", result.Error.Code);
        Assert.Empty(_accounts.Items);
    }

    [Fact]
    public async Task ListAccounts_ReturnsAccountsOrderedByName()
    {
        _accounts.Add(Account.Create("Poupança").Value);
        _accounts.Add(Account.Create("Carteira").Value);
        var useCase = new ListAccountsUseCase(_accounts);

        var result = await useCase.ExecuteAsync();

        Assert.Equal(["Carteira", "Poupança"], result.Select(account => account.Name));
    }

    [Fact]
    public async Task GetAccount_WithUnknownId_FailsWithNotFound()
    {
        var useCase = new GetAccountUseCase(_accounts);

        var result = await useCase.ExecuteAsync(new GetAccountInput(Guid.NewGuid()));

        Assert.Equal("Account.NotFound", result.Error.Code);
    }

    [Fact]
    public async Task UpdateAccount_WithValidData_SavesChanges()
    {
        var account = Account.Create("Carteira").Value;
        _accounts.Add(account);
        var useCase = new UpdateAccountUseCase(_accounts, _unitOfWork);

        var result = await useCase.ExecuteAsync(new UpdateAccountInput(account.Id, "Carteira física", IsActive: false));

        Assert.True(result.IsSuccess);
        Assert.Equal("Carteira física", result.Value.Name);
        Assert.False(result.Value.IsActive);
        Assert.Equal(1, _unitOfWork.SaveCount);
    }

    [Fact]
    public async Task UpdateAccount_KeepingOwnName_Succeeds()
    {
        var account = Account.Create("Carteira").Value;
        _accounts.Add(account);
        var useCase = new UpdateAccountUseCase(_accounts, _unitOfWork);

        var result = await useCase.ExecuteAsync(new UpdateAccountInput(account.Id, "Carteira", IsActive: false));

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateAccount_ToNameOfAnotherAccount_FailsWithNameAlreadyExists()
    {
        _accounts.Add(Account.Create("Carteira").Value);
        var savings = Account.Create("Poupança").Value;
        _accounts.Add(savings);
        var useCase = new UpdateAccountUseCase(_accounts, _unitOfWork);

        var result = await useCase.ExecuteAsync(new UpdateAccountInput(savings.Id, "CARTEIRA", IsActive: true));

        Assert.Equal("Account.NameAlreadyExists", result.Error.Code);
        Assert.Equal(0, _unitOfWork.SaveCount);
    }

    [Fact]
    public async Task UpdateAccount_WithUnknownId_FailsWithNotFound()
    {
        var useCase = new UpdateAccountUseCase(_accounts, _unitOfWork);

        var result = await useCase.ExecuteAsync(new UpdateAccountInput(Guid.NewGuid(), "Carteira", IsActive: true));

        Assert.Equal("Account.NotFound", result.Error.Code);
    }
}
