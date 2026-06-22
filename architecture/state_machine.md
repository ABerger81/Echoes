# State Machines

## Game State

Top-level game states and transitions. Full detail in docs/game_flow.md.

```
MainMenu
  ↓ Start
Playing
  ├─[Esc]──→ Paused
  │            ├── Resume      → Playing
  │            ├── Restart     → Playing (scene reload)
  │            ├── Quit        → MainMenu
  │            └── Settings    → Settings Panel → Paused
  │
  ├─[Win]──→ WinPanel
  │            └── Continue    → HighScoreScreen*  → Playing
  │
  └─[Lose]──→ GameOverPanel
               └── Continue    → HighScoreScreen*  → Playing
```

*HighScoreScreen: Vertical Slice (Milestone 16). MVP has a direct Restart button on the end panels.
Future Vision: Continue → Hub (instead of High Score Screen).

---

## Playing — Internal Phase Transition

Playing is one Unity scene. The phase changes via GameManager state flags, not scene loads.

```
Exploration Phase  (IsEscapeTriggered = false)
  ↓ Major Treasure collected OR Panic accidental trigger
Escape Phase       (IsEscapeTriggered = true)
  ↓ Player reaches Exit
Win                (_isLevelOver = true → OnLevelComplete)
  ↓ Timer expires
Lose               (_isLevelOver = true → OnTimerExpired)
```

`_isLevelOver` is a one-way latch — whichever fires first (Win or Lose) sets it true and blocks the other. See Assets/_Game/Scripts/Managers/GameManager/GameManager.cs.

---

## Heartbeat State

Runs within Playing during both Exploration and Escape phases. Transitions are bidirectional. Escalation is fast; de-escalation is deliberately slow.

```
Calm (60 BPM)
  ↕  noise / proximity / jumpscares / darkness over time
Alert (90 BPM)
  ↕
Fear (120 BPM)
  ↕
Panic (150 BPM)
```

Inputs that drive escalation: player action noise (walk/sprint/collect), breathing (heartbeat-driven continuous), monster proximity, jumpscares, ambient darkness over time.
Inputs that drive de-escalation: sustained silence, distance from monster, time inside a safe zone.

See systems/heartbeat_system.md.

---

## Monster State (Expansion — Milestone 12)

```
Passive Wandering
  (Exploration Phase — responds to noise but does not actively hunt)
  ↓ OnEscapeTriggered fires
Active Hunt
  (moves toward last heard noise source)
  ↓ No noise detected for X continuous seconds
Wandering-While-Hunting
  (still alert, no fixed target — semi-random patrol)
  ↕ Noise detected again
Active Hunt
```

The monster cannot enter safe zones in any state. See systems/monster_system.md.
