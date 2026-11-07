# Rule: dotnet

- .NET 9, `file-scoped namespace`, nullable ligado, warnings as errors.
- Código em inglês. Comentários de ensino podem estar em português.
- Preferir Minimal APIs a controllers.
- Casos de uso registrados um a um no `*ApplicationExtensions`.
- Result para regra de negócio. Exceção só para falha inesperada (configuração, IO).
- Construtor primário quando não esconde a intenção.
- Enums persistidos como string.
