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

* Heartbeat System (state machine + noise/breathing drivers)
* Adaptive Audio (mixer snapshots per state, music arc, mythology-specific ambience)
* Safe Zones (binary protection, monster threshold, two-stage hiding)
* Pause Menu (Resume, Restart, Quit, Settings)
* Monster AI (noise-based wandering + active hunt on escape trigger)
* Lighting Effects (torch decay, flicker, glow floor)
* Basic Horror Events (jumpscares)

Success Criteria:

* Player experiences tension
* Player understands danger escalation
* Player understands the monster responds to their behaviour, not their position

---

## Vertical Slice

One complete mythology level. MVP + Expansion features combined, plus:

* Sidequest system (3 collectibles → altar → hidden room → weakness knowledge + lore)
* Phobia pool (randomised jumpscare types and triggers per run)
* Player hands (two visible first-person hands, light source in left hand)
* High score screen (local, top 10 per level, PlayerPrefs)
* Level design pass (12–18 rooms, correct placement of all elements)

Goal: Portfolio piece, suitable for public playtesting.

Success Criteria:

* One complete playthrough feels polished end to end
* A new player can be handed a build with no instruction and understand the game
* The tone — horror, Victorian exploration, mythology — is fully communicated

---

## Future Vision

Full game with multiple mythological regions = Steam-shippable scope. See vision.md.

