---
name: create-efcore-persistence
description: Cria configuração EF, repositório e migration para um agregado do Ledger.
---

# Skill: create-efcore-persistence

## Processo

1. `IEntityTypeConfiguration<T>` com `ToTable("ledger_<plural>")`, `ValueGeneratedNever` no Id, índice único quando o domínio exigir.
2. Repositório implementa a interface do Domain. Único lugar que usa `LedgerDbContext` no módulo, além do UoW.
3. `dotnet ef migrations add ...` no projeto Infrastructure, startup Host.
4. Enums como string.

## Exemplo

`src/Backend/Modules/Ledger/CashBook.Ledger.Infrastructure/Persistence/`

## Regras

`persistence`. Dapper não entra aqui.
