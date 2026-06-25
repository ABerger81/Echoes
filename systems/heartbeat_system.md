# Heartbeat System

## Purpose

Create tension and communicate danger.

---

# Design Goals

- Increase immersion
- Signal danger
- Affect player perception

---

# Drivers

Everything the player does generates noise. Noise is the primary input to the heartbeat system — not a UI meter, but a hidden value the player reads through audio and peripheral visual feedback.

**Noise hierarchy — action-based (lowest to highest):**

| Action | Noise Level |
|---|---|
| Idle / standing still, calm heartbeat | Near zero |
| Walking | Low |
| Collecting treasure | Medium burst |
| Sprinting | High (continuous) |

Noise decays over time. Rapid or sustained actions prevent decay, keeping noise elevated. Monster proximity amplifies consequence — the same noise level matters more when the monster is near.

**Breathing — heartbeat-driven continuous noise:**

Heartbeat state is internal — only the player hears and feels it. The monster cannot detect heartbeat directly.

Elevated heartbeat produces audible breathing — and breathing is external, detectable by the monster as a continuous noise source even when the player is completely still. This is the only noise that persists independently of action:

| Heartbeat State | Breathing Level | Monster Detectability |
|---|---|---|
| Calm (60 BPM) | Near zero | Indistinguishable from silence |
| Alert (90 BPM) | Faint | Detectable only at very close range |
| Fear (120 BPM) | Noticeable | Detectable at close range while stationary |
| Panic (150 BPM) | Heavy | Detectable from significant distance while stationary |

Breathing decays as heartbeat calms. A player who sprinted to a hiding spot has not gone silent — their breathing continues generating noise until the heartbeat drops. The breathing audio the player hears (see docs/audio_design.md) is the same signal the monster detects.

**Other drivers:**
- Monster Proximity / Active Pursuit (Escape Phase)
- Ambient Light Exposure — darkness raises heartbeat over time even with no immediate threat. See docs/mechanics.md (Light Exposure).
- Scripted Events (jumpscares) — push the state up by one step

---

# States

## Calm

Heartbeat: 60 BPM

Effects:
- No visual distortion
- Breathing: near zero — no detectable noise from the player

---

## Alert

Heartbeat: 90 BPM

Effects:
- Audible heartbeat begins
- Breathing: faint continuous noise — detectable only if the monster is very close

Events (probabilistic, not guaranteed):
- **Presence signals** — the monster is somewhere in the level. Audio: distant footstep, scraping sound, breath. Visual: peripheral shadow, torch flicker, momentary glowing eyes in the periphery.
- Audio signals are accessible to all players. Visual signals are subtle — beginner players may never notice them; attentive/experienced players discover them as easter eggs and gain richer information about danger.
- Presence signals are relatively common at this state to normalize unease without causing panic.

---

## Fear

Heartbeat: 120 BPM

Effects:
- Peripheral darkening
- Breathing: noticeable continuous noise — detectable at close range even while stationary

Events (probabilistic, less likely than Alert signals):
- **Direct jumpscares** — sudden loud sound, something lunging toward camera briefly. More intense than presence signals, less frequent.

---

## Panic

Heartbeat: 150 BPM

Effects:
- Heavy breathing (audible to player and detectable by monster from significant distance — even while stationary)
- Reduced situational awareness

Events (rare — the "black swan"):
- **Accidental escape trigger** — fires `GameManager.OnEscapeTriggered`, identical to picking up Major Treasure. The player has made so much noise they woke the monster without ever touching the Major Treasure.
- Probability tuned so most players never trigger this, but it happens often enough that players warn each other about it. Reckless sprinting and rapid collection during exploration is the main cause.
- Mechanically: same event, same code path as Major Treasure pickup. No separate implementation needed.

---

# Noise → Heartbeat Connection

The sprint/escape tension: during Escape Phase, sprinting reaches the exit faster but generates high noise, potentially escalating heartbeat and triggering monster attention. Walking quietly is slower but safer. This is a meaningful decision, not an obvious one.

---

# Breathing Down — Safe Zones

A hiding spot provides binary protection from detection by position — the monster cannot locate the player through sight or proximity. But if the player entered with an elevated heartbeat, their breathing continues to generate noise even while hidden.

The safe zone is fully safe only once breathing has calmed — which requires the heartbeat to drop, which requires time and stillness.

This creates a two-stage hiding experience consistent with horror film tension:

1. **Enter the hiding spot** — physically hidden, the monster cannot locate the player by position
2. **Wait for breathing to calm** — elevated breathing is still detectable nearby; the player is not yet safe to move

Leaving too early — while still breathing heavily — means the monster detects breathing immediately at close range: the worst possible moment to be heard.

The player judges by audio: when the breathing loop they hear has calmed, it is safe to consider moving. They still must judge whether the monster has wandered far enough away — both conditions must be true.

---

# Pre-M12 Design Intent — Sneak Mode (M11)

Third movement mode added alongside Walk (W) and Sprint (W + Shift).

| Mode | Input | `sneakNoiseLevel` | Speed |
|---|---|---|---|
| Sneak | WASD + Hold Left Ctrl | 0.1 | ~40–50% of walk |
| Walk | WASD | 0.3 | normal |
| Sprint | WASD + Hold Left Shift | 0.65 | fast |

**Why 0.1 matters:** `SetContinuousNoise` uses `Mathf.Max` — it can only raise pressure. A sneakNoiseLevel of 0.1 is below the Alert threshold (0.25). While pressure is already above 0.1 (e.g. Fear), the sneak call does nothing and normal decay continues — identical to standing still but mobile. While at Calm (pressure near zero), sneak floors pressure at 0.1, keeping the player below Alert. Sneak = move without triggering Alert.

