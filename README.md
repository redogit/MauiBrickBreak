# MauiBrickBreak — REDO

Commercial-access note: the existing MIT license remains authoritative for material it covers. The owner's newer no-commercial-access policy does not revoke MIT rights; it governs only material not already covered by an existing license. See [COMMERCIAL_ACCESS_POLICY.md](COMMERCIAL_ACCESS_POLICY.md).

The original .NET MAUI/Orbit brick-breaker remains in this repository as the historical implementation.

## Current build — v2

From the repository root:

```bash
dotnet build REDOGIT.slnx --configuration Release
dotnet run --project successors/redogit-2026-v2/MauiBrickBreak.SelfCheck/MauiBrickBreak.SelfCheck.csproj --configuration Release --no-build
```

`REDOGIT.slnx` is the current solution entry point. v2 separates reusable game behavior from its verifier:

- `MauiBrickBreak.Core` — framework-independent state, geometry, collisions, and observable game events;
- `MauiBrickBreak.SelfCheck` — executable behavior contract.

The current contract verifies wall reflection, paddle reflection, block hit depletion, and the bottom-boundary `BallLost` consequence. A renderer can consume those events without owning the underlying rules.

## Preserved predecessors

- The historical `MauiBrickBreak.sln`, .NET 6 MAUI application, Orbit rendering code, assets, and controls remain intact.
- `successors/redogit-2026/` remains the first verified framework-independent successor and is now the predecessor to v2.
- The old app's `Orbit.Engine` reference was outside this repository; neither REDOGIT successor depends on it.

The historical controls were `a/s/w/d` for movement and `l` to launch. Those remain part of the predecessor's record rather than being silently rewritten.

See [`REDOGIT.md`](REDOGIT.md).
