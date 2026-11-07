using CashBook.BuildingBlocks.Domain;
using CashBook.BuildingBlocks.Results;

namespace CashBook.Ledger.Domain.Categories;

/// <summary>
/// Classificação de um lançamento (Salário, Mercado). O tipo é fixo após a criação:
/// lançamentos dependem dele para manter a coerência entrada/saída.
/// </summary>
public sealed class Category : Entity
{
    public const int NameMaxLength = 100;

    private Category(Guid id, string name, CategoryType type)
        : base(id)
    {
        Name = name;
        Type = type;
    }

    // Materialização pelo EF Core.
    private Category()
    {
        Name = string.Empty;
    }

    public string Name { get; private set; }

    public CategoryType Type { get; private set; }

    public static Result<Category> Create(string name, CategoryType type)
    {
        if (!Enum.IsDefined(type))
        {
            return CategoryErrors.TypeInvalid;
        }

        var validation = ValidateName(name);
        if (validation.IsFailure)
        {
            return validation.Error;
        }

        return new Category(Guid.CreateVersion7(), validation.Value, type);
    }

    public Result Rename(string name)
    {
        var validation = ValidateName(name);
        if (validation.IsFailure)
        {
            return validation.Error;
        }

        Name = validation.Value;
        return Result.Success();
    }

    private static Result<string> ValidateName(string? name)
    {
        var normalized = name?.Trim() ?? string.Empty;

        if (normalized.Length == 0)
        {
            return CategoryErrors.NameRequired;
        }

        if (normalized.Length > NameMaxLength)
        {
            return CategoryErrors.NameTooLong;
        }

        return normalized;
    }
}
