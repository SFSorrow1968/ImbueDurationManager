# Imbue Duration Manager

Imbue Duration Manager controls how long imbues last across the game.

## Design Rules
- Presets are batch editors only.
- Collapsible categories are always the source of truth for runtime behavior.
- Runtime reads only collapsible values, never preset fields directly.

## Core Features
- In-game duration presets from **Way Less** to **Infinite**.
- Context profile presets to bias player/NPC/thrown/world behavior.
- Per-context collapsibles for fine tuning:
  - Player Held
  - Player Thrown
  - NPC Held
  - NPC Thrown
  - World / Dropped
- Global multiplier defaulted slightly longer than base game (`0.85x` drain).
- Robust diagnostics logging and periodic telemetry summaries.

## Build
- `dotnet build ImbueDurationManager.csproj -c Release`
- `dotnet build ImbueDurationManager.csproj -c Nomad`

Output folders:
- `bin/Release/PCVR/ImbueDurationManager/`
- `bin/Release/Nomad/ImbueDurationManager/`

## Smoke
- `powershell -File _agent/ci-smoke.ps1`
- `powershell -File _agent/ci-smoke.ps1 -Strict`
