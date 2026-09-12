# MauiBrickBreak — REDO

The original .NET MAUI/Orbit brick-breaker remains in this repository as the historical implementation.

## Current build

From the repository root:

```bash
dotnet build REDOGIT.slnx --configuration Release
dotnet run --project successors/redogit-2026/MauiBrickBreak.Core.csproj --configuration Release --no-build
```

`REDOGIT.slnx` is the current solution entry point. The historical `MauiBrickBreak.sln`, .NET 6 MAUI application, and rendering code remain intact as predecessor material.

The current .NET 10 successor starts from the behavior that matters rather than the old UI/runtime wiring:

- ball motion;
- wall reflection;
- paddle reflection;
- block collision and hit depletion;
- deterministic state transitions that can be checked without a GUI.

The predecessor references an `Orbit.Engine` project outside this repository. The REDO core removes that hidden build dependency from the behavior being verified. A future renderer can consume the core rather than own the game rules.

The historical controls were `a/s/w/d` for movement and `l` to launch. Those remain part of the predecessor's record rather than being silently rewritten.

See [`REDOGIT.md`](REDOGIT.md).