Includes: Hub world (Explorer's Club), curio cabinet, NPC, meta-quest, multiple levels, online leaderboard, Hub progression system.

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
* NavMesh and basic AI pathfinding
* Probability curves and AnimationCurve
* PlayerPrefs for local data persistence

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

### Milestone 3 – Treasure & Score System (Complete)

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
* [x] A simple non-AI threat exists — countdown timer, no real pathfinding

Definition of Done:

* The explore → trigger → escape → win loop is completable start to finish
* Player understands they must reach the exit once escape is triggered
* No horror/AI polish required — this milestone only proves the loop works

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

### Milestone 6 – UI System (Complete)

Checklist:

* [x] Score text displayed
* [x] Score updates when treasure is collected
* [x] Win screen appears with final score
* [x] Game Over screen appears with final score
* [x] Restart button works and reloads scene cleanly

Definition of Done:

* Information is clearly visible
* UI updates correctly during gameplay
* Restart flow works reliably
* Cursor is locked during play and released on end screens

---

## Expansion Milestones (Do Not Start Before MVP Complete)

### Milestone 7 – Heartbeat & Tension System (Complete)

Checklist:

* [x] Heartbeat state machine exists (Calm, Alert, Fear, Panic)
* [x] Noise hierarchy drives state transitions (idle/walk/collect/sprint)
* [x] Breathing is separate from heartbeat: heartbeat = internal, breathing = external/detectable
* [x] Breathing decays as heartbeat calms (not instant)
* [x] State transitions triggered by noise, monster proximity, escape trigger, jumpscares
* [x] Each state has at least a placeholder audio/visual response

Definition of Done: see systems/heartbeat_system.md

---

### Milestone 8 – Audio Tension System (Complete)

Checklist:

* [x] AudioManager exists
* [x] Mixer Snapshots exist for each Heartbeat state
* [x] Music arc implemented: melodic at level start → texture at Alert/Fear → silence at Panic
* [x] Ambient ground layer plays and loops correctly per mythology
* [x] Breathing loop audio tied to heartbeat state
* [x] Jumpscare audio events trigger correctly (API exists, but triggers are M14)
* [x] De-escalation blends slower than escalation

Definition of Done: see docs/audio_design.md

---

### Milestone 9 – Safe Zones (Complete when M12 is Complete)

Checklist:

* [x] Safe Zone trigger volume exists
* [x] Heartbeat decreases while inside a Safe Zone
* [ ] Monster cannot enter Safe Zone (mythology-consistent threshold — carved symbol, rune, etc.)
* [x] No time limit — player leaves when breathing calms and monster sounds distant (requires M12, Timer is placeholder)
* [x] Two-stage experience works: position hidden on entry, breathing must calm before safe to move (Breathing works, Monster behavior verified in M12)

Definition of Done:

* Player understands entering hides their position but elevated breathing remains detectable
* Player learns to judge when to leave by audio, not a timer
* Monster consistently stops at the safe zone threshold
* See systems/safe_zones.md

---

### Milestone 10 – Mythology Clue System

Checklist:

* [ ] Clue objects exist and are discoverable in the level environment
* [ ] Clues are environmental only — no UI log, no hint system
* [ ] Monster weakness knowledge must be observed, remembered, and applied by the player
* [ ] At least one clue meaningfully changes player behaviour during Escape Phase

Definition of Done: see systems/clue_system.md

---

### Milestone 11 – Pause Menu, Sneak Mode & HUD

Checklist:

**Pause Menu:**
* [ ] Escape key opens pause menu during Playing state
* [ ] Time.timeScale = 0 while paused
* [ ] Pause menu contains: Resume, Restart Level, Quit to Main Menu, Settings
* [ ] Settings panel contains: master volume slider, mouse sensitivity slider, key mapping (rebindable; defaults shown)
* [ ] Cursor unlocked and visible in pause menu
* [ ] Cursor re-locked and hidden on Resume
* [ ] Restart from pause menu works identically to Restart from end screen

**Sneak Mode:**
* [ ] Hold Left Ctrl activates breath-hold; deep inhale audio cue plays on activation
* [ ] Sneak speed: ~40–50% of walk speed
* [ ] Sneak noise level: 0.1 — below Alert threshold (0.25); movement while sneaking does not escalate state
* [ ] Breath-hold timer: 8–12 seconds maximum; no UI countdown
* [ ] Voluntary release (release Ctrl before timer expires): no noise burst
* [ ] Forced exhale (timer expires): small AddNoiseBurst
* [ ] Ctrl held inside a safe zone has no effect on breathing decay — not a usable exploit

**Hunted Mode:**
* [ ] HeartbeatManager: second parameter set implemented (slower noise decay, sprint alone reaches Panic)
* [ ] Hunted Mode activates permanently on escape trigger (Major Treasure collected)
* [ ] Mythology-specific monster vocalization fires as a one-shot on escape trigger (Minotaur roar, serpent hiss, etc.) — the external signal that the hunt has started; Panic audio transition happens automatically alongside it

**HUD:**
* [ ] Treasure score: running numeric total always visible (0 → 100 → 200 as treasure is collected — never fraction format)
* [ ] Movement mode icons: three small icons always visible, active icon highlighted — Sneak (breath icon), Walk (footprints), Sprint (running figure)
* [ ] Heartbeat state: audio only — no visual indicator of any kind
* [ ] Collectible slots: deferred to M13

Definition of Done:

* All pause menu options work correctly
* Game state is fully preserved on Resume (no dropped events, no timer drift)
* Settings values take effect immediately, including key remapping
* Sneak mode: noise level correct; timer fires; forced exhale triggers burst; voluntary release does not; deep inhale / held silence / exhale audio cues all play correctly
* Hunted Mode: parameters switch on escape trigger; monster vocalization fires once; player can hear/feel the difference
* HUD: score updates live on each treasure pickup; movement icon switches correctly on each mode change; no heartbeat visual anywhere
* See docs/game_flow.md and systems/heartbeat_system.md

---

### Milestone 12 – Monster AI: Wandering & Detection

Checklist:

* [ ] Monster present in the level from the start (passive wandering during Exploration)
* [ ] Monster moves toward the most recently heard noise source
* [ ] Monster wanders semi-randomly when no noise detected for X seconds
* [ ] Monster cannot enter safe zones (threshold respected)
* [ ] Monster transitions to active hunt on OnEscapeTriggered
* [ ] Monster discovery triggers Hunted Mode in HeartbeatManager (same parameter switch as escape trigger)
* [ ] Monster extinguishes or dims player's carried light on escape trigger
* [ ] Active hunt speed is slightly faster than player sprint

Definition of Done:

* Monster is a believable threat from first step — reckless players feel it
* Monster consistently loses the scent after sustained player silence
* Monster never enters a safe zone
* See systems/monster_system.md

---

## Vertical Slice Milestones (Do Not Start Before Expansion Complete)

### Milestone 13 – Sidequest: Collectibles, Altar, Hidden Room

Checklist:

* [ ] 3 IInteractable sidequest collectibles placed in off-path areas
* [ ] Additional non-sidequest interactable objects placed alongside them — player must judge which items belong to the ritual
* [ ] Collectible HUD: 3 inventory slots visible during play; fills as sidequest items are collected (no label, no text)
* [ ] IInteractable altar exists and accepts offerings
* [ ] Altar triggers when all 3 items have been offered
* [ ] Hidden room opens after offering is accepted
* [ ] Hidden room is dark on entry
* [ ] Torches in hidden room ignite automatically after offering (not on player entry)
* [ ] Monster weakness knowledge displayed environmentally inside hidden room
* [ ] Meta-quest lore fragment present in hidden room
* [ ] Hidden room functions as a safe zone (monster threshold at entrance)

Definition of Done: see systems/clue_system.md

---

### Milestone 14 – Phobia Pool

Checklist:

* [ ] PhobiaType enum exists
* [ ] Each jumpscare event tagged with one or more PhobiaType values
* [ ] Per-run phobia weights randomly assigned at level load
* [ ] Event selection prefers higher-weighted types for this run
* [ ] Minimum 3 phobia types implemented: Nyctophobia, Scopophobia, Acousticophobia
* [ ] Jumpscare cooldown respected globally across all phobia types

Definition of Done:

* Two runs of the same level feel meaningfully different in their scare profile
* No two jumpscares fire within the cooldown window
* See docs/mechanics.md — Jumpscare / Phobia Pool

---

### Milestone 15 – Player Hands / Interaction Polish

Checklist:

* [ ] Two first-person hands visible at bottom of screen
* [ ] Left hand holds and displays the active light source (candle/lantern)
* [ ] Right hand animates toward interactable objects on interact
* [ ] Interactable objects show subtle visual feedback when in range (no UI text)
* [ ] (Optional) Magnifying glass tool for examining inscriptions
* [ ] (Optional) Brush/trowel for revealing partially buried items

Definition of Done:

* Interaction feels physical and grounded in the era
* No floating "Press E" text — world gives visual feedback instead
* See docs/interaction.md

---

### Milestone 16 – High Score / Local Leaderboard

Checklist:

* [ ] Top 10 scores stored locally via PlayerPrefs per level
* [ ] High Score Screen shown between end panel and Play Again button
* [ ] Current run's score highlighted if it entered the top 10
* [ ] "Play Again" button reloads the scene cleanly

Definition of Done:

* Scores persist across sessions
* High Score Screen appears correctly after both win and loss
* See docs/game_flow.md

---

### Milestone 17 – Level Design Pass

Checklist:

* [ ] Level has 12–18 distinct areas (rooms + corridors + dead ends)
* [ ] 30–40% of areas are dead ends
* [ ] 5–7 Minor Treasures placed in off-path rooms, none on main path
* [ ] 3 sidequest collectibles placed in different zones of the level
* [ ] Altar placed mid-level, accessible from multiple routes
* [ ] Hidden room adjacent to or triggered by altar
* [ ] 2–3 safe zones placed off main path, none near Major Treasure candidates
* [ ] 3–5 Major Treasure candidate rooms at high graph distance from entrance
* [ ] Exit = Entrance (same door used to enter and escape)
* [ ] Light sources distributed along exploration routes
* [ ] Level completes in 15–25 minutes on first thorough playthrough

Definition of Done:

* A new player can complete the level without instruction
* All systems (sidequest, safe zones, Monster AI, heartbeat, audio) interact correctly in the level
* Playtesting confirms the tension arc: calm exploration → escalating dread → escape panic
* See docs/level_design.md

---

## Core Systems

MVP:

* PlayerController
* InteractionSystem
* TreasureSystem
* EscapeSystem (basic — timer, exit trigger, win/lose events)
* GameManager
* UIManager
* AudioManager (basic)

Expansion adds:

* HeartbeatSystem (state machine, noise/breathing drivers)
* MonsterSystem (noise-based AI — wandering, detection, active hunt)
* SafeZoneSystem (trigger volumes, threshold blocking)
* PauseMenu
* EscapeSystem (full — monster AI integration, light extinguish on trigger)
* AudioManager (adaptive — mixer snapshots, music arc, layered ambience)

Vertical Slice adds:

* SidequestSystem (collectibles, altar, hidden room trigger)
* PhobiaPool (tagged events, per-run weights)
* PlayerHands (first-person hand rendering, interaction animation)
* HighScoreSystem (local PlayerPrefs, top 10 per level)

Future Vision adds:

* HubSystem (Explorer's Club, expedition selection)
* CurioCabinet
* NPC
* MetaQuestSystem

---

## Development Principle

Implementation tasks use checkboxes.

Subjective quality, feel, balance, atmosphere, and polish are evaluated through Definition of Done sections and playtesting rather than checklists.
