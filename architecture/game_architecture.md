# Game Architecture

## Purpose

High-level overview of project structure, managers, systems, and the core patterns used throughout the codebase.

---

# Design Principles

- **Single Responsibility** — each script owns one concern
- **Event-Driven Communication** — systems broadcast via C# events; listeners subscribe without holding references to the broadcaster
- **Low Coupling** — systems don't reference each other directly where events suffice
- **High Cohesion** — related behaviour lives in one place

---

# Core Pattern — Static Events on GameManager

State changes that multiple systems need to respond to are broadcast as static events on GameManager. Subscribers register in `Awake()` and unsubscribe in `OnDestroy()`. No inter-system Inspector wiring required.

```csharp
// GameManager (publisher)
public static event System.Action<int> OnScoreChanged;
public static event System.Action OnEscapeTriggered;
public static event System.Action OnLevelComplete;
public static event System.Action OnTimerExpired;

// Any subscriber
private void Awake()   { GameManager.OnLevelComplete += HandleWin; }
private void OnDestroy() { GameManager.OnLevelComplete -= HandleWin; }
```

**Always unsubscribe in OnDestroy().** Static events outlive scene objects — failing to unsubscribe causes callbacks to fire on destroyed objects after scene reload.

---

# IInteractable Interface

All interactable world objects implement `IInteractable`. The `Interactor` script fires `Interact()` on whatever the centre-screen raycast hits. `Interactor` has no knowledge of what it is interacting with — the object handles its own response.

```csharp
public interface IInteractable
{
    void Interact();
    string GetPromptText();
}
```

---

# Core Managers

| Manager | Responsibility | Script |
|---|---|---|
| GameManager | Score, escape state, timer, win/lose events, cursor lock on Awake | GameManager.cs |
| UIManager | Score HUD, win/game-over panels, restart, cursor release | UIManager.cs |
| AudioManager | Mixer snapshots, ambience layers, jumpscare audio | AudioManager.cs (Milestone 8) |

---

# Systems by Scope

**MVP (implemented):**

| System | Responsibility | Key Script |
|---|---|---|
| InteractionSystem | Centre-screen raycast, IInteractable dispatch | Interactor.cs |
| TreasureSystem | Score on Minor collect, escape trigger on Major | Treasure.cs |
| EscapeSystem | Exit trigger volume, level complete on player entry | ExitPoint.cs |
| CaptureSystem | Freeze movement + interaction on win or lose | CaptureHandler.cs |

**Expansion (Milestones 7–12):**

| System | Responsibility |
|---|---|
| HeartbeatSystem | Noise/breathing state machine — Calm/Alert/Fear/Panic |
| SafeZoneSystem | Trigger volumes with mythology-consistent monster threshold |
| MonsterSystem | Noise-based AI — passive wandering, active hunt on escape trigger |
| PauseMenu | Time.timeScale, pause state, settings panel |

**Vertical Slice (Milestones 13–17):**

| System | Responsibility |
|---|---|
| SidequestSystem | IInteractable collectibles, altar, hidden room trigger |
| PhobiaPool | Per-run phobia weighting, tagged jumpscare event selection |
| PlayerHands | First-person hand rendering, light source on left hand, interaction animation |
| HighScoreSystem | PlayerPrefs top-10 local leaderboard per level |

**Future Vision:**

| System | Responsibility |
|---|---|
| HubSystem | Explorer's Club scene, expedition selection, curio cabinet |
| NPC | Briefing, lore delivery, narrative progression |
| MetaQuestSystem | Cross-level lore assembly, secret order revelation |

---

# Static State Reset Pattern

Static fields on GameManager persist across scene reloads in the Unity editor. Reset them in `OnDestroy()` so a fresh Play session doesn't inherit stale state:

```csharp
private void OnDestroy()
{
    IsEscapeTriggered = false;
    _isLevelOver = false;
    Treasure.OnCollected -= HandleCollected;
}
```
