# Mechanics

## Purpose

This document contains all gameplay mechanics.

Mechanics describe rules — what a system does from the player's perspective. They do not describe implementation (see architecture/ and systems/ for that), and they do not duplicate audio implementation detail (see docs/audio_design.md for that).

---

# Player Movement

Three movement modes: Walk (WASD), Sprint (WASD + Hold Shift), and Sneak (WASD + Hold Ctrl, added M11).

**No jump mechanic.** Jumping does not fit the horror tone, breaks the level geometry assumptions (vault over barriers intended to force routing decisions), and creates hiding exploits. This is a permanent design decision — not a deferred feature.

## Related Systems

- systems/heartbeat_system.md — noise levels per movement mode
- docs/mechanics.md — Sneak Mode

---

# Sneak Mode

## Description

Third movement mode. Hold Left Ctrl to initiate a breath-hold. A deep inhale audio cue plays on activation — the player is consciously suppressing their breathing.

The player can hold their breath for 8–12 seconds. Two outcomes when they stop:

- **Voluntary release** (player releases Ctrl before timer): no noise burst. Releasing on time costs nothing.
- **Forced exhale** (timer expires): small `AddNoiseBurst`. Holding too long has a penalty.

While holding breath, movement noise level drops to 0.1 — below the Alert threshold (0.25). Moving slowly while sneaking does not escalate heartbeat state, provided pressure is already below 0.1. Pressure above 0.1 continues to decay naturally regardless.

Sneak is slower than Walk (~40–50% of walk speed). The noise benefit comes at a movement cost. This is a genuine choice: slower and quieter vs. faster and louder.

**Hold, not toggle.** Toggle allows set-and-forget sneaking. Hold keeps sneak intentional — the player must commit to each use.

## Safe Zone Constraint

Sneak cannot be used to accelerate breathing decay inside a safe zone. `IsInSafeZone` already blocks `SetContinuousNoise` entirely, so the sneak noise level (0.1) has no effect when already inside. Holding Ctrl inside a safe zone is neutral — it does not help or hurt.

## Inputs

* Left Ctrl (Hold)
* WASD (simultaneous)

## Outputs

* Deep inhale audio cue on activation
* Movement noise level 0.1 while holding
* Small `AddNoiseBurst` on forced exhale (timer expired only)

## Milestone

M11 (Expansion). See systems/heartbeat_system.md — Sneak Mode.

## Related Systems

* Heartbeat System
* AudioManager — sneak breath audio

---

# Interaction

## Description

Player can interact with marked objects in the world via a center-screen raycast.

## Inputs

* Interaction Key
* Look Direction (raycast origin/direction)

## Outputs

* Object response (pickup, inspect, trigger)

## Related Systems

* TreasureSystem
* ClueSystem

---

# Treasure Collection

## Description

Player collects treasure to increase score. Treasure comes in two types:

* Minor Treasure — increases score, carries a scaled chance of triggering a jumpscare/tension increase when collected. Risk escalates with greed — the more collected, the higher the chance.
* Major Treasure — exists exactly once per level, triggers the Escape Phase when collected.

## Inputs

* Interaction Key

## Outputs

* Score Increase
* (Major only) Escape Triggered event
* (Minor) Jumpscare chance — probability scales with collection count (see Jumpscare Scaling below)

## Count Per Level

**5–7 Minor Treasures per level.** Below 5: not enough steps for the greed curve to build. Above 8: starts feeling like a checklist rather than discovery. 6–7 is the target — enough for meaningful cabinet display, enough greed-decision moments, not enough to exhaust.

The sidequest collectibles (3 per level, see systems/clue_system.md) are separate items — they go to the altar, not score, and occupy distinct curio cabinet slots.

## Level Size (follows from count)

Design from time and rooms, not square metres:

| Target | Value |
|---|---|
| First-time thorough exploration | 15–25 minutes |
| Treasure-bearing rooms | ~1 per Minor Treasure |
| Total distinct areas | 12–18 rooms + corridors + dead ends |
| Dead end percentage | 30–40% — where treasures hide, rewarding exploration over rushing |

A player following only the main path should find 1–2 Minors at most. A player who investigates every branch finds 5–7. The level physically enforces the exploration-vs-rush decision.

## Jumpscare Scaling

Jumpscare chance uses a normalized curve: `t = collected / totalInLevel`, evaluated against a probability curve. This normalises automatically — the same formula works regardless of how many Minors the level contains.

