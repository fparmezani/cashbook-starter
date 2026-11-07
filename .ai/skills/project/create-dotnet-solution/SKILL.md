---
name: create-dotnet-solution
description: Cria a solução .NET 9 do CashBook (governança, Shared, Host, pasta de testes) na pasta atual. Use ao iniciar o projeto ou quando a spec 01 pedir a fundação backend.
---

# Skill: create-dotnet-solution

Tipo: determinística (script).

## Quando usar

Pasta vazia ou só com `.ai/`. Objetivo: solution compilando com Shared + Host + `/health`.

## Passos

1. Executar `pwsh .ai/skills/project/create-dotnet-solution/create-dotnet-solution.ps1`.
2. O script é idempotente: se `CashBook.sln` já existe, ele para sem sobrescrever.
3. Validar: `dotnet build` e `dotnet test`.

## Regras

Aplicar `architecture`, `dotnet`, `naming`, `agent-behavior`.

## Arquivos esperados

- `CashBook.sln`, `global.json`, `Directory.Build.props`, `Directory.Packages.props`, `Directory.Build.targets`, `.editorconfig`
- `src/Backend/CashBook.BuildingBlocks/`
- `src/Backend/CashBook.BuildingBlocks.Api/`
- `src/Backend/CashBook.Host.Api/` com `MapGet("/health")`

## Exemplo de referência

O Host de referência está em `src/Backend/CashBook.Host.Api/Program.cs`.
