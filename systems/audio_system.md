# Audio System

## Purpose

Wire the Heartbeat state machine to audio — mixer snapshots, ambience layering, breathing loops, and the music arc. Audio is the primary tension delivery system: the player reads danger through sound, not UI.

See docs/audio_design.md for the full design intent behind each state's audio behaviour.

---

# Design Goals

- State-driven: audio changes because HeartbeatState changed, not on a timer
- Withdrawal signals danger: music strips away as tension rises; silence at Panic is intentional
- Diegetic-first: every sound could believably come from the world
- De-escalation is slower than escalation: coming down feels earned, not instant

---

# Heartbeat State → Mixer Snapshot Mapping

Each HeartbeatState maps to one Unity Audio Mixer Snapshot. Transitions use `AudioMixerSnapshot.TransitionTo(blendTime)`.

| State | Snapshot | Escalate Blend | De-escalate Blend |
|---|---|---|---|
| Calm | Calm | — | 3.0s |
| Alert | Alert | 0.3s | 3.0s |
| Fear | Fear | 0.3s | 3.0s |
| Panic | Panic | 0.3s | — |

Escalation is fast (sudden danger). De-escalation is slow (earned calm).

---

# Mixer Group Structure

```
Master
├─ Music
├─ Ambience
│   ├─ Ambience_A      ← base layer, always present
│   ├─ Ambience_B      ← secondary layer, fades in at Alert
│   ├─ Ambience_Deep   ← low rumble, fades in at Alert
│   └─ Ambience_Drips  ← detail layer, fades in at Fear
├─ Breathing
│   ├─ Breathing_Alert
│   ├─ Breathing_Fear
│   └─ Breathing_Panic
└─ SFX
```

Per-state ambience layer volumes (set per snapshot in the mixer):

| Group | Calm | Alert | Fear | Panic |
|---|---|---|---|---|
| Ambience_A | 0 dB | 0 dB | 0 dB | 0 dB |
| Ambience_B | −80 dB | −6 dB | −3 dB | 0 dB |
| Ambience_Deep | −80 dB | −6 dB | −3 dB | 0 dB |
| Ambience_Drips | −80 dB | −80 dB | −6 dB | 0 dB |

Per-state breathing layer volumes:

| Group | Calm | Alert | Fear | Panic |
|---|---|---|---|---|
| Breathing_Alert | −80 dB | −6 dB | −80 dB | −80 dB |
| Breathing_Fear | −80 dB | −80 dB | −6 dB | −80 dB |
| Breathing_Panic | −80 dB | −80 dB | −80 dB | 0 dB |

---

# Breathing Audio

Three separate looping AudioSources, one per non-Calm state, routed to Breathing sub-groups. The mixer snapshot controls which loop is audible. Calm = silence.

The breathing loop the player hears is the same signal HeartbeatManager.BreathingLevel represents — when the player hears breathing calm, their noise output is genuinely dropping. See systems/heartbeat_system.md — Breathing.

---

# Music Arc

Melody and texture layers exist as separate AudioSources routed to the Music group. The snapshot reduces Music group volume as state rises, removing melody at Alert/Fear and silencing it entirely at Panic.

No melodic music is reintroduced during Escape. See docs/audio_design.md — Music Arc.

---

# Per-Mythology Ambience

Each mythology uses the same snapshot/group structure. Only the audio clips differ. Current implementation: Minotaur Labyrinth (Greek) only.

Future: `MythologyAudioProfile` ScriptableObject — one asset per mythology holding one clip set per HeartbeatState. AudioManager asks for "the Fear-state clips for the active profile" without knowing which mythology is loaded. Not implemented in M8.

---

# Implementation (M8)

**Scripts:**
- `Assets/_Game/Scripts/Managers/AudioManager/AudioManager.cs` — subscribes to `HeartbeatManager.OnStateChanged`, transitions snapshots, exposes `PlayJumpscare()` API

**Mixer asset:**
- `Assets/_Game/Audio/EchoesAudioMixer` — groups, sub-groups, four snapshots with per-group volumes set

**AudioSources on AudioManager GameObject:**
- Ambience_A, Ambience_B, Ambience_Deep, Ambience_Drips (four ambience layers)
- MusicMelody, MusicTexture (no clips yet — Vertical Slice)
- Breathing_Alert, Breathing_Fear, Breathing_Panic (three breathing loops)

**Tuning defaults:**
- `escalateBlend`: 0.3s — fast on the way up
- `deEscalateBlend`: 3.0s — slow on the way down

**Visual:**
- `Assets/_Game/Scripts/UI/HeartbeatVisuals.cs` — upgraded from CanvasGroup placeholder to URP Volume Vignette
- `vignetteBlendSpeed`: 0.3 — smooth lerp via `Mathf.MoveTowards` in Update()
- Vignette intensities: Alert 0.25, Fear 0.45, Panic 0.65

**Public API:**
- `PlayJumpscare(AudioClip clip)` — fire-and-forget one-shot. Called by jumpscare system (M14).

---

# Open Questions

- Exact per-mythology clip assignments — content pass during Vertical Slice
- `MythologyAudioProfile` ScriptableObject implementation — deferred to when second mythology exists
- Exact jumpscare cooldown value — needs playtesting (see docs/audio_design.md)
- How many simultaneous ambience layers before mix becomes muddy — needs implementation testing
