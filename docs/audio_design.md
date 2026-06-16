# Audio Design

## Purpose

Formalizes how audio communicates the Heartbeat state, danger, and discovery. Audio is the primary tension delivery system for this game — visuals and UI stay minimal so sound carries the emotional load.

This doc defines what the audio system must do. Implementation code lives alongside systems/heartbeat_system.md once Milestone 8 starts.

---

# Design Principles

- Adaptive over fixed: audio changes because the Heartbeat state changed, not on a timer.
- Restraint over loudness: a precisely-timed quiet sound is scarier than a loud one. Avoid continuous music; prefer ambience, environmental tones, and sparse musical stings.
- Silence is a tool: the moment right after a jumpscare sting should often be near-total silence, not a fade-out. This is a deliberate cue, not a bug.
- Diegetic-first: prioritize sounds the player can believe come from the world (dripping water, distant footsteps) over abstract score, especially during Calm and Alert.

---

# Heartbeat State → Mixer Snapshot Mapping

Each Heartbeat state (see systems/heartbeat_system.md) maps to one Unity Audio Mixer Snapshot. Transitions use `AudioMixerSnapshot.TransitionTo(blendTime)`, never a hard cut — the blend time itself is a tension tool.

| State | BPM | Snapshot Behavior | Blend In Time |
|---|---|---|---|
| Calm | 60 | Base ambience only. Full dynamic range. No heartbeat audio. | 1–2s, gentle |
| Alert | 90 | Base ambience + low tension drone. Faint heartbeat introduced at low volume. | 0.5–1s |
| Fear | 120 | Ambience narrows (high frequencies rolled off). Heartbeat louder/faster. Whisper / distant-threat layer added. | 0.3–0.5s |
| Panic | 150 | Heartbeat dominates the mix. Most ambience ducked. Breathing loop added. Music near-silent except sparse stings. | Under 0.2s |

De-escalation (Panic → Calm) should blend slower than escalation. Escalation should feel sudden; coming down should feel earned, not instant.

---

# Per-Mythology Ambient Layering

Each mythology gets its own base ambience and per-state layer set, but uses the same state-machine logic above. This keeps the Heartbeat/Audio system mythology-agnostic — only the audio content swaps.

## Minotaur Labyrinth (Greek)

- Calm: stone corridor wind, distant echo
- Alert: faint hoofbeat stomps added
- Fear: bull breathing/snorting added
- Panic: roar and heavy stomps dominate

## Serpent Temple (Norse)

- Calm: dripping water, low cavern hum
- Alert: distant hiss added
- Fear: scale-dragging sound added
- Panic: venom-hiss and bone-rattle dominate

Implementation hint: this maps naturally onto a `MythologyAudioProfile` ScriptableObject holding one AudioClip set per Heartbeat state. One profile asset per mythology; the Heartbeat/AudioManager just asks for "the Fear-state clips for the active profile" without needing to know which mythology is loaded.

---

# Jumpscare Trigger Rules

- A jumpscare pushes the Heartbeat state up by exactly one step (e.g. Alert → Fear), never directly to Panic. Panic is reserved for the Escape Phase.
- Minimum cooldown between scripted jumpscares within a single Exploration phase, to avoid desensitizing the player (exact value: open question below).
- Immediately after a jumpscare sting, cut most other audio for a brief moment before resuming ambience — the silence is the second half of the scare.
- Jumpscares are tied to Minor Treasure pickups and specific interactions (per mechanics.md), not random ambient timers. The player's own curiosity should cause the scare — that reinforces the "knowledge is risk" loop the rest of the design already leans on.

---

# Implementation Priority

- V1: Unity's built-in AudioSource + AudioMixer + Snapshots. This is sufficient for the entire Vertical Slice.
- Later, optional, not before V1 works: FMOD or Wwise, only if the built-in mixer becomes a real limitation you've actually hit. Don't reach for middleware before you've felt the built-in tools' ceiling yourself.

---

# Open Questions

- Exact jumpscare cooldown value — needs playtesting, not a design decision yet.
- How many ambience layers can play simultaneously before the mix becomes muddy on typical hardware?
- Should music ever play during Panic, or stay pure SFX/ambience the whole time?
