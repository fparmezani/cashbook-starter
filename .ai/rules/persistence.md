# Rule: persistence

- EF Core + SQLite é o padrão para escrita (módulo Ledger).
- Dapper só no módulo Reporting, só leitura, só DTO/record de projeção.
- Dapper não carrega entidade de domínio.
- Mesma connection string `CashBook` nos dois módulos.
- Migrations ficam em Ledger.Infrastructure. Host aplica em Development.
- Tabelas prefixadas por módulo (`ledger_accounts`). SQLite não usa schema.
- Decimal no SQLite é TEXT: queries Dapper devem converter o valor no C# (`SqliteValue.ToDecimal`).
- Datas em filtro Dapper: `date(coluna)` para não depender do formato textual.
