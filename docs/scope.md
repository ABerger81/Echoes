# Game Prototype Scope – Version 2

## MVP

Current Development Scope

Goal: Validate the core gameplay loop (matches Roadmap MVP).

Features:

* Movement
* Interaction
* Treasure Collection
* Score
* Basic Escape
* Death / Capture State
* Basic UI

Not Included:

* Heartbeat / Tension System
* Adaptive Audio
* Mythology Clues
* Multiple Regions
* Complex Monster AI
* Story Campaign
* Procedural Generation
* Multiplayer

Note: Heartbeat, Clues, and full Audio Tension are intentionally excluded from MVP. Per roadmap.md, they belong to the Expansion phase. The MVP's only job is to prove explore → collect → escape → win/lose is fun with placeholder feedback (basic SFX, simple UI text) before tension systems are layered in.

---

## Expansion

Goal: Validate tension and atmosphere on top of a working MVP loop.

Features:

* Heartbeat System
* Adaptive / Audio Tension System
* Safe Zones
* Mythology Clues
* Lighting Effects
* Basic Horror Events (jumpscares)

Success Criteria:

* Player experiences tension
* Player understands danger escalation

---

## Vertical Slice

One complete mythology level. MVP + Expansion features combined = portfolio piece, suitable for public playtesting.

---

## Future Vision

Full game with multiple mythological regions = Steam-shippable scope. See vision.md.

---

## Scope Rule

Any new feature must be classified before implementation:

* MVP
* Expansion
* Vertical Slice
* Future Vision

If a feature can't be classified yet, it goes in the relevant doc's Open Questions section — not into a milestone checklist.

---

## Learning Goals

Learn:

* Prefabs
* Raycasting
* Game state management
* UI systems
* Audio systems (basic, then adaptive)
* Event-driven architecture (C# events / ScriptableObject event channels)
* Basic Unity architecture
* Git hygiene for Unity projects

---

## MVP Milestones

### Milestone 1 – Player Movement (Complete)

Checklist:

* [x] Scene created
* [x] First Person Controller imported (Unity Starter Assets)
* [x] WASD movement works
* [x] Mouse look works

Definition of Done:

* Player can move without errors
* Camera rotation is smooth
* Movement speed is consistent
* Controls feel responsive during testing

---

### Milestone 2 – Interaction System (Complete)

Checklist:

* [x] Center-screen raycast exists
* [x] Interact key sends raycast
* [x] Raycast detects Interactable objects
* [x] Interaction triggers a response (pickup / inspect)
* [x] Interacted object disappears or changes state

Definition of Done:

* Player can reliably interact with intended objects
* Non-interactable objects are ignored
* Interaction feels predictable
* No obvious interaction bugs occur during testing

---

### Milestone 3 – Treasure & Score System (complete)

Checklist:

* [x] Treasure prefab created
* [x] TreasureType enum created (Minor, Major)
* [x] Minor Treasure implemented (increases score)
* [x] Major Treasure implemented (triggers Escape Phase)
* [x] GameManager tracks and exposes current score

Definition of Done:

* All treasure types behave correctly on pickup
* New treasure types can be added without modifying the interaction system
* Treasure behavior is determined by TreasureType, not hardcoded per object

---

### Milestone 4 – Basic Escape (Complete)

Checklist:

* [x] Major Treasure pickup raises an "Escape Triggered" event
* [x] Exit point exists and is detectable
* [x] Reaching the exit after escape is triggered ends the level successfully
* [x] (Optional for MVP) A simple non-AI threat exists — e.g. a timer, or a placeholder chaser with no real pathfinding

Definition of Done:

* The explore → trigger → escape → win loop is completable start to finish
* Player understands they must reach the exit once escape is triggered
* No horror/AI polish required yet — this milestone only proves the loop works

---

### Milestone 5 – Death / Capture State (Complete)

Checklist:

* [x] Death/Capture state exists
* [x] Player input is disabled on capture
* [x] Capture event triggers correctly
* [x] Game Over state is reached reliably
* [x] Capture feedback is shown to the player (text or screen effect)

Definition of Done:

* Player can no longer interact after capture
* Capture consistently transitions to Game Over
* No gameplay systems continue running unexpectedly

---

### Milestone 6 – UI System

Checklist:

* [ ] Score text displayed
* [ ] Score updates when treasure is collected
* [ ] Game Over screen appears
* [ ] Restart button works

Definition of Done:

* Information is clearly visible
* UI updates correctly during gameplay
* Restart flow works reliably

---

## Expansion Milestones (Do Not Start Before MVP Milestones 1–6 Are Done)

### Milestone 7 – Heartbeat & Tension System

Checklist:

* [ ] Heartbeat state machine exists (Calm, Alert, Fear, Panic)
* [ ] State transitions are triggered by gameplay events (proximity, jumpscares, escape trigger)
* [ ] Each state has at least a placeholder audio/visual response

Definition of Done: see systems/heartbeat_system.md

---

### Milestone 8 – Audio Tension System

Checklist:

* [ ] AudioManager exists
* [ ] Mixer Snapshots exist for each Heartbeat state
* [ ] Ambient ground layer plays and loops correctly
* [ ] Jumpscare audio events trigger correctly

Definition of Done: see docs/audio_design.md

---

### Milestone 9 – Safe Zones

Checklist:

* [ ] Safe Zone trigger volume exists
* [ ] Heartbeat decreases while inside a Safe Zone
* [ ] Safe Zone has a time limit during Escape Phase

Definition of Done:

* Player understands a Safe Zone is "safer," not "safe forever"
* Time pressure is felt without feeling unfair

---

### Milestone 10 – Mythology Clue System

Checklist:

* [ ] Clue objects exist and are discoverable
* [ ] Clues are recorded somewhere the player can recall (journal/UI)
* [ ] At least one clue meaningfully changes player behavior during Escape Phase

Definition of Done: see systems/clue_system.md

---

## Core Systems

MVP:

* PlayerController
* InteractionSystem
* TreasureSystem
* EscapeSystem (basic)
* GameManager
* UIManager
* AudioManager (basic)

Expansion adds:

* HeartbeatSystem
* ClueSystem
* EscapeSystem (full — chase AI, safe zone timers)
* AudioManager (adaptive — mixer snapshots, layered ambience)

---

## Development Principle

Implementation tasks use checkboxes.

Subjective quality, feel, balance, atmosphere, and polish are evaluated through Definition of Done sections and playtesting rather than checklists.
