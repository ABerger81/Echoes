**Version 1 Goal**

A tiny playable prototype where:
- player walks in first person
- mines blocks
- collects gold
- dies when uncovering monster
- score is displayed

NO multiplayer
NO crafting
NO enemy AI
NO quests
NO advanced graphics
NO procedural caves
NO inventory system

**Technical Goals**

1. Scene Navigation
- [ ] move in editor
- [ ] create objects
- [ ] organize hierarchy

2. First Person Controller
Learn:
- [ ] WASD movement
- [ ] mouse look

3. Raycasting
Critical for mining - pickaxe mechanic
what am I looking at?:
- [ ] center-screen ray
- [ ] detect wall
- [ ] click to mine
That unlocks:
- mining
- interaction
- detection

4. Prefabs
- [ ] blocks

5. Basic UI
- [ ] score text
- [ ] game over text

Build Vertical Slice Prototype
- [ ] one tiny playable loop
    NOT:
    - many disconnected systems

First prototype:
One small room

Inside:
- walls
- hidden gold
- hidden monster

Player:
- mines walls
- gains score
- dies if monster uncovered

**Milestone 1**
- [ ] Player can walk

**Milestone 2**
- [ ] Player can destroy a wall

**Milestone 3**
- [ ] Destroyed wall reveals:
    - [ ] gold
    OR
    - [ ] monster

**Milestone 4**
- [ ] Game over screen works

**Milestone 5**
- [ ] Audio tension system

