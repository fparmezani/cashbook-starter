---
name: run-quality-checks
description: Compila a solução e roda todos os testes. Use ao final de cada spec.
---

# Skill: run-quality-checks

## Comandos

```powershell
dotnet build CashBook.sln
dotnet test CashBook.sln --nologo
```

Opcional frontend:

```powershell
cd src/Frontend/cashbook-web
npm run build
```

## Evidência

Build 0 erro. Testes: 0 failed. Anotar na spec (data e quantidade de testes).
