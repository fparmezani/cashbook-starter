using CashBook.BuildingBlocks.Results;

namespace CashBook.Ledger.Domain.Categories;

public static class CategoryErrors
{
    public static readonly Error NameRequired =
        Error.Validation("Category.NameRequired", "O nome da categoria é obrigatório.");

    public static readonly Error NameTooLong =
        Error.Validation("Category.NameTooLong", $"O nome da categoria deve ter no máximo {Category.NameMaxLength} caracteres.");

    public static readonly Error TypeInvalid =
        Error.Validation("Category.TypeInvalid", "O tipo da categoria deve ser Income ou Expense.");

    public static readonly Error NameAlreadyExists =
        Error.Conflict("Category.NameAlreadyExists", "Já existe uma categoria com esse nome.");

    public static readonly Error NotFound =
        Error.NotFound("Category.NotFound", "Categoria não encontrada.");
}
