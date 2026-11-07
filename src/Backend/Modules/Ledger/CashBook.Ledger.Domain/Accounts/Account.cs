using CashBook.BuildingBlocks.Domain;
using CashBook.BuildingBlocks.Results;

namespace CashBook.Ledger.Domain.Accounts;

/// <summary>
/// Conta onde o dinheiro está (carteira, conta corrente, poupança).
/// </summary>
public sealed class Account : Entity
{
    public const int NameMaxLength = 100;

    private Account(Guid id, string name)
        : base(id)
    {
        Name = name;
        IsActive = true;
    }

    // Materialização pelo EF Core.
    private Account()
    {
        Name = string.Empty;
    }

    public string Name { get; private set; }

    public bool IsActive { get; private set; }

    public static Result<Account> Create(string name)
    {
        var validation = ValidateName(name);
        if (validation.IsFailure)
        {
            return validation.Error;
        }

        return new Account(Guid.CreateVersion7(), validation.Value);
    }

    public Result Update(string name, bool isActive)
    {
        var validation = ValidateName(name);
        if (validation.IsFailure)
        {
            return validation.Error;
        }

        Name = validation.Value;
        IsActive = isActive;
        return Result.Success();
    }

    private static Result<string> ValidateName(string? name)
    {
        var normalized = name?.Trim() ?? string.Empty;

        if (normalized.Length == 0)
        {
            return AccountErrors.NameRequired;
        }

        if (normalized.Length > NameMaxLength)
        {
            return AccountErrors.NameTooLong;
        }

        return normalized;
    }
}
