using CashBook.Ledger.Domain.Abstractions;
using CashBook.Ledger.Domain.Accounts;
using CashBook.Ledger.Domain.Categories;
using CashBook.Ledger.Infrastructure.Persistence;
using CashBook.Ledger.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashBook.Ledger.Infrastructure;

public static class LedgerInfrastructureExtensions
{
    // Mesma connection string usada pelo Reporting (rule: persistence).
    public const string ConnectionStringName = "CashBook";

    public static IServiceCollection AddLedgerInfrastructure(this IServiceCollection services)
    {
        // A connection string é lida ao criar o DbContext (não no registro),
        // assim os testes conseguem trocá-la por um SQLite temporário.
        services.AddDbContext<LedgerDbContext>((provider, options) =>
        {
            var connectionString = provider.GetRequiredService<IConfiguration>().GetConnectionString(ConnectionStringName)
                ?? throw new InvalidOperationException($"Connection string '{ConnectionStringName}' não configurada.");
            options.UseSqlite(connectionString);
        });

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static async Task MigrateLedgerDatabaseAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
    {
        await using var scope = services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<LedgerDbContext>();
        await dbContext.Database.MigrateAsync(cancellationToken);
    }
}
