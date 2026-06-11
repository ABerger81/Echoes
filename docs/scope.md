# Game Prototype Scope – Version 1

## MVP

Current Development Scope

Features:

* Movement
* Interaction
* Treasure Collection
* Score
* Death
* Basic UI

Not Included:

* Multiple Regions
* Complex AI
* Story Campaign
* Procedural Generation
* Multiplayer

---

## Expansion

Future Features

* Heartbeat System
* Environmental Tension
* Safe Zones
* Mythology Clues

---

## Vertical Slice

One complete mythology level.

---

## Future Vision

Full game with multiple mythological regions.

---

## Scope Rule

Any new feature must be classified before implementation:

* MVP
* Expansion
* Vertical Slice
* Future Vision


---

## Learning Goals

Learn:

* Prefabs
* Raycasting
* Game state management
* UI systems
* Audio systems
* Basic Unity architecture

---

## Milestone 1 – Player Movement

### Implementation Checklist

* [x] Scene created
* [x] First Person Controller imported
* [x] WASD movement works
* [x] Mouse look works

### Definition of Done

Movement is considered complete when:

* Player can move without errors
* Camera rotation is smooth
* Movement speed is consistent
* Controls feel responsive during testing

---

## Milestone 2 – Mining System

### Implementation Checklist

* [ ] Center-screen raycast exists
* [ ] Left click sends raycast
* [ ] Raycast detects Block
* [ ] Hit block can be mined
* [ ] Mined block disappears

### Definition of Done

Mining is considered complete when:

* Player can reliably mine blocks
* Incorrect objects are not mined
* Mining interaction feels predictable
* No obvious interaction bugs occur during testing

---

## Milestone 3 – Block Types

### Implementation Checklist

* [ ] Block prefab created
* [ ] BlockType enum created
* [ ] Stone block implemented
* [ ] Gold block implemented
* [ ] Monster block implemented
* [ ] Gold increases score
* [ ] Monster triggers death event

### Definition of Done

Block system is considered complete when:

* All block types behave correctly
* New block types can be added without modifying the mining system
* Block behavior is determined by BlockType

---

## Milestone 4 – Death State

### Implementation Checklist

* [ ] Death state exists
* [ ] Player input is disabled on death
* [ ] Death event triggers correctly

### Definition of Done

Death system is considered complete when:

* Player can no longer interact after death
* Death consistently transitions to Game Over
* No gameplay systems continue running unexpectedly

---

## Milestone 5 – UI System

### Implementation Checklist

* [ ] Score text displayed
* [ ] Score updates when gold is collected
* [ ] Game Over screen appears
* [ ] Restart button works

### Definition of Done

UI is considered complete when:

* Information is clearly visible
* UI updates correctly during gameplay
* Restart flow works reliably

---

## Milestone 6 – Audio Tension System

### Implementation Checklist

* [ ] AudioManager exists
* [ ] Ambient audio plays
* [ ] Monster event plays jumpscare sound
* [ ] Audio changes based on game state

### Definition of Done

Audio system is considered complete when:

* Audio responds correctly to gameplay events
* Volume levels are reasonable
* Sounds trigger consistently
* Audio improves player awareness and atmosphere

---

## Core Systems

* PlayerController
* MiningSystem
* Block
* GameManager
* UIManager
* AudioManager

---

## Development Principle

Implementation tasks use checkboxes.

Subjective quality, feel, balance, atmosphere, and polish are evaluated through Definition of Done sections and playtesting rather than checklists.
