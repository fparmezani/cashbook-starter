# Spec 03 — Lançamentos e testes

**Encontro:** 3  
**Tag plano B:** `encontro-3`

## Objetivo

Entidade Transaction com invariantes, saldo da conta, testes unitários e um teste de integração de ponta a ponta, tela de lançamentos.

## Skills e rules

- Skills: `create-entity`, `create-use-case`, `create-efcore-persistence`, `create-api-endpoint`, `create-unit-tests`, `create-angular-feature`, `run-quality-checks`
- Rules: testing, architecture, persistence

## Tarefas

- [ ] T1. `Transaction.Create`: valor > 0, descrição, tipo coerente com categoria, conta ativa.
- [ ] T2. Use cases Create/List/Delete + `GetAccountBalance`.
- [ ] T3. Migration `AddTransactions`.
- [ ] T4. Endpoints `/api/transactions` e `GET /api/accounts/{id}/balance`.
- [ ] T5. Testes de domínio e de use case com fakes.
- [ ] T6. Integração: entrada 5000 + saída 1500 → saldo 3500; tipo incompatível → 400.
- [ ] T7. Feature Angular `transactions`.

## Arquivos esperados

- `CashBook.Ledger.Domain/Transactions/Transaction.cs`
- `tests/CashBook.Host.Api.IntegrationTests/LedgerEndpointsTests.cs`
- `src/Frontend/cashbook-web/src/app/features/transactions/`

## Validação

```powershell
dotnet test
```

## Evidências

- Testes verdes: _
- Saldo bate no Scalar ou na tela: _
