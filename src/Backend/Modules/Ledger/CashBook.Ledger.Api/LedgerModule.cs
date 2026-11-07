using System.Text.Json.Serialization;
using CashBook.Ledger.Api.Endpoints;
using CashBook.Ledger.Application;
using CashBook.Ledger.Infrastructure;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace CashBook.Ledger.Api;

/// <summary>
/// Porta de entrada do módulo Ledger. O Host só chama AddLedgerModule, MigrateLedgerDatabaseAsync e MapLedgerEndpoints.
/// </summary>
public static class LedgerModule
{
    public static IServiceCollection AddLedgerModule(this IServiceCollection services)
    {
        // CategoryType trafega como "Income"/"Expense" no JSON.
        services.Configure<JsonOptions>(options =>
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        services.AddLedgerApplication();
        services.AddLedgerInfrastructure();

        return services;
    }

    public static Task MigrateLedgerDatabaseAsync(this IServiceProvider services, CancellationToken cancellationToken = default) =>
        LedgerInfrastructureExtensions.MigrateLedgerDatabaseAsync(services, cancellationToken);

    public static IEndpointRouteBuilder MapLedgerEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapAccountEndpoints();
        endpoints.MapCategoryEndpoints();

        return endpoints;
    }
}
