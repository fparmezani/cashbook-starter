using CashBook.BuildingBlocks.Results;

namespace CashBook.Ledger.Domain.Accounts;

public static class AccountErrors
{
    public static readonly Error NameRequired =
        Error.Validation("Account.NameRequired", "O nome da conta é obrigatório.");

    public static readonly Error NameTooLong =
        Error.Validation("Account.NameTooLong", $"O nome da conta deve ter no máximo {Account.NameMaxLength} caracteres.");

    public static readonly Error NameAlreadyExists =
        Error.Conflict("Account.NameAlreadyExists", "Já existe uma conta com esse nome.");

    public static readonly Error NotFound =
        Error.NotFound("Account.NotFound", "Conta não encontrada.");
}