**Formula guideline by count:**
- 7–8 Minors → quadratic (t²): ramps at a comfortable pace, greed bites in the second half
- 5–6 Minors → cubic (t³): extends the safe early window; quadratic punishes too hard too fast with fewer steps

**Implementation: Unity `AnimationCurve` (recommended).** Draw the curve in the Inspector — no formula change required to tune. X axis: 0.0 (no pickups) to 1.0 (all collected). Y axis: 0.0 to target max chance.

```csharp
[SerializeField] private AnimationCurve jumpscareScaling;

private float JumpscareChance()
{
    float t = (float)_minorCollected / _minorTreasureCount;
    return jumpscareScaling.Evaluate(t);
}
```

**Cap at 70–80%,** not 100%. At 100% the last pickup is a guaranteed scare — certainty removes tension. A 70–80% cap means even the final pickup can surprise by *not* triggering.

## Placement Rule

Major Treasure is not placed at a fixed location. It is placed in a room selected from a set of pre-placed candidate rooms (or, later, from rooms whose graph distance from the entrance is above a minimum threshold), re-rolled every playthrough. This guarantees the player explores most of the level before triggering Escape, and prevents memorization from making the chase trivial on repeat plays.

## Related Systems

* Score System
* Escape System

## Open Questions

* ~~Should Minor Treasure jumpscare chance scale with collection count, or stay constant?~~ — **Resolved:** Scales. Quadratic for 7–8 Minors, cubic for 5–6. AnimationCurve for tuning. Cap at 70–80%. 5–7 Minors per level target.
* What exact distance/placement rule for Major Treasure feels fair vs. tedious? Needs playtesting, not a design decision yet.

---

# Heartbeat

## Description

Represents the player's stress level as a state machine (Calm, Alert, Fear, Panic). Drives audio and visual feedback. Full state definitions live in systems/heartbeat_system.md; audio implementation lives in docs/audio_design.md.

## Inputs

* Scary Events (jumpscares)
* Monster Proximity
* Escape Phase trigger
* Ambient Light Exposure (continuous — see Light Exposure below)

## Outputs

* State change (Calm → Alert → Fear → Panic)
* Audio Changes (see audio_design.md)
* Visual Effects (peripheral darkening, etc.)

## Related Systems

* Exploration
* Escape
* AudioManager

---

# Safe Zone / Hiding

## Description

A discrete space — a hiding spot — where the monster cannot detect the player at all. Complete, binary protection while inside, distinct from the continuous Light Exposure trade-off below.

**No forced exit timer.** The pressure to leave comes entirely from monster behavior, not a countdown. This matches the emotional truth of the genre: in horror, the person hiding decides when to move based on sound — footsteps, breathing, searching noises. They leave when *they* judge it's safe enough, not when a UI tells them to.

During **Exploration**, Safe Zones have no time limit and no pressure — hiding is genuine rest.

During **Escape Phase**, the monster is actively hunting. It wanders toward last-heard sounds and re-sweeps areas. If the player is completely silent inside the hiding spot, the monster eventually loses the scent and moves elsewhere. The player hears it moving away — that is their cue. Leaving too early (monster still nearby) means making footstep noise at close range, the worst possible moment. Camping indefinitely is naturally prevented by the monster eventually wandering back to re-check the area.

The hiding loop is: hear it searching → stay still and silent → hear it move away → judge the moment → leave → risk immediate re-detection if wrong.

## Inputs

* Player entering a Safe Zone trigger volume
* Monster proximity and patrol state (drives whether the hiding spot feels safe)

## Outputs

* Heartbeat decrease while inside (Exploration and early Escape Phase)
* Heartbeat rise if monster lingers nearby outside (proximity audio cue even while hidden)
* Player decision: stay or go, based entirely on audio

## Related Systems

* Heartbeat System
* Escape System

## Open Questions

* ~~How long is the Escape Phase time limit, and does it vary by mythology/level?~~ — **Resolved:** No forced timer. Monster patrol behavior creates natural pressure. Player decides when to leave based on audio cues — consistent with horror genre and the monster's established noise-based hunting behavior.

---

# Light Exposure (Ambient Safety Trade-off)

## Description

A continuous trade-off layered under all exploration and escape, separate from discrete Safe Zones. Standing in darkness lowers the player's chance of being detected by the monster, but Heartbeat rises gradually the longer they stay there. Standing in light keeps Heartbeat calmer and navigation easier, but increases the distance from which the monster can detect the player. Neither choice is fully safe — that's deliberate. There is no zone that is simply "safe."

