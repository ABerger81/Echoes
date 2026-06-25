# Game Flow

## Purpose

Define all game states and how the player moves between them — from playing through pausing, end screens, high scores, and future hub transitions.

---

# Design Philosophy

- **No saves, no checkpoints.** Old school. Restart always resets to the beginning of the level. There is no mid-level progress to recover.
- **Cursor discipline.** Cursor lock state changes only at defined transition points. Never assume it is in the right state — always set it explicitly.
- **Minimal UI.** Menus exist to be used quickly and dismissed.

---

# Game States

```
MainMenu
  ↓ Start
Playing
  ↓ Esc key → Paused
                ↓ Resume       → Playing
                ↓ Restart      → Playing (scene reload)
                ↓ Quit         → MainMenu
                ↓ Settings     → Settings Panel (overlay, returns to Paused)

Playing
  ↓ Win  → WinPanel
  ↓ Lose → GameOverPanel
              ↓ See End Screen Flow below
```

---

# End Screen Flow — Three-Phase Progression

The end screen evolves across development phases:

**MVP (current):**
```
Playing → WinPanel / GameOverPanel → [Restart] → Playing
```

**Vertical Slice (Milestone 16):**
```
Playing → WinPanel / GameOverPanel → High Score Screen → [Play Again] → Playing
```

**Future Vision:**
```
Playing → WinPanel / GameOverPanel → Hub
```

---

# Pause Menu

**Trigger:** Escape key during Playing state.
**Effect:** `Time.timeScale = 0` — gameplay pauses.

**Contents:**
- Resume — return to Playing
- Restart Level — scene reload (same as the end-screen Restart)
- Quit to Main Menu
- Settings — opens Settings Panel as an overlay

**Settings Panel:**
- Master volume slider
- Mouse sensitivity slider
- Key mapping — rebindable controls; default bindings shown
- Back — returns to Pause menu

---

# High Score (Milestone 16 — Vertical Slice)

Local only. `PlayerPrefs`. Top 10 scores per level. No backend, no account, no internet required.

**Flow after end panel:**
1. WinPanel / GameOverPanel shows the run's final score
2. Player presses "Continue" → transitions to High Score Screen
3. High Score Screen shows top 10 for this level; current run's score is highlighted if it entered the list
4. Player presses "Play Again" → scene reload

**Entry rule:** A score is added to the top 10 only if the list has fewer than 10 entries, or the score beats the lowest existing entry. Scores are stored per level (keyed by scene build index or a level identifier).

---

# Cursor Rules

| State | Cursor state |
|---|---|
| Playing | Locked, hidden |
| Paused | Unlocked, visible |
| Settings Panel | Unlocked, visible |
| WinPanel | Unlocked, visible |
| GameOverPanel | Unlocked, visible |
| High Score Screen | Unlocked, visible |
| MainMenu | Unlocked, visible |

**On scene reload (Restart):** Cursor must be explicitly re-locked in code before `SceneManager.LoadScene()` — Unity does not reset cursor state on reload. Already implemented in `UIManager.Restart()`.

**On scene load (Awake):** `GameManager.Awake()` locks and hides the cursor immediately, covering both fresh loads and restarts.

See `Assets/_Game/Scripts/Managers/GameManager/GameManager.cs` and `Assets/_Game/Scripts/Managers/UIManager/UIManager.cs`.

---

# Milestone

- MVP end screen (current): Milestone 6 (complete)
- Pause menu: Milestone 11 (Expansion)
- High Score Screen: Milestone 16 (Vertical Slice)
- Hub transition: Future Vision

---

# Related Systems

- Assets/_Game/Scripts/Managers/UIManager/UIManager.cs — Restart, cursor, end panels
- Assets/_Game/Scripts/Managers/GameManager/GameManager.cs — cursor lock on Awake, static field reset on OnDestroy
- systems/escape_system.md — win/lose events that trigger end panel transitions
- docs/mechanics.md — `_isLevelOver` guard prevents duplicate event firing across win and capture paths