**Hold, not toggle.** Toggle allows players to set-and-forget and always sneak. Hold keeps sneak intentional and temporarily costly.

**Safe zone exploit constraint.** `IsInSafeZone` already blocks `SetContinuousNoise` entirely. Inside a safe zone, sneak's only effect (low noise input) is already moot. Sneak must never directly modify `breathingDecayRate`, `_noisePressure`, or `BreathingLevel` — only operate via `sneakNoiseLevel`. This ensures holding Ctrl inside a safe zone has no effect on how fast breathing calms.

**"No completing by sneaking" is a level design constraint.** Monster patrol timing, safe zone exits, and time-pressure pickups must create situations where sneak is too slow. The noise system alone does not prevent a full-game sneak run.

---

# Phase Shift — Hunted Mode (Post-M12 Design Intent)

Two distinct gameplay phases require two distinct difficulty profiles for the noise/heartbeat system:

**Exploration phase** — should feel relatively permissive. The player can sprint, collect, and make mistakes without immediate punishment. The monster is present but passive. The system is forgiving enough that observant play feels safe.

**Hunted phase** — activated when the player is discovered by the monster OR when the Major Treasure is collected (escape triggered). Whichever happens first, the mode switch is permanent until exit or capture. The system becomes measurably harder:

| Parameter | Exploration | Hunted |
|---|---|---|
| `noiseDecayRate` | 0.05 (current) | ~0.02 (slower decay — Panic lasts longer) |
| `sprintNoiseLevel` | 0.65 (sprint → Fear) | ~0.80 (sprint alone → Panic) |

This makes safe zones essential in the hunted phase rather than merely useful. The player must judge every movement decision — walking vs. sprinting, entering a safe zone vs. pushing toward the exit.

**Signal to the player:** the mode switch must have a visible/audible cue. Without it, the sudden difficulty increase reads as a bug. Minimum: a heartbeat audio spike at the moment of switch. Design the cue before wiring the trigger.

**Open design questions (resolve during M12 planning):**
- What exactly constitutes "monster discovers player"? Hunt-state entry, line-of-sight, or reaching a last-known position?
- If the monster discovers the player very early (first room), is a full-game hunted mode intended or too punishing?
- If hunted mode triggers on discovery, and the player then hides and the monster returns to patrol — do parameters stay hard? (Intent: yes — the monster is aware and will return.)
- Does hunted mode need a minimum safe zone decay time to prevent impossible situations? (Recommendation: yes — cap minimum decay so Calm is reachable in ≤20s even in hard mode.)

---

# Implementation (M7)

**Scripts:**
- `Assets/_Game/Scripts/Managers/HeartbeatManager/HeartbeatManager.cs` — state machine, owns `_noisePressure` and `BreathingLevel`
- `Assets/_Game/Scripts/Player/PlayerNoiseEmitter.cs` — reads player movement and treasure events, reports noise to HeartbeatManager
- `Assets/_Game/Scripts/UI/HeartbeatVisuals.cs` — placeholder vignette overlay via CanvasGroup; to be replaced with URP Volume effect in M8

**Noise thresholds:**

| `_noisePressure` | State |
|---|---|
| < 0.25 | Calm |
| ≥ 0.25 | Alert |
| ≥ 0.50 | Fear |
| ≥ 0.75 | Panic |

**Noise inputs:**

| Action | Method | Value |
|---|---|---|
| Walking | `SetContinuousNoise` | 0.3 |
| Sprinting | `SetContinuousNoise` | 0.65 |
| Minor Treasure collected | `AddNoiseBurst` | +0.25 |
| Major Treasure / Escape triggered | forces `_noisePressure` = 1.0 | — |
| Jumpscare (M14) | `PushUpOneStep` | +1 state |

**Tuning defaults:**
- `noiseDecayRate`: 0.05 — noise drains slowly; keeps Panic state for ~3 seconds after a burst before stepping down
- `breathingRiseRate`: 3.0 — breathing rises fast (immediate feedback)
- `breathingDecayRate`: 0.1 — breathing decays slowly (~10 seconds from Panic to Calm)
- `sprintNoiseLevel`: 0.65 — sprint alone reaches Fear; Panic requires sprint + treasure burst combined

**Notes:**
- Sprint detection reads `StarterAssetsInputs.sprint` directly — not inferred from speed, so tuning MoveSpeed or SprintSpeed never breaks it
- Velocity uses horizontal magnitude only (`new Vector3(velocity.x, 0, velocity.z).magnitude`) — `CharacterController.velocity` includes Y from gravity, which would report noise while standing still

---

# Open Questions

- ~~Can heartbeat affect movement?~~ — **Noted for Expansion:** At Panic state, reduced situational awareness is the MVP effect. Movement speed reduction could be added in Expansion as an additional Panic penalty.
- ~~Can heartbeat affect clue interpretation?~~ — **Noted for Expansion:** At high heartbeat states, clue readability could be impaired (visual distortion, shaking). Expansion territory — requires Clue System (Milestone 10) to exist first.
- ~~Is the monster present from the start, or only on escape trigger?~~ — **Resolved:** Present from level start, wandering passively. It responds to noise from the first step — elevated heartbeat, loud actions, and heavy breathing are all detectable during Exploration. It does not actively chase until the Major Treasure is collected. This is why the heartbeat/noise system matters before Escape is ever triggered: the monster is already there.
