# MauiBrickBreak REDO — 2026

This successor extracts the durable game behavior from the original MAUI/Orbit implementation.

## Boundary

The core owns:

- positions and velocity;
- rectangular collision tests;
- wall reflection;
- paddle reflection;
- block hit depletion.

It does **not** own rendering, input widgets, images, dependency injection, or platform-specific lifecycle behavior.

## Run the checks

```bash
dotnet run --project MauiBrickBreak.Core.csproj
```

Expected checks:

- `wall reflection: PASS`
- `paddle reflection: PASS`
- `block hit depletion: PASS`

A nonzero process exit means the successor contract failed.

## Next bounded step

Add a MAUI 10 renderer as a separate adapter over this core. .NET 10 is the current LTS baseline; the renderer should not move collision rules back into views or drawables.
