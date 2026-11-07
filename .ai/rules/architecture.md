# Rule: architecture

- Monolito modular. O Host só compõe módulos (`AddXModule`, `MapXEndpoints`).
- Cada módulo tem Domain, Application, Infrastructure e Api em projetos separados.
- Domain não referencia ASP.NET, EF Core, Dapper nem Angular.
- Application depende só do Domain (e de BuildingBlocks).
- Infrastructure implementa contratos do Domain ou da Application.
- Shared (`CashBook.BuildingBlocks`) só contém Entity, Result, Error e IUseCase.
- BuildingBlocks.Api é o único Shared que pode depender de ASP.NET Core.
- Módulos não se referenciam. Comunicação é pelo banco (escrita Ledger, leitura Reporting) ou pelo Host.
