---
name: create-unit-tests
description: Cria testes xUnit de domínio ou use case com fakes. Sem EF e sem HTTP.
---

# Skill: create-unit-tests

## Processo

1. Projeto `CashBook.Ledger.UnitTests`.
2. Domínio: um fato por invariante, afirmar `Error.Code`.
3. Aplicação: fake de repositório em memória (ver `tests/CashBook.Ledger.UnitTests/Fakes`).
4. Não instanciar `LedgerDbContext`.

## Exemplo

`tests/CashBook.Ledger.UnitTests/Domain/TransactionTests.cs`
