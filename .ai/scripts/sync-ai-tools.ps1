[CmdletBinding()]
param(
    [string]$Root = ""
)

if (-not $Root) {
    $scriptDir = if ($PSScriptRoot) { $PSScriptRoot } else { Split-Path -Parent $MyInvocation.MyCommand.Path }
    $Root = (Resolve-Path (Join-Path $scriptDir "..\..")).Path
}

$ErrorActionPreference = "Stop"
$ai = Join-Path $Root ".ai"

function New-WrapperDir([string]$Path) {
    if (-not (Test-Path $Path)) {
        New-Item -ItemType Directory -Path $Path | Out-Null
    }
}

function Write-Pointer([string]$Target, [string]$RelativeSource, [string]$Name, [string]$Description) {
    $content = @"
---
name: $Name
description: $Description
---

Fonte canônica (não edite este wrapper): ``$RelativeSource``

Leia e siga o arquivo acima no repositório.
"@
    Set-Content -Path $Target -Value $content -Encoding utf8
}

$cursorRules = Join-Path $Root ".cursor\rules"
$cursorSkills = Join-Path $Root ".cursor\skills"
$claudeSkills = Join-Path $Root ".claude\skills"
$agentsSkills = Join-Path $Root ".agents\skills"
New-WrapperDir $cursorRules
New-WrapperDir $cursorSkills
New-WrapperDir $claudeSkills
New-WrapperDir $agentsSkills

Get-ChildItem (Join-Path $ai "rules") -Filter *.md | ForEach-Object {
    $rel = ".ai/rules/$($_.Name)"
    Write-Pointer (Join-Path $cursorRules ($_.BaseName + ".mdc")) $rel $_.BaseName "Rule $($_.BaseName) do CashBook"
}

Get-ChildItem (Join-Path $ai "skills") -Recurse -Filter SKILL.md | ForEach-Object {
    $name = $_.Directory.Name
    $rel = $_.FullName.Substring($Root.Length).TrimStart("\", "/")
    $desc = "Skill $name do CashBook"
    Write-Pointer (Join-Path $cursorSkills "$name.md") $rel $name $desc
    New-WrapperDir (Join-Path $claudeSkills $name)
    Write-Pointer (Join-Path $claudeSkills "$name\SKILL.md") $rel $name $desc
    New-WrapperDir (Join-Path $agentsSkills $name)
    Write-Pointer (Join-Path $agentsSkills "$name\SKILL.md") $rel $name $desc
}

Write-Host "Adaptadores gerados em .cursor, .claude e .agents a partir de .ai"
