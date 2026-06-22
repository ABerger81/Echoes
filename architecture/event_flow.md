# Event Flow

Documents the major event chains in the game. Full implementation lives in GameManager.cs and the relevant system scripts. Expansion events are noted where they hook into the existing flow.

---

## Minor Treasure Collected

```
Player interacts with Minor Treasure
  → Treasure.OnCollected(TreasureType.Minor)
    → GameManager.HandleCollected(Minor)
      → Score += 100
      → OnScoreChanged?.Invoke(Score)
        → UIManager.UpdateScore()         (HUD updates live)
      → JumpscareChance evaluated         (phobia pool — Milestone 14)
```

---

## Major Treasure Collected — Escape Triggered

```
Player interacts with Major Treasure
  → Treasure.OnCollected(TreasureType.Major)
    → GameManager.HandleCollected(Major)
      → IsEscapeTriggered = true
      → _escapeTimer = escapeTimerDuration
      → OnEscapeTriggered?.Invoke()
        → EscapeUI.ShowEscapePrompt()         (auto-hides after 4s)
        → Monster → Active Hunt               (Milestone 12)
        → Monster extinguishes carried light  (Milestone 12)
        → HeartbeatSystem escalates           (Milestone 7)
```

---

## Player Reaches Exit (Win)

```
Player enters ExitPoint trigger volume
  → ExitPoint.OnTriggerEnter
      checks: IsEscapeTriggered && !_triggered
    → GameManager.TriggerLevelComplete()
      → _isLevelOver = true
      → OnLevelComplete?.Invoke()
        → CaptureHandler.HandleCapture()    (movement + interaction disabled)
        → UIManager.ShowWinPanel()          (final score shown, cursor released)
```

---

## Timer Expires (Capture / Lose)

```
GameManager.Update() — _escapeTimer reaches zero
  → _isLevelOver = true
  → escapeTimerDuration = 0f               (prevents repeat firing)
  → Score = 0
  → OnScoreChanged?.Invoke(0)              (score resets before panel appears)
  → OnTimerExpired?.Invoke()
    → CaptureHandler.HandleCapture()       (movement + interaction disabled)
    → UIManager.ShowGameOverPanel()        (score 0 shown, cursor released)
```

Note: `_isLevelOver` is set before either event fires, so whichever path completes first (Win or Lose) blocks the other. Walking into the exit after timer expiry does nothing.

---

## Heartbeat State Transition (Expansion — Milestone 7)

```
Noise input changes (action noise OR breathing level OR proximity OR jumpscare)
  → HeartbeatSystem evaluates noise value
    → State rises:  Calm → Alert → Fear → Panic   (fast)
    → State falls:  Panic → Fear → Alert → Calm   (slow — several seconds per step)
      → AudioManager.TransitionToSnapshot(state)   (Milestone 8)
      → Visual effects updated
      → Breathing audio level updated
```

---

## Panic — Accidental Escape Trigger (Expansion — Milestone 7)

```
HeartbeatSystem reaches Panic (150 BPM)
  → Probabilistic check fires (rare — tuned so most players never see this)
    → GameManager.OnEscapeTriggered?.Invoke()
      (identical code path to Major Treasure pickup — no separate implementation)
```

---

## Sidequest Completion (Vertical Slice — Milestone 13)

```
Player places third collectible at Altar
  → Altar.OnOffering()
    → Altar acceptance sound fires          (loud — monster may hear it)
    → Hidden room trigger activates         (wall/door opens)
    → Torches in hidden room ignite         (ritual response, not player-triggered)
    → Lore fragment + weakness knowledge available in hidden room
    → Hidden room marked as Safe Zone       (monster threshold active)
```

---

## Restart

```
Player clicks Restart (UIManager.Restart() or Pause menu)
  → Cursor.lockState = CursorLockMode.Locked
  → Cursor.visible = false
  → SceneManager.LoadScene(buildIndex)
    → OnDestroy fires on all MonoBehaviours
      → GameManager: IsEscapeTriggered = false, _isLevelOver = false
      → All event subscriptions unregistered
    → Scene reloads from scratch
    → GameManager.Awake(): cursor re-locked, subscriptions re-registered
```
