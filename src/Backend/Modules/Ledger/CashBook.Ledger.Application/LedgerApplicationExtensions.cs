using CashBook.Ledger.Application.Accounts.CreateAccount;
using CashBook.Ledger.Application.Accounts.GetAccount;
using CashBook.Ledger.Application.Accounts.ListAccounts;
using CashBook.Ledger.Application.Accounts.UpdateAccount;
using CashBook.Ledger.Application.Categories.CreateCategory;
using CashBook.Ledger.Application.Categories.ListCategories;
using CashBook.Ledger.Application.Categories.UpdateCategory;
using Microsoft.Extensions.DependencyInjection;

namespace CashBook.Ledger.Application;

public static class LedgerApplicationExtensions
{
    // Casos de uso registrados um a um (rule: dotnet).
    public static IServiceCollection AddLedgerApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateAccountUseCase>();
        services.AddScoped<ListAccountsUseCase>();
        services.AddScoped<GetAccountUseCase>();
        services.AddScoped<UpdateAccountUseCase>();

        services.AddScoped<CreateCategoryUseCase>();
        services.AddScoped<ListCategoriesUseCase>();
        services.AddScoped<UpdateCategoryUseCase>();

        return services;
    }
}
