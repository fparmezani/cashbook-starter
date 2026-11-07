using CashBook.BuildingBlocks.Application;
using CashBook.BuildingBlocks.Results;
using CashBook.Ledger.Domain.Abstractions;
using CashBook.Ledger.Domain.Accounts;

namespace CashBook.Ledger.Application.Accounts.UpdateAccount;

public sealed record UpdateAccountInput(Guid Id, string Name, bool IsActive);

public sealed class UpdateAccountUseCase(IAccountRepository accounts, IUnitOfWork unitOfWork)
    : IUseCase<UpdateAccountInput, AccountResponse>
{
    public async Task<Result<AccountResponse>> ExecuteAsync(UpdateAccountInput request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var account = await accounts.GetByIdAsync(request.Id, cancellationToken);
        if (account is null)
        {
            return AccountErrors.NotFound;
        }

        var updated = account.Update(request.Name, request.IsActive);
        if (updated.IsFailure)
        {
            return updated.Error;
        }

        if (await accounts.NameExistsAsync(account.Name, account.Id, cancellationToken))
        {
            return AccountErrors.NameAlreadyExists;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return AccountResponse.From(account);
    }
}
