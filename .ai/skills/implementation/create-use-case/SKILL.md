---
name: create-use-case
description: Cria caso de uso (input, output, ExecuteAsync) e registra no *ApplicationExtensions. Use depois da entidade e do contrato de repositório.
---

# Skill: create-use-case

## Processo

1. Input e Output são records na Application.
2. Classe `XUseCase` implementa `IUseCase<TInput, TOutput>` quando couber.
3. Busca agregados no repositório. Entrega ao domínio. Persiste via `IUnitOfWork`.
4. Não repetir regra que já está na entidade.
5. Registrar `AddScoped<XUseCase>()` no extensions do módulo.

## Exemplo

[CreateTransactionUseCase.cs](../../../../src/Backend/Modules/Ledger/CashBook.Ledger.Application/Transactions/CreateTransaction/CreateTransactionUseCase.cs)

## Validação

Teste com fake em memória: fluxo feliz e um erro de domínio.
