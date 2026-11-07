---
name: create-backend-module
description: Cria um módulo backend (Domain, Application, Infrastructure, Api) e registra no Host. Use para Ledger (spec 02) e Reporting (spec 04).
---

# Skill: create-backend-module

## Entrada

- `Name`: Ledger ou Reporting
- Reporting não tem Domain rico: Application + Infrastructure + Api bastam.

## Passos

1. Criar os projetos `CashBook.<Name>.*` sob `src/Backend/Modules/<Name>/`.
2. Referências: Domain → BuildingBlocks; Application → Domain (Ledger) ou BuildingBlocks (Reporting); Infrastructure → Application/Domain; Api → Application + Infrastructure + BuildingBlocks.Api.
3. `Add<Name>Module` e `Map<Name>Endpoints` no Host. O Host não ganha regra de negócio.
4. Adicionar os csproj à solution.

## Exemplo

[LedgerModule.cs](../../../../src/Backend/Modules/Ledger/CashBook.Ledger.Api/LedgerModule.cs)
[ReportingModule.cs](../../../../src/Backend/Modules/Reporting/CashBook.Reporting.Api/ReportingModule.cs)

## Validação

`dotnet sln list` contém os novos projetos. `dotnet build` passa.
