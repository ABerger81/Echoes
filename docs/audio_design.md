# Audio Design

Assets/_Game/Audio/
Ambience/
├─ Hub/              Warm, interior. Crackling fireplace, distant chatter, clock ticking.
│                    Civilized and safe — contrast to the levels.
├─ Labyrinth/        Cold stone, distant echo, low wind through corridors.
│                    Enclosed, ancient, no living thing nearby.
├─ SerpentTemple/    Dripping water, deep cavern hum, damp hollow resonance.
├─ Tomb/             Dry, still. Sand whisper, faint resonance, absolute silence underneath.
└─ Shrine/           Wind through bamboo, distant temple bell, wooden creak.

Breathing/           Involuntary, physical. Frightened exertion calming down.
                     Not stylized. Not dying. Just a body betraying fear.

Music/
├─ Hub/              Victorian orchestral. Warm, period-appropriate. Humable.
├─ Melody/           Same feel as Hub but exploration-flavored. Curiosity, not dread.
└─ Texture/          No melody, no rhythm. Bowed strings, low organ tone.
                     Felt rather than heard. Indistinguishable from ambience.

SFX/
├─ Interaction/      Door creak, stone scrape, metal latch, pickup chime.
├─ Jumpscare/
│   ├─ Acousticophobia/    Sudden loud sting. Directional. Pure shock.
│   ├─ Scopophobia/        A breath from behind. Quiet. The sense of being watched.
│   ├─ Nyctophobia/        Deep silence deepening further — then a sting.
│   ├─ Thanatophobia/      Creak, settling, something long dead shifting.
│   ├─ Entomophobia/       Skittering burst. Brief. Directional. Floor level.
│   ├─ Chiroptophobia/     Rush of wingbeats from darkness.
│   └─ Musophobia/         Rapid scrabbling. Brief. Floor level.
├─ Monster/
│   ├─ Labyrinth/    Hoofbeats, bull breath, distant roar, heavy stomps.
│   ├─ SerpentTemple/Hiss, scale-dragging on stone, venom-hiss, bone rattle.
│   ├─ Tomb/         TBD — bone percussion, resonant breath, stone grinding.
│   └─ Shrine/       TBD — shuffling on wood, distant growl, cracking timber.
├─ Sidequest/        Altar: ancient resonant acceptance tone.
│                    Torch ignition: low whomp, crackle, spreading flame.
└─ UI/               Subtle. Menu click, score increment. Never jarring.

Torch/               Ignite whomp, steady flicker loop, extinguish hiss.




## Purpose

Formalizes how audio communicates the Heartbeat state, danger, and discovery. Audio is the primary tension delivery system for this game — visuals and UI stay minimal so sound carries the emotional load.

This doc defines what the audio system must do. Implementation code lives alongside systems/heartbeat_system.md once Milestone 8 starts.

---

# Design Principles

- Adaptive over fixed: audio changes because the Heartbeat state changed, not on a timer.
- Restraint over loudness: a precisely-timed quiet sound is scarier than a loud one. During tense phases (Alert, Fear, Panic, Escape) prefer ambience, environmental tones, and sparse stings over continuous music. The Music Arc uses melody only during Calm/hub — it does not play during tension.
- Silence is a tool: the moment right after a jumpscare sting should often be near-total silence, not a fade-out. This is a deliberate cue, not a bug.
- Diegetic-first: prioritize sounds the player can believe come from the world (dripping water, distant footsteps) over abstract score, especially during Calm and Alert.
- Withdrawal signals danger: removing audio layers is as powerful as adding them. The disappearance of music tells the player something has changed — without any UI.

---

# Music Arc

The game uses a progressive withdrawal of music to signal escalating danger — not a static "music on/off" switch, and not silence from the start.

| Phase | Music State |
|---|---|
| Hub / level start | Melodic, period-appropriate Victorian orchestral exploration music. Humable. Establishes the era and the player's identity as a respectable explorer. |
| Mid-exploration (Calm → Alert) | Melody strips away as Heartbeat rises. Rhythm dissolves first, then melody fades. Musical texture remains — sustained tones, no identifiable rhythm or melody. |
| Alert / Fear states | Musical texture only — a bowed string sustain, a low organ tone. Felt rather than consciously heard. Indistinguishable from ambience to a casual listener. |
| Escape / Panic | No music of any kind. Pure SFX, ambience, heartbeat, breathing, monster sounds. The monster's own audio fills the soundscape — footsteps getting closer, roar, breath — real, directional, and responsive to the player's actions. |

