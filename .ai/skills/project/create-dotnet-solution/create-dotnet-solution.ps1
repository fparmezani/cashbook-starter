[CmdletBinding()]
param(
    [string]$Root = (Get-Location).Path
)

$ErrorActionPreference = "Stop"
Set-Location $Root

if (Test-Path "CashBook.sln") {
    Write-Host "CashBook.sln já existe. Nada a fazer."
    exit 0
}

dotnet new sln -n CashBook --force

dotnet new classlib -n CashBook.BuildingBlocks -o src/Backend/CashBook.BuildingBlocks
dotnet new classlib -n CashBook.BuildingBlocks.Api -o src/Backend/CashBook.BuildingBlocks.Api
dotnet new web -n CashBook.Host.Api -o src/Backend/CashBook.Host.Api
dotnet new xunit -n CashBook.Ledger.UnitTests -o tests/CashBook.Ledger.UnitTests
dotnet new xunit -n CashBook.Host.Api.IntegrationTests -o tests/CashBook.Host.Api.IntegrationTests

dotnet sln CashBook.sln add `
    src/Backend/CashBook.BuildingBlocks/CashBook.BuildingBlocks.csproj `
    src/Backend/CashBook.BuildingBlocks.Api/CashBook.BuildingBlocks.Api.csproj `
    src/Backend/CashBook.Host.Api/CashBook.Host.Api.csproj `
    tests/CashBook.Ledger.UnitTests/CashBook.Ledger.UnitTests.csproj `
    tests/CashBook.Host.Api.IntegrationTests/CashBook.Host.Api.IntegrationTests.csproj

if (-not (Test-Path "src/Backend/CashBook.Host.Api/CashBook.Host.Api.csproj")) {
    throw "Host.Api não foi criado."
}

Write-Host "Solução criada. Copie global.json, Directory.*.props e Directory.Build.targets da referência se ainda não existirem."
Write-Host "Próximo: implementar Entity, Result, Error e o endpoint /health."
