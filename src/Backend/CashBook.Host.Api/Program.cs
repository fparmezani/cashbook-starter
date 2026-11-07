using CashBook.Ledger.Api;
using Scalar.AspNetCore;

const string FrontendCorsPolicy = "Frontend";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddCors(options =>
    options.AddPolicy(FrontendCorsPolicy, policy => policy
        .WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()));

builder.Services.AddLedgerModule();

var app = builder.Build();

app.UseExceptionHandler();
app.UseCors(FrontendCorsPolicy);

if (app.Environment.IsDevelopment())
{
    // Migrations do Ledger aplicadas só em Development (rule: persistence).
    await app.Services.MigrateLedgerDatabaseAsync();
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapGet("/health", () => TypedResults.Ok(new HealthResponse("Healthy")))
    .WithName("Health")
    .WithTags("Health");

app.MapLedgerEndpoints();

await app.RunAsync();

internal sealed record HealthResponse(string Status);

// Exposto para WebApplicationFactory nos testes de integração.
public partial class Program;
