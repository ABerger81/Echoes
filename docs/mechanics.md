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

Major Treasure is not placed at a fixed location. It is placed in a room selected from the set of rooms whose graph distance from the entrance is above a minimum threshold (e.g. at least 60% of the maze's longest path), re-rolled every playthrough. This guarantees the player explores most of the level before triggering Escape, and prevents memorization from making the chase trivial on repeat plays.

## Related Systems

* Score System
* Escape System

## Open Questions

* What exact distance threshold feels fair vs. tedious? Needs playtesting, not a design decision yet.
* Should Minor Treasure jumpscare chance scale with how many Minor Treasures have already been collected (risk escalates with greed), or stay constant?

---

# Heartbeat

## Description

Represents the player's stress level as a state machine (Calm, Alert, Fear, Panic). Drives audio and visual feedback. Full state definitions live in systems/heartbeat_system.md; audio implementation lives in docs/audio_design.md.

## Inputs

* Scary Events (jumpscares)
* Monster Proximity
* Escape Phase trigger

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

A space where the player's heartbeat decreases over time. During Exploration, Safe Zones have no time limit. During Escape Phase, Safe Zones have a time limit — staying too long forces the player back into danger.

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
* Can the monster ever enter a Safe Zone, or is it strictly off-limits?

---

# Escape / Chase

## Description

Triggered by Major Treasure collection. The monster activates and the player must reach the level exit. Tension is communicated almost entirely through audio and Heartbeat state, since the player cannot fight.

Note: the MVP version of this mechanic (scope.md Milestone 4) is intentionally minimal — a timer or a placeholder threat with no real pathfinding. The full chase behavior described below is an Expansion-phase target (Milestone 7+), not something built on day one.

## Inputs

* Escape Triggered event
* Monster proximity / line of sight

## Outputs

* Success: Score Saved, Leaderboard Entry
* Failure: Reduced Score, Game Over

## Related Systems

* Heartbeat System
* AudioManager
* UIManager (Game Over screen)

## Open Questions

* Is the monster's behavior scripted (fixed patrol/predictable route) or reactive (chases based on player position/noise)? This decision changes both difficulty tuning and implementation effort significantly, and needs to be decided before building the Expansion-phase version, not during it.
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

* Is the clue-to-action link purely environmental (player has to recognize the symbol themselves), or does the game ever give an explicit UI prompt? Purely environmental is scarier but riskier for new players to understand at all.
* How many clues are needed per level — one critical clue, or several redundant ones in case the player misses one?

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
