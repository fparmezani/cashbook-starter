---
name: create-api-endpoint
description: Expõe um caso de uso em Minimal API e converte Result com ToHttpResult.
---

# Skill: create-api-endpoint

## Processo

1. Arquivo `*Endpoints.cs` no projeto Api do módulo.
2. `MapGroup("/api/...")`.
3. Endpoint só liga request → use case → `ToHttpResult`.
4. POST de criação usa `Results.Created` ou `CreatedAtRoute`.
5. Chamar o Map no `*Module.Map*Endpoints`.

## Exemplo

[AccountEndpoints.cs](../../../../src/Backend/Modules/Ledger/CashBook.Ledger.Api/Endpoints/AccountEndpoints.cs)
[ResultExtensions.cs](../../../../src/Backend/CashBook.BuildingBlocks.Api/ResultExtensions.cs)

## Validação

Swagger/Scalar lista o endpoint. Integração cobre 201 e 400.
