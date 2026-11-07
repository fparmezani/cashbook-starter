# Estrutura .ai do CashBook

Fonte única, independente da ferramenta. Adaptadores finos são gerados por script.

```text
.ai/
├── rules/          restrições passivas
├── skills/         tarefas ativas (SKILL.md + exemplos)
├── specs/          o que cada encontro entrega
├── workflows/      como executar uma spec
├── templates/
└── scripts/
```

## Conceitos

| Artefato | Papel |
| --- | --- |
| Rule | Convenção. Não descreve o passo a passo. |
| Skill | Como fazer uma tarefa. Referencia rules. |
| Spec | O que entregar, em que ordem, com evidência. |
| Workflow | Sequência operacional para qualquer spec. |

## Sincronizar ferramentas

```powershell
pwsh .ai/scripts/sync-ai-tools.ps1
```

Gera wrappers em `.cursor/`, `.claude/` e `.agents/` apontando para estes arquivos. Sem symlink (Windows).

## Ordem sugerida

1. Ler a spec do encontro.
2. Seguir `.ai/workflows/implement-spec.md`.
3. Carregar só as skills da fase.
4. Validar com `dotnet test` e, no frontend, `npm start`.
