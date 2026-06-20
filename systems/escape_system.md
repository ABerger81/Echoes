# Escape System

## Purpose

Transform exploration into panic.

---

# Scope Note

The full system described below is an Expansion-phase target (scope.md Milestone 7+). The MVP version (Milestone 4) is intentionally minimal — a countdown timer adds urgency, no AI or pathfinding — just enough to prove the explore → trigger → escape → win/lose loop works.

---

## MVP Implementation (Milestones 4 & 5)

### Files

| File | Role |
|---|---|
| `Assets/_Game/Scripts/Managers/GameManager/GameManager.cs` | Owns escape state, timer, and win/lose events |
| `Assets/_Game/Scripts/Escape/ExitPoint.cs` | Trigger volume — detects Player + checks IsEscapeTriggered |
| `Assets/_Game/Scripts/Escape/EscapeUI.cs` | Shows "ESCAPE! Find the exit." on screen, auto-hides after 4 s |
| `Assets/_Game/Scripts/Player/CaptureHandler.cs` | Disables FirstPersonController + Interactor on capture or win |
| `Assets/_Game/Scripts/Player/CaptureUI.cs` | Shows "You were captured." on screen, stays until restart |

### Event Flow

```
Player picks up Major Treasure
  → Treasure.OnCollected(TreasureType.Major)
    → GameManager.HandleCollected(Major)
      → IsEscapeTriggered = true
      → OnEscapeTriggered?.Invoke()        ← Milestone 7 hooks here (heartbeat escalation)
      → countdown timer starts

Countdown hits zero
  → GameManager._isLevelOver = true
  → OnTimerExpired?.Invoke()
    → CaptureHandler.HandleCapture()       (movement + interaction disabled)
    → CaptureUI.ShowCaptureMessage()       ("You were captured." on screen)

Player walks into ExitPoint trigger
  → ExitPoint.OnTriggerEnter checks IsEscapeTriggered
    → true → GameManager.TriggerLevelComplete()
      → GameManager._isLevelOver = true
      → OnLevelComplete?.Invoke()
        → CaptureHandler.HandleCapture()   (movement + interaction disabled)
        → Milestone 6 hooks here           (win screen)
```

### Key Decisions

**Static `IsEscapeTriggered` property** — ExitPoint reads this directly without any Inspector wiring. Consistent with the static-event pattern established on `Treasure.OnCollected`.

**Timer in GameManager, not a separate script** — The timer is three fields and a few lines in `Update()`. A dedicated `EscapeTimer.cs` would be premature for MVP.

**`TriggerLevelComplete()` is a static method** — ExitPoint calls it with `GameManager.TriggerLevelComplete()`, no instance reference needed.

**Static state reset in `OnDestroy()`** — `IsEscapeTriggered = false` and `_isLevelOver = false` on destroy prevents stale state carrying over when stopping and restarting Play in the editor.

**`_isLevelOver` guard in GameManager** — Set true on timer expiry or level complete, whichever fires first. `TriggerLevelComplete()` returns early if already set, so walking into the exit after capture does nothing.

**`_triggered` guard in ExitPoint** — The PlayerCapsule has both a `CapsuleCollider` and a `CharacterController`, which causes Unity to fire `OnTriggerEnter` twice per entry. Instance bool prevents double-firing.

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