## Inputs

* Player's current ambient light level (lit vs. dark area)
* Time spent at the current light level

## Outputs

* Heartbeat state drift (up in darkness over time, down in light)
* Detection range modifier for the monster

## Related Systems

* Heartbeat System
* Escape System
* Light / Torch Trail

## Open Questions

* How fast should Heartbeat drift in darkness — fast enough to matter, slow enough not to punish normal exploration?
* Exact detection range difference between lit and dark areas — needs playtesting.

---

# Escape / Chase

## Description

Triggered by Major Treasure collection. The monster activates, and as its first act, extinguishes or significantly dims the light the player is currently carrying. From that point, survival depends on noise discipline, knowledge from the Clue System, and reaching Safe Zones — not on outrunning the monster in a straight line.

The monster is a noise-based hunter with no sight or vision cone. It moves toward the most recent location it heard a sound, and wanders when nothing recent has been heard. Sprinting is loud and draws it directly; walking is quieter; standing fully still and silent for a few continuous seconds is the only way to make it lose the scent entirely — not instantly, so "freeze forever" isn't a guaranteed win. While actively hunting, its speed is slightly faster than the player's sprint speed, so running in a straight line alone is a losing strategy; surviving requires composure and knowledge, not just speed.

Note: the MVP version of this mechanic (scope.md Milestone 4) is intentionally minimal — a timer or a placeholder threat with no real pathfinding. The full noise-based hunter described above is an Expansion-phase target (Milestone 12), not something built on day one.

## Inputs

* Escape Triggered event
* Player noise level (footsteps, sprinting, voice)
* Time since the monster last heard a noise

## Outputs

* Monster movement toward last heard noise, or wandering
* Success: final score shown on Win Panel (local high score screen added in Vertical Slice — Milestone 16)
* Failure: score reset to 0, shown on Game Over Panel

## Related Systems

* Heartbeat System
* Light / Torch Trail
* AudioManager
* UIManager (Game Over screen)

## Open Questions

* ~~If caught, is it instant Game Over, or a short forced "capture" sequence first?~~ — **Resolved:** Instant Game Over for MVP. A capture sequence (sound, visual effect) is deferred to Expansion alongside full monster AI. See systems/escape_system.md.

---

# Clue / Fact-Based Navigation

## Description

During Exploration, the player learns a mythology-specific fact about the monster (e.g. "the Minotaur circles its last heard position before moving on — staying still after it passes is safer than running"). During Escape, that fact becomes actionable (e.g. standing completely still when the monster passes rather than fleeing).

## Inputs

* Clue discovered during Exploration
* Matching environmental marker present during Escape

## Outputs

* Faster/safer route available to a player who learned and remembers the clue

## Related Systems

* Exploration System
* Escape System

## Open Questions

* ~~Is the clue-to-action link purely environmental, or does the game ever give an explicit UI prompt?~~ — **Resolved:** Environmental only. No UI prompt. Player interprets and acts; the game never confirms they understood correctly. See systems/clue_system.md.
* ~~How many clues are needed per level?~~ — **Resolved:** Multiple redundant clues via the sidequest hidden room. Missing all clues is acceptable — level is still completable, just harder. See systems/clue_system.md.

---

# Light / Torch Trail

## Description

The player lights a personal portable flame (a "fireObject" appropriate to the mythology — a torch, a burning relic fragment, etc.) from an initial light source found early during Exploration. While exploring, the player can light additional fixed light objects scattered through the level, building a personal trail back toward the entrance/exit.

Lit objects burn down over a lifespan but never fully extinguish on their own — they fade to a small, persistent glow floor. This guarantees some visibility along a remembered route without guaranteeing full visibility, and avoids the level ever going to a literal black screen. Each light's decay rate has a small randomized variance (and may occasionally flicker/gutter), so the trail's exact visibility differs slightly between playthroughs even when the player explores the same way — preserving "no perfect memorization" without changing the level geometry.

Decay should be visually noticeable during calm Exploration — a light dimming or flickering while the player can casually observe it — so it is foreshadowed rather than discovered as a problem only during the panic of Escape.

## Inputs

* Lighting interaction (Interact key, while carrying an active flame, on an unlit light object)
* Time since each light object was lit

## Outputs

* Visible light radius, decaying over time toward a guaranteed minimum glow
* Navigation aid for the return route

## Related Systems

