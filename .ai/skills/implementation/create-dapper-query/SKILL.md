---
name: create-dapper-query
description: Cria query de leitura com Dapper no módulo Reporting. Use só para Extrato e Resumo mensal.
---

# Skill: create-dapper-query

## Processo

1. Interface na Application (`IStatementQuery`, `IMonthlySummaryQuery`).
2. Implementação em Reporting.Infrastructure. Retorna DTO, nunca entidade.
3. Mesma connection string `CashBook`.
4. Filtrar datas com `date(coluna)`.
5. Converter Amount com `SqliteValue.ToDecimal` (SQLite guarda decimal como TEXT/blob).
6. Materializar Dapper com classe de propriedades (não record posicional) quando o tipo vier como `byte[]`.

## Exemplo

[StatementQuery.cs](../../../../src/Backend/Modules/Reporting/CashBook.Reporting.Infrastructure/Queries/StatementQuery.cs)

## Validação

Teste de integração: dois lançamentos no mês → totais do extrato batem.
