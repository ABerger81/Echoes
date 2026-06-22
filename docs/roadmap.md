# Roadmap

## Purpose

This document describes the planned evolution of the project.

The roadmap is divided into development phases.

Features should only be implemented when their phase becomes active.

---

# MVP

Goal:

Validate the core gameplay loop.

Features:

* First Person Controller
* Interaction System
* Treasure Collection
* Score System
* Basic Escape
* Death / Capture State
* UI

Success Criteria:

* Complete playable loop exists
* Player can win
* Player can lose

---

# Expansion

Goal:

Validate tension and atmosphere.

Features:

* Heartbeat System (state machine + noise/breathing drivers)
* Adaptive Audio (mixer snapshots, music arc, mythology-specific ambience)
* Safe Zones (binary protection, monster threshold, two-stage hiding)
* Pause Menu
* Monster AI (noise-based wandering + active hunt on escape trigger)
* Lighting Effects
* Basic Horror Events (jumpscares)

Success Criteria:

* Player experiences tension
* Player understands danger escalation
* Player understands the monster responds to their noise, not their position

---

# Vertical Slice

Goal:

Build one complete mythology level.

Features:

* One mythology, one monster (Greek — Minotaur Labyrinth)
* Sidequest system (3 collectibles → altar → hidden room → knowledge)
* Level design pass (12–18 rooms, correct placement of all elements)
* Player hands (two visible first-person hands, light source in left hand)
* Local high score screen (PlayerPrefs, top 10 per level)

Success Criteria:

* Representative gameplay experience
* Suitable for public testing
* A new player can be handed a build with no instruction and understand the game

---

# Future Vision

See docs/vision.md for long-term goals and docs/scope.md for full feature breakdown.
