using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;

namespace CashBook.Host.Api.IntegrationTests;

/// <summary>
/// Sobe a API em Development (migrations aplicadas no startup) apontando para um SQLite temporário,
/// apagado ao final. Nunca toca no cashbook.db de desenvolvimento.
/// </summary>
public sealed class CashBookApiFactory : WebApplicationFactory<Program>
{
    private readonly string _databasePath = Path.Combine(Path.GetTempPath(), $"cashbook-tests-{Guid.NewGuid():N}.db");

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");
        builder.UseSetting("ConnectionStrings:CashBook", $"Data Source={_databasePath}");
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (disposing)
        {
            // O pool do SQLite segura o arquivo aberto; liberar antes de apagar.
            SqliteConnection.ClearAllPools();
            if (File.Exists(_databasePath))
            {
                File.Delete(_databasePath);
            }
        }
    }
}
