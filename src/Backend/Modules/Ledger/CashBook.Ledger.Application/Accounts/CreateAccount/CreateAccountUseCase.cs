using CashBook.BuildingBlocks.Application;
using CashBook.BuildingBlocks.Results;
using CashBook.Ledger.Domain.Abstractions;
using CashBook.Ledger.Domain.Accounts;

namespace CashBook.Ledger.Application.Accounts.CreateAccount;

public sealed record CreateAccountInput(string Name);

public sealed class CreateAccountUseCase(IAccountRepository accounts, IUnitOfWork unitOfWork)
    : IUseCase<CreateAccountInput, AccountResponse>
{
    public async Task<Result<AccountResponse>> ExecuteAsync(CreateAccountInput request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var created = Account.Create(request.Name);
        if (created.IsFailure)
        {
            return created.Error;
        }

        var account = created.Value;
        if (await accounts.NameExistsAsync(account.Name, cancellationToken: cancellationToken))
        {
            return AccountErrors.NameAlreadyExists;
        }

        accounts.Add(account);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return AccountResponse.From(account);
    }
}
