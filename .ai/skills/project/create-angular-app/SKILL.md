---
name: create-angular-app
description: Cria o app Angular standalone em src/Frontend/cashbook-web com core, shared e rota inicial. Use na spec 01.
---

# Skill: create-angular-app

## Quando usar

Backend `/health` já responde. Precisa do frontend na porta 4200.

## Passos

1. Se `src/Frontend/cashbook-web/package.json` existir, não recriar.
2. `ng new cashbook-web --directory src/Frontend/cashbook-web --routing --style=css --ssr=false --skip-git --skip-tests --defaults`
3. Criar `core/config/environment.ts` com `apiBaseUrl: 'http://localhost:5000'`.
4. Criar interceptor que prefixa a URL.
5. Home com botão "Testar conexão com a API" em `health`.
6. Pastas vazias `features/` e `shared/ui/`.

## Regras

`angular`, `naming`, `agent-behavior`.

## Validação

`npm start` abre `http://localhost:4200`. O botão da Home fala com `/health`.
