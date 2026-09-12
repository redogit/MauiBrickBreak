# MauiBrickBreak — REDO

The original .NET MAUI/Orbit brick-breaker remains in this repository as the historical implementation.

The current rebuild starts from the behavior that matters rather than the old UI/runtime wiring:

- ball motion;
- wall reflection;
- paddle reflection;
- block collision and hit depletion;
- deterministic state transitions that can be tested without a GUI.

## Current successor

`successors/redogit-2026/` contains a .NET 10 framework-independent game core and an executable smoke test.

```bash
cd successors/redogit-2026
dotnet run
```

The predecessor targeted .NET 6 and referenced an `Orbit.Engine` project outside this repository. The redo removes that hidden build dependency from the behavioral core. A future MAUI renderer can sit on top of the verified core rather than owning the game rules.

The historical controls were `a/s/w/d` for movement and `l` to launch. Those remain part of the predecessor's record rather than being silently rewritten.

See [`REDOGIT.md`](REDOGIT.md).
