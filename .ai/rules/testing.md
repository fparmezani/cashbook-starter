# Rule: testing

- Domínio e aplicação: testes unitários com fakes em memória. Sem EF.
- API: pelo menos um fluxo de integração com `WebApplicationFactory` e SQLite temporário.
- Todo teste de regra de negócio afirma o `Error.Code`.
- Pacotes de teste entram via `Directory.Build.targets` quando `IsTestProject=true`.
- Evidência de spec = `dotnet test` verde + arquivo esperado presente.
