# Decision Field Profile — MauiBrickBreak

Canonical method: `redogit/redogit/DECISION_FIELD_CORE_2026-09-17.md`.

```text
X = current game state: ball, paddle, blocks, boundaries, event state
O = bounded game-rule contract under the self-check
D = consequential collision/state distinctions
R = geometry, collision, depletion and event relations
F = update motion, detect collision, reflect, decrement block, emit BallLost, verify
E = executable self-check + REDOGIT lineage
G = declared wall/paddle/block/bottom-boundary behavior is satisfied
U = renderer behavior, full-gameplay equivalence, platform-specific behavior outside the contract
```

The field separates rules from rendering:

```text
GAME_RULE_STATE != RENDERER_STATE
EVENT_OUTPUT != RENDERER_AUTHORITY
SELF_CHECK != FULL_GAME_COMPLETENESS
```
