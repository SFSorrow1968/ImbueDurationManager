# Tooling Quirks

## Issue
CI runners may not have local game DLLs.

## Context
Without `../libs/*.dll`, full mod builds cannot compile.

## Solution
Smoke scripts detect missing libraries. In strict mode they fail fast; otherwise they skip build steps with clear warnings.
