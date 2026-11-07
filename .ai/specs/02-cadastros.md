# Spec 02 — Cadastros

**Encontro:** 2  
**Tag plano B:** `encontro-2`

## Objetivo

Módulo Ledger com Account e Category persistidos (EF Core + SQLite). Telas Angular de lista e formulário.

## Skills e rules

- Skills: `create-backend-module`, `configure-sqlite-persistence`, `create-entity`, `create-use-case`, `create-efcore-persistence`, `create-api-endpoint`, `create-angular-feature`, `run-quality-checks`
- Rules: architecture, persistence, naming, angular

## Tarefas

- [x] T1. Criar módulo Ledger (4 projetos) e registrar no Host.
- [x] T2. Entidades Account e Category + erros.
- [x] T3. Use cases Create/List/Update (e Get Account).
- [x] T4. EF: configurações, repositórios, migration inicial.
- [x] T5. Endpoints `/api/accounts` e `/api/categories`.
- [x] T6. Features Angular `accounts` e `categories`.
- [x] T7. Testes de domínio + integração do POST duplicado (409).

## Arquivos esperados

- `CashBook.Ledger.Domain/Accounts/Account.cs`
- `CashBook.Ledger.Domain/Categories/Category.cs`
- `CashBook.Ledger.Infrastructure/Persistence/LedgerDbContext.cs`
- `src/Frontend/cashbook-web/src/app/features/accounts/`

## Validação

```powershell
dotnet test
```

Criar uma conta no Angular e recarregar a página: a conta permanece (SQLite).

## Evidências

Data: 2026-11-07 (SDK .NET 9.0.311, EF Core 9.0.22, dotnet-ef 9.0.0, Angular CLI 20.0.5). Tag `encontro-2` não usada.

- Build: `dotnet build CashBook.sln --nologo` → 0 Warning(s), 0 Error(s) (warnings as errors).
- Testes: `dotnet test CashBook.sln --nologo` → 56 passed, 0 failed.
  - `CashBook.Ledger.UnitTests`: 45 (domínio Account/Category + use cases com fakes em memória + 16 da spec 01).
  - `CashBook.Host.Api.IntegrationTests`: 11 (accounts/categories 201, 400, 404, PUT, POST duplicado → 409 `Account.NameAlreadyExists` / `Category.NameAlreadyExists`; health 2). SQLite temporário por classe de teste.
- Migration aplicada: `InitialLedger` (`ledger_accounts`, `ledger_categories`, nome único com collation NOCASE, `Type` como TEXT), aplicada no startup em Development (`MigrateLedgerDatabaseAsync`).
- Angular: `npm run build` → lazy chunks `accounts-routes` e `categories-routes`.
- Conta visível após F5: no navegador, `/accounts` vazio → criar "Carteira" → aparece na lista; "carteira" de novo → mensagem "Já existe uma conta com esse nome. (Account.NameAlreadyExists)"; F5 → "Carteira" continua. Mesmo fluxo em `/categories` com "Mercado" (Saída). Console: só o 409 esperado.

### Notas de execução

- Um `CashBook.Host.Api.exe` órfão da verificação da spec 01 (PID 74356) segurava `bin/Debug` e a porta 5000. Build/testes rodaram com `--artifacts-path` (pasta temporária); a migration foi gerada chamando `ef.dll` sobre esse build; a verificação no navegador usou o Host novo na porta 5050 com as chamadas de 5000 redirecionadas só na aba de teste.
- `UpdateCategory` só renomeia: o tipo é fixo após a criação (a spec 03 valida tipo do lançamento x categoria).
- `.editorconfig`: `[**/Migrations/*.cs] generated_code = true` (migrations usam namespace em bloco).
- `HealthEndpointTests` passou a usar `CashBookApiFactory` para não criar `cashbook.db` na pasta de testes.
