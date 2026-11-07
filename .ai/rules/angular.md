# Rule: angular

- Componentes standalone. Sem NgModule de feature.
- Pastas: `core/`, `shared/`, `features/<nome>/`.
- Cada feature tem `*.model.ts`, `*.api.ts`, `*.page.ts`, `*.page.html`, `*.routes.ts`.
- URL base da API vem do interceptor. Feature só conhece o caminho do recurso (`api/accounts`).
- Formulários reativos. Mensagem de erro via `describeApiError`.
- Sem biblioteca de UI. Estilos em `styles.css`.
