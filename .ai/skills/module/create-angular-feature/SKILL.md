---
name: create-angular-feature
description: Cria uma feature Angular (model, api, page, routes) e registra em app.routes.ts e no menu. Use em cada encontro a partir do 2.
---

# Skill: create-angular-feature

## Entrada

`feature` = accounts | categories | transactions | reports

## Passos

1. Criar `src/app/features/<feature>/` com model, api, page, html, routes.
2. API só usa caminho relativo (`api/...`).
3. Registrar `loadChildren` em `app.routes.ts`.
4. Incluir o link em `app.ts`.

## Exemplo

`src/Frontend/cashbook-web/src/app/features/accounts/`

## Validação

A rota abre sem erro de console. Lista vazia é estado válido.
