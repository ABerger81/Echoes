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

# Open Questions

- ~~Can heartbeat affect movement?~~ — **Noted for Expansion:** At Panic state, reduced situational awareness is the MVP effect. Movement speed reduction could be added in Expansion as an additional Panic penalty.
- ~~Can heartbeat affect clue interpretation?~~ — **Noted for Expansion:** At high heartbeat states, clue readability could be impaired (visual distortion, shaking). Expansion territory — requires Clue System (Milestone 10) to exist first.
- ~~Is the monster present from the start, or only on escape trigger?~~ — **Resolved:** Present from level start, wandering passively. It responds to noise from the first step — elevated heartbeat, loud actions, and heavy breathing are all detectable during Exploration. It does not actively chase until the Major Treasure is collected. This is why the heartbeat/noise system matters before Escape is ever triggered: the monster is already there.
