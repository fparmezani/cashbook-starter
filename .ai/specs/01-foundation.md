# Spec 01 — Fundação

**Encontro:** 1  
**Tag plano B:** `encontro-1`

## Objetivo

Solution .NET 9 compilando, Shared com Entity/Result/Error, Host com `/health`, Angular no ar falando com a API.

## Skills e rules

- Skills: `create-dotnet-solution`, `create-angular-app`, `create-unit-tests`, `run-quality-checks`
- Rules: architecture, dotnet, angular, naming, agent-behavior, testing

## Tarefas

- [x] T1. Rodar `create-dotnet-solution` (ou copiar governança da referência).
- [x] T2. Implementar `Entity`, `Error`, `Result`, `IUseCase` em BuildingBlocks.
- [x] T3. Implementar `ResultExtensions.ToHttpResult` em BuildingBlocks.Api.
- [x] T4. Host: CORS para `localhost:4200`, OpenAPI/Scalar, `GET /health`.
- [x] T5. Testes de Result/Entity.
- [x] T6. Angular: environment, interceptor, Home com teste de `/health`.
- [x] T7. `run-quality-checks`.

## Arquivos esperados

- `src/Backend/CashBook.BuildingBlocks/Domain/Entity.cs`
- `src/Backend/CashBook.BuildingBlocks/Results/Result.cs`
- `src/Backend/CashBook.Host.Api/Program.cs`
- `src/Frontend/cashbook-web/src/app/core/http/api-base-url.interceptor.ts`

## Validação

```powershell
dotnet test
# API em http://localhost:5000/health
# Angular em http://localhost:4200
```

## Evidências

Data: 2026-11-07 (SDK .NET 9.0.311, Node 24.19.0, Angular CLI 20.0.5). Tag `encontro-1` não foi usada (plano B não necessário).

- Build: `dotnet build CashBook.sln --nologo` → Build succeeded, 0 Warning(s), 0 Error(s) (TreatWarningsAsErrors ligado).
- Testes: `dotnet test CashBook.sln --nologo` → 18 passed, 0 failed.
  - `CashBook.Ledger.UnitTests`: 16 (ResultTests 11, EntityTests 5).
  - `CashBook.Host.Api.IntegrationTests`: 2 (`/health` 200 + header CORS para `localhost:4200`).
- Health 200: `dotnet run --project src/Backend/CashBook.Host.Api --launch-profile http` → `GET http://localhost:5000/health` = `200 {"status":"Healthy"}`, com `Access-Control-Allow-Origin: http://localhost:4200`. `/scalar/v1` e `/openapi/v1.json` = 200.
- Angular: `npm run build` → bundle gerado (lazy chunk `home-routes`). `ng serve` em `http://localhost:4200` = 200; clique em "Testar conexão com a API" exibiu "API respondeu: Healthy" (verificado no navegador).
- Arquivos esperados presentes: `Entity.cs`, `Result.cs`, `Program.cs`, `api-base-url.interceptor.ts`.

### Notas de execução

- `pwsh` não instalado: o script de T1 rodou com `powershell` 5.1 (`-ExecutionPolicy Bypass`).
- Sem referência para copiar, a governança foi escrita: `global.json` (9.0.300, latestFeature), `Directory.Build.props` (net9.0, nullable, warnings as errors), `Directory.Packages.props` (versões centrais), `Directory.Build.targets` (pacotes de teste quando `IsTestProject=true`), `.editorconfig` (file-scoped namespace), `.gitignore`.
- Removidos os boilerplates do template (`Class1.cs`, `UnitTest1.cs`) e `app.css` do Angular.
- Porta do Host alterada no `launchSettings.json` para 5000 (template gerava 5080).
- Home ficou em `src/app/home/` para manter `features/` vazio, como pede a skill `create-angular-app`.
