# Escape System

## Purpose

Transform exploration into panic.

---

# Scope Note

The full system described below is an Expansion-phase target (scope.md Milestone 7+). The MVP version (Milestone 4) is intentionally minimal — a timer or a placeholder threat with no real pathfinding — just enough to prove the explore → trigger → escape → win/lose loop works.

---

# Trigger

Player acquires treasure.

---

# Escape Loop

Treasure Found
↓
Monster Activated
↓
Player Escapes
↓
Success or Failure

---

# Success

Player reaches exit.

Rewards:

- Score Saved
- Leaderboard Entry

---

# Failure

Monster catches player.

Penalties:

- Reduced Score
- Game Over

---

# Open Questions

- Is monster behavior scripted (fixed patrol/predictable route) or reactive (chases based on player position/noise)?
- Can the player drop or lose already-collected treasure if caught, or is it simply Game Over with no partial credit?
- Are Safe Zones available during Escape, and do they carry the time limit described in mechanics.md?
