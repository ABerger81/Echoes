# Mechanics

## Purpose

This document contains all gameplay mechanics.

Mechanics describe rules — what a system does from the player's perspective. They do not describe implementation (see architecture/ and systems/ for that), and they do not duplicate audio implementation detail (see docs/audio_design.md for that).

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

* Minor Treasure — increases score, carries a small chance of triggering a jumpscare/tension increase when collected.
* Major Treasure — exists exactly once per level, triggers the Escape Phase when collected.

## Inputs

* Interaction Key

## Outputs

* Score Increase
* (Major only) Escape Triggered event

## Placement Rule

Major Treasure is not placed at a fixed location. It is placed in a room selected from a set of pre-placed candidate rooms (or, later, from rooms whose graph distance from the entrance is above a minimum threshold), re-rolled every playthrough. This guarantees the player explores most of the level before triggering Escape, and prevents memorization from making the chase trivial on repeat plays.

## Related Systems

* Score System
* Escape System

## Open Questions

* What exact distance/placement rule feels fair vs. tedious? Needs playtesting, not a design decision yet.
* Should Minor Treasure jumpscare chance scale with how many Minor Treasures have already been collected (risk escalates with greed), or stay constant?

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

A discrete space — a hiding spot — where the monster cannot detect the player at all, for a limited time. During Exploration, Safe Zones have no time limit. During Escape Phase, Safe Zones have a time limit — staying too long forces the player back into danger. This is complete, binary protection, distinct from the continuous Light Exposure trade-off below.

## Inputs

* Player entering/staying in a Safe Zone trigger volume

## Outputs

* Heartbeat decrease (Exploration)
* Time-limited heartbeat decrease, then forced exit (Escape Phase)

## Related Systems

* Heartbeat System
* Escape System

## Open Questions

* How long is the Escape Phase time limit, and does it vary by mythology/level?

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

Note: the MVP version of this mechanic (scope.md Milestone 4) is intentionally minimal — a timer or a placeholder threat with no real pathfinding. The full noise-based hunter described above is an Expansion-phase target (Milestone 9+), not something built on day one.

## Inputs

* Escape Triggered event
* Player noise level (footsteps, sprinting, voice)
* Time since the monster last heard a noise

## Outputs

* Monster movement toward last heard noise, or wandering
* Success: Score Saved, Leaderboard Entry
* Failure: Reduced Score, Game Over

## Related Systems

* Heartbeat System
* Light / Torch Trail
* AudioManager
* UIManager (Game Over screen)

## Open Questions

* If caught, is it instant Game Over, or a short forced "capture" sequence first?

---

# Clue / Fact-Based Navigation

## Description

During Exploration, the player learns a mythology-specific fact (e.g. "the Minotaur follows blood trails"). During Escape, that fact becomes a usable navigation aid (e.g. blood trail symbols on walls indicate a faster route).

## Inputs

* Clue discovered during Exploration
* Matching environmental marker present during Escape

## Outputs

* Faster/safer route available to a player who learned and remembers the clue

## Related Systems

* Exploration System
* Escape System

## Open Questions

* Is the clue-to-action link purely environmental, or does the game ever give an explicit UI prompt?
* How many clues are needed per level — one critical clue, or several redundant ones in case the player misses one?

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
* Exact randomized variance range for decay rate, and whether occasional flicker/gutter events are worth the extra implementation cost.
* Does the glow floor's appearance differ per mythology (embers vs. faint fungal glow vs. something else)? Likely yes — may warrant its own short doc later, the same way audio got split out of mechanics.md into audio_design.md, once this system is actually being built.

---

# Jumpscare

## Description

A scripted micro-climax tied to specific triggers (examining certain objects, crossing certain thresholds). Pushes the Heartbeat state up by one step, not directly to Panic, to preserve pacing.

## Inputs

* Trigger volume or interaction event

## Outputs

* Audio sting (see audio_design.md)
* Heartbeat state increase by one step

## Related Systems

* Heartbeat System
* AudioManager

## Open Questions

* Should jumpscare triggers and types be randomized per playthrough, to avoid the second playthrough feeling scripted/safe?
* Minimum cooldown between jumpscares, to avoid desensitizing the player?
