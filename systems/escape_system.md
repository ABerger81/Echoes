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
Player's Light Extinguished or Dimmed
↓
Player Escapes
↓
Success or Failure

---

# Monster Behavior (Resolved Design)

No sight, no line-of-sight checks. The monster moves toward the most recent location it heard a sound, and wanders when nothing recent has been heard. It requires several continuous seconds of stillness and silence before fully losing track of the player — long enough that simply freezing isn't a guaranteed permanent escape. While actively hunting, its speed is slightly faster than the player's sprint speed, so outrunning it in a straight line isn't a viable strategy alone. This makes player noise — not the monster's intelligence — the core danger signal during Escape. Full detail in docs/mechanics.md (Escape / Chase); audio implementation in docs/audio_design.md.

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

- Can the player drop or lose already-collected treasure if caught, or is it simply Game Over with no partial credit?
- If caught, is it instant Game Over, or a short forced "capture" sequence first?