* Heartbeat System (ambient light feeds the Light Exposure trade-off)
* Escape System (the monster extinguishes/dims the player's currently-carried light at the start of Escape Phase)
* Interaction System

## Open Questions

* Exact lifespan duration and minimum glow floor brightness — needs playtesting, not a design decision yet.
* Exact randomized variance range for decay rate — needs playtesting.
* ~~Are flicker/gutter events worth the extra implementation cost?~~ — **Resolved:** Yes. A reusable coroutine per lit light object — reduce intensity briefly, restore. Low cost, high atmospheric value. Reinforces that lit routes aren't guaranteed comfort. Frequency, depth, and duration are playtesting values.
* Does the glow floor's appearance differ per mythology (embers vs. faint fungal glow vs. something else)? Likely yes — may warrant its own short doc later, once this system is actually being built.

---

# Jumpscare

## Description

A micro-climax tied to specific triggers (examining certain objects, crossing certain thresholds, collecting Minor Treasures past the scaling threshold). Pushes the Heartbeat state up by one step, not directly to Panic, to preserve pacing.

Jumpscares draw from a **phobia pool** — a set of fear types tagged to each event. Because fear is personal (what scares one player may not scare another), broad coverage across multiple phobia types ensures the game reaches each player somewhere. Precedent: Resident Evil Village deliberately targeted a different phobia group in each chapter for exactly this reason.

## Phobia Pool

Each event is tagged with one or more `PhobiaType` values. At level start the game silently assigns random weights to each type for this run. Event selection prefers higher-weighted types — one run leans on claustrophobia and scopophobia, the next on thanatophobia and acousticophobia. The player never sees the weights.

| Phobia | Fear of | Expression in this game |
|---|---|---|
| Nyctophobia | Darkness | Candle system, glow floor, light decay — already core |
| Claustrophobia | Confined spaces | Narrowing corridors, low ceilings, rooms that feel smaller than they are |
| Kenophobia | Vast empty spaces | Sudden open chambers after tight passages — exposed, no cover |
| Scopophobia | Being watched | Glowing eyes in periphery, carved faces that seem to track, the sense of being known |
| Pediophobia | Uncanny human-like figures | Statues that were facing one way, now face another — no animation, just displacement |
| Catoptrophobia | Mirrors / reflections | Victorian era mirrors — a figure reflected that should not be there |
| Thanatophobia | Death / mortality | Skeletons of previous explorers, objects belonging to the dead, inscriptions counting down |
| Acousticophobia | Sudden loud sounds | Audio sting jumpscares — already inherent to the system |
| Arachnophobia | Spiders | Environmental webs, skittering sounds, shadow shapes (not the monster) |
| Entomophobia | Insects | Sound design, peripheral shadow movement |
| Chiroptophobia | Bats | Burst of wings from darkness in underground and cave areas |
| Musophobia | Rats / rodents | Skittering audio, brief movement in peripheral shadow |
| Ophidiophobia | Snakes | Core to Norse level; ambient elsewhere — serpentine cracks, dragging sounds |
| Trypophobia | Irregular holes / clusters | Surface texture and wall design — affects those who have it without others noticing |
| Acrophobia | Heights | Level-specific — platforms over drops, looking into pits (requires verticality) |

**Implementation scope:** Build 3–4 phobia types for MVP and Vertical Slice. Nyctophobia, Scopophobia, Acousticophobia, and Thanatophobia cover the most ground with the least additional work — they are already partially present. Expand with each new level.

## Randomization

Both **which triggers fire** and **which type fires** are randomized per playthrough:

- A randomized subset of eligible triggers is selected each run — the same interaction that scared you last run may do nothing this run
- The specific event drawn from the pool varies even when the same trigger fires
- Scripted story beats remain fixed; only ambient and treasure-adjacent jumpscares randomize
- Randomization respects the cooldown — no two jumpscares within the minimum cooldown window regardless of which triggers were selected

## Inputs

* Trigger volume or interaction event
* Minor Treasure collection beyond the jumpscare scaling threshold (see Treasure Collection)
* Per-run phobia type weights

## Outputs

* Phobia-typed audio and/or visual event (see audio_design.md)
* Heartbeat state increase by one step

## Related Systems

* Heartbeat System
* AudioManager

## Open Questions

* ~~Should jumpscare triggers and types be randomized per playthrough?~~ — **Resolved:** Yes. Both triggers and types randomize per run. Scripted beats stay fixed. Cooldown applies globally.
* Minimum cooldown between jumpscares — needs playtesting, not a design decision yet.
