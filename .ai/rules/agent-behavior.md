# Rule: agent-behavior

- Não apagar, mover nem sobrescrever arquivo sem autorização explícita.
- Não criar pasta fora do projeto atual.
- Listar arquivos que serão criados antes de escrever, quando a tarefa for grande.
- Ao final: `dotnet build` e `dotnet test`. Se quebrar, corrigir antes de declarar pronto.
- Skills determinísticas (scripts) são idempotentes: se a estrutura já existe, não recriam.
- Preferir sessão nova por spec, em vez de conversa infinita.
