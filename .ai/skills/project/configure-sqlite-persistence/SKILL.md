---
name: configure-sqlite-persistence
description: Configura EF Core SQLite no módulo Ledger (connection string CashBook, DbContext, migration inicial). Use na spec 02 antes dos repositórios.
---

# Skill: configure-sqlite-persistence

## Passos

1. Pacotes em Ledger.Infrastructure: `Microsoft.EntityFrameworkCore.Sqlite`, `Microsoft.EntityFrameworkCore.Design`.
2. `appsettings.json` do Host: `"CashBook": "Data Source=cashbook.db"`.
3. `LedgerDbContext` com `ApplyConfigurationsFromAssembly`.
4. `AddDbContext` em `AddLedgerInfrastructure`.
5. `MigrateLedgerDatabaseAsync` chamado no Host em Development.
6. Design package também no Host para `dotnet ef`.

## Regras

`persistence`, `architecture`.

## Validação

`dotnet ef migrations list --project src/Backend/Modules/Ledger/CashBook.Ledger.Infrastructure --startup-project src/Backend/CashBook.Host.Api`
