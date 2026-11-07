# Spec 04 — Relatórios

**Encontro:** 4  
**Tag plano B:** `encontro-4`

## Objetivo

Módulo Reporting com Dapper: Extrato por período e Resumo mensal. Tela Angular. Pitch de 5 minutos.

## Skills e rules

- Skills: `create-backend-module`, `create-dapper-query`, `create-use-case`, `create-api-endpoint`, `create-angular-feature`, `run-quality-checks`
- Rules: persistence (Dapper só aqui), architecture

## Tarefas

- [ ] T1. Módulo Reporting (Application, Infrastructure, Api). Sem Domain rico.
- [ ] T2. `GetStatement` (abertura, linhas, totais, saldo acumulado).
- [ ] T3. `GetMonthlySummary` (entradas, saídas, líquido, breakdown por categoria).
- [ ] T4. Endpoints `/api/reports/statement` e `/api/reports/monthly-summary`.
- [ ] T5. Feature Angular `reports`.
- [ ] T6. Teste de integração dos totais.
- [ ] T7. Mentorado apresenta o projeto em 5 minutos.

## Arquivos esperados

- `CashBook.Reporting.Infrastructure/Queries/StatementQuery.cs`
- `src/Frontend/cashbook-web/src/app/features/reports/`

## Validação

```powershell
dotnet test
```

Período inválido (`to` < `from`) → 400 `Reporting.InvalidPeriod`.

## Evidências

- Extrato bate com os lançamentos: _
- Pitch feito: _
