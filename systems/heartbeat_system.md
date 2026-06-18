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

- Scripted Events (jumpscares) — discrete, push the state up by one step.
- Monster Proximity / Active Pursuit (Escape Phase).
- Ambient Light Exposure — continuous, not discrete. Darkness raises Heartbeat over time even with no immediate threat present; light keeps it calmer. See docs/mechanics.md (Light Exposure) and systems/escape_system.md.

---

# States

## Calm

Heartbeat: 60 BPM

Effects:

- No visual distortion

---

## Alert

Heartbeat: 90 BPM

Effects:

- Audible heartbeat

---

## Fear

Heartbeat: 120 BPM

Effects:

- Peripheral darkening

---

## Panic

Heartbeat: 150 BPM

Effects:

- Heavy breathing
- Reduced situational awareness

---

# Open Questions

- Can heartbeat affect movement?
- Can heartbeat affect clue interpretation?