**Rule:** If the player can hum it, it does not play after mid-exploration. The withdrawal of melody IS the signal that something fundamental has changed. Earned silence is more powerful than any composed piece at this moment. During Panic and Escape there is no music of any kind — no melody, no texture, no sustained tone. The arc completes in silence. The monster fills the soundscape.

**No melodic music is reintroduced during Escape.** Period-appropriate pieces exist (e.g. Rimsky-Korsakov's Flight of the Bumblebee, 1899) but carry cultural associations — comedy, cartoons, chase parody — that would break tension at the worst moment. The monster is the soundtrack during Escape.

Implementation: melody, rhythm, and texture layers exist as separate tracks in the Unity Audio Mixer. Melody and rhythm layers fade out as Heartbeat state rises. The same snapshot transition system handles this as the ambience layer transitions.

---

# Heartbeat State → Mixer Snapshot Mapping

Each Heartbeat state (see systems/heartbeat_system.md) maps to one Unity Audio Mixer Snapshot. Transitions use `AudioMixerSnapshot.TransitionTo(blendTime)`, never a hard cut — the blend time itself is a tension tool.

| State | BPM | Music Layer | Ambience / SFX | Blend In Time |
|---|---|---|---|---|
| Calm | 60 | Melodic Victorian exploration music, full volume | Base ambience only. No heartbeat audio. | 1–2s, gentle |
| Alert | 90 | Melody fading — texture only beginning | Base ambience + low tension drone. Faint heartbeat at low volume. Faint breathing audible. | 0.5–1s |
| Fear | 120 | Musical texture only (sustained strings/organ) | Ambience narrows (high frequencies rolled off). Heartbeat louder/faster. Noticeable breathing. Whisper / distant-threat layer added. | 0.3–0.5s |
| Panic | 150 | No music | Heartbeat dominates the mix. Most ambience ducked. Heavy breathing loop prominent. Monster sounds dominate. | Under 0.2s |

De-escalation (Panic → Calm) should blend slower than escalation. Escalation should feel sudden; coming down should feel earned, not instant. The melody layer does not return until the player returns to the Hub between expeditions.

---

# Breathing Audio

Breathing is both what the player hears (internal feedback) and what the monster detects (external noise source). See systems/heartbeat_system.md — Breathing.

The breathing loop that plays for the player is the same signal the monster's detection system reads. When the player hears their breathing calming in a hiding spot, that is the accurate signal that their noise output is dropping.

The breathing loop should feel involuntary and physical — not stylized. It is not a music element. It is the player's body betraying them.

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

## The Tomb (Egyptian — Planned, Future Vision)

- Calm: TBD — dry sand wind, distant resonance
- Alert: TBD — stone grinding, muffled footsteps added
- Fear: TBD — bone percussion, low resonant breathing added
- Panic: TBD

## The Shrine (Japanese — Planned, Future Vision)

- Calm: TBD — wind through bamboo, distant temple bell
- Alert: TBD — shuffling on wood, distant growl added
- Fear: TBD — heavy breathing, cracking wood added
- Panic: TBD

Implementation hint: this maps naturally onto a `MythologyAudioProfile` ScriptableObject holding one AudioClip set per Heartbeat state. One profile asset per mythology; the Heartbeat/AudioManager just asks for "the Fear-state clips for the active profile" without needing to know which mythology is loaded.

---

# Safe Zone Audio

The safe zone is not announced — no UI label, no musical sting on entry. The player learns they are in a safe zone through sustained absence of monster detection and a subtle shift in acoustic feel.

Inside a safe zone the acoustic space narrows — slightly shorter reverb tail, lower ambient noise floor. This is a design intent for the sound designer, not a hard technical specification. The effect should be felt subconsciously, not noticed consciously.

The breathing loop continues inside the safe zone until heartbeat calms. The player hears their own breathing drop from heavy → faint → near-silent. That audio decay is the signal: now it is safe to consider leaving. See systems/safe_zones.md — Two-Stage Hiding Experience.

---

# Hunted Mode Audio — Escape Trigger One-Shot

When `OnEscapeTriggered` fires (Major Treasure collected), two audio events happen simultaneously:

1. **HeartbeatManager transition to Panic** — automatic; AudioManager already snaps to the Panic snapshot (no music, heartbeat dominates, heavy breathing). This is the internal response.
2. **Monster vocalization one-shot** — a single mythology-specific monster sound plays at that moment. This is the external signal: the creature heard you, and it is coming.

| Mythology | One-shot vocalization |
|---|---|
| Greek / Minotaur | Deep resonant roar — bull-throated, close, from no fixed direction |
| Norse / Jörmungandr | Long hiss — slow, deliberate, like something enormous becoming aware |
| Egyptian (planned) | TBD |
| Japanese (planned) | TBD |

The vocalization is diegetic — it comes from the monster, not from a score cue or UI event. It must never feel like a musical sting. It is the creature announcing itself.

**Implementation (M11):** Fire the one-shot from `AudioManager` on `GameManager.OnEscapeTriggered`. A single `AudioSource.PlayOneShot(escapeVocalizationClip)` at the moment of trigger. One clip per mythology, configurable per `MythologyAudioProfile`.

See systems/heartbeat_system.md — Phase Shift / Hunted Mode.

---

# Sneak Mode Audio

Sneak is activated by holding Left Ctrl. The breath-hold is communicated entirely through audio — no UI indicator, no visual feedback.

| Moment | Audio |
|---|---|
| Ctrl pressed (activation) | Deep inhale clip plays once — the player consciously taking a breath before holding it |
| While holding | Breathing loop stops — the player cannot hear themselves breathing; silence is the signal |
| Voluntary release (Ctrl released before timer) | Quiet exhale; no sting; no drama |
| Forced exhale (timer expires) | Sharp exhale with a subtle tension sting — the body betraying the player |

The absence of the breathing loop during a hold is the primary feedback. The player "knows" they are quiet because they cannot hear themselves. When the forced exhale fires, the sting makes the cost immediate and visceral.

See docs/mechanics.md — Sneak Mode.

---

# Sidequest Audio

## Altar Offering Acceptance

When the player places the third collectible at the altar, a ritual acceptance sound fires. This sound is:
- Resonant and deliberate — not a UI success chime. Something ancient acknowledging the offering.
- Loud enough that the monster may hear it from nearby. Completing the sidequest is a moment of vulnerability as well as reward.
- Followed by a brief near-silence, then the hidden room mechanism sound (stone moving, rope tightening, door releasing).

## Hidden Room Torch Ignition

When the hidden room opens after the offering, torches ignite automatically — not because the player entered, but because the ritual was completed. The ignition sound should feel like a response, not a coincidence:
- A low whomp of flame catching, spreading along the wall
- A brief crackle as the room fully illuminates
- Then the full lit acoustic of the room — slightly warmer, more reverberant than the corridor outside

Both sounds are mythology-specific in character (the altar sound for an Egyptian tomb will differ from a Norse temple), but the same ritual intent applies across all mythologies.

---

# Jumpscare Trigger Rules

- A jumpscare pushes the Heartbeat state up by exactly one step (e.g. Alert → Fear), never directly to Panic. Panic is reserved for the Escape Phase.
- Minimum cooldown between scripted jumpscares within a single Exploration phase, to avoid desensitizing the player (exact value: playtesting).
- Immediately after a jumpscare sting, cut most other audio for a brief moment before resuming ambience — the silence is the second half of the scare.
- Jumpscares are tied to Minor Treasure pickups and specific interactions (per mechanics.md), not random ambient timers. The player's own curiosity should cause the scare — that reinforces the "knowledge is risk" loop the rest of the design already leans on.

## Phobia Pool Audio

Jumpscares draw from a phobia pool with per-run weighted randomisation. Each phobia type has a distinct audio expression. The full pool and type definitions live in docs/mechanics.md — Jumpscare / Phobia Pool. Audio responsibilities per type:

| Phobia | Audio expression |
|---|---|
| Acousticophobia | Sudden loud audio sting — the core jumpscare sound. Direction varies per trigger. |
| Scopophobia | A breath or subtle sound from an unexpected direction — the sense of being heard before being found |
| Nyctophobia | A deepening of silence just before something happens — audio duck, then sting |
| Thanatophobia | Environmental — creak, settling, the sound of something long dead shifting |
| Entomophobia | Skittering burst, brief and directional |
| Chiroptophobia | Rush of wing-beats from darkness |
| Musophobia | Rapid scrabbling, brief, from floor level |

All phobia audio must respect the diegetic-first principle — every sound can be believed to come from the world, not from a soundtrack.

---

# Implementation Priority

- V1: Unity's built-in AudioSource + AudioMixer + Snapshots. This is sufficient for the entire Vertical Slice.
- Later, optional, not before V1 works: FMOD or Wwise, only if the built-in mixer becomes a real limitation you've actually hit. Don't reach for middleware before you've felt the built-in tools' ceiling yourself.

---

# Open Questions

- Exact jumpscare cooldown value — needs playtesting, not a design decision yet.
- How many ambience layers can play simultaneously before the mix becomes muddy on typical hardware? Needs implementation testing, not a design decision yet.
