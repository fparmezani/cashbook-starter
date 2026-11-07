---
name: create-entity
description: Cria entidade de domínio com fábrica Create, invariantes e Error codes. Use para Account, Category e Transaction.
---

# Skill: create-entity

## Processo

1. Estender `Entity` (`CashBook.BuildingBlocks.Domain`).
2. Construtor privado. Única porta de entrada: `Create` retornando `Result<T>`.
3. Normalizar strings (trim). Validar vazio e tamanho máximo.
4. Erros em classe `XErrors` com códigos estáveis.
5. Sem referência a EF, HTTP ou DTO.

## Exemplo

[Account.cs](../../../../src/Backend/Modules/Ledger/CashBook.Ledger.Domain/Accounts/Account.cs)
[Transaction.cs](../../../../src/Backend/Modules/Ledger/CashBook.Ledger.Domain/Transactions/Transaction.cs)

## Regras

`architecture`, `dotnet`, `naming`.

## Validação

Teste unitário: nome vazio → `Account.NameRequired`. Valor <= 0 → `Transaction.AmountMustBePositive`.
