# Ensaio (feito na construção desta referência)

- `create-dotnet-solution.ps1` foi executado numa pasta temporária vazia: criou `CashBook.sln` e os 5 projetos. Idempotência: se a sln existe, o script sai.
- `sync-ai-tools.ps1` gerou wrappers em `.cursor/`, `.claude/` e `.agents/`.
- `dotnet test` na referência: 36 unitários + 5 de integração, 0 falhas.
- `npm run build` no Angular: gerou `reports-routes`, `transactions-routes`, `accounts-routes`, `categories-routes`.
- Specs 02–04 não foram reexecutadas do zero (o código de referência já é o resultado validado). Na aula, o mentorado reconstrói com as specs; se falhar, tag do encontro.
