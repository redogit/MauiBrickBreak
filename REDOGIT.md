# REDOGIT

This repository is being redone by preserving the predecessor and moving the durable game behavior into a testable successor.

## Legacy input

The historical application is a Windows-focused .NET 6 MAUI game using Orbit Engine rendering. Ball, paddle, and block classes mix state, rendering, and collision-related data.

## Successor boundary

The current successor under `successors/redogit-2026/` owns only deterministic game state and rules. It has no MAUI, image, dependency-injection, or Orbit dependency.

That gives the project a stable boundary:

`input -> update -> collision -> consequence -> observable state`

Rendering is an adapter, not the source of truth.

## Completion rule

A successor is not accepted merely because it looks cleaner. Its executable checks must demonstrate wall reflection, paddle reflection, and block hit depletion.

The predecessor remains available through Git history. Future UI work must consume the verified core rather than re-embedding the rules in a view.
