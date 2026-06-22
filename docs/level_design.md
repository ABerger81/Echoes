# Level Design Guidelines

## Purpose

Define the structural rules for building a level. All mythology levels follow these guidelines. Content changes per mythology — structure does not.

---

# Time Target

**15–25 minutes** for a first-time player doing thorough exploration.

A player rushing the main path reaches the Major Treasure faster — but misses Minor Treasures, sidequest knowledge, and safe zone locations. Thorough exploration is rewarded with score, knowledge, and safety. The time target is for an attentive explorer, not a speedrunner.

---

# Room Structure

| Element | Target | Placement rule |
|---|---|---|
| Total distinct areas | 12–18 | Rooms + corridors + dead ends |
| Dead ends | 30–40% of all areas | Where Minor Treasures and sidequest collectibles hide |
| Treasure-bearing rooms | ~1 per Minor Treasure (5–7 total) | Never on the main path |
| Safe zones | 2–3 | Off main path, never near Major Treasure candidates |
| Sidequest collectibles | 3 | Scattered in dead ends across different zones |
| Altar | 1 | Mid-level, accessible from multiple routes |
| Hidden room | 1 | Triggered by altar; dark on entry, lights after offering |
| Major Treasure candidate rooms | 3–5 | High graph distance from entrance; one selected per run |

A player following only the main path should encounter 1–2 Minor Treasures at most. A player who investigates every dead end finds 5–7. The geometry enforces the exploration-vs-rush decision without instructions or markers.

---

# Exit = Entrance

The exit is the same door the player entered through. The escape phase is navigating *back* across familiar territory under active threat — not finding a new exit.

**Why this works:**
- The level does not need to be twice as large
- The player already knows the route — the tension comes from the monster, not disorientation
- The torch trail lit on the way *in* guides the way *out* (now dimmer, some possibly guttered)
- Narratively: the adventurer entered, completed their mission, escapes back through the same door

The Major Treasure must be placed at high graph distance from the entrance to ensure the player traverses most of the level before triggering escape — and therefore has most of the journey to retrace.

---

# Major Treasure Placement

- One Major Treasure per level
- Placed in a candidate room selected from a pool at level load — re-rolled every run
- Candidate rooms pre-placed by the designer at high graph distance from the entrance
- **Reachability (runtime):** At level load, each candidate room is checked for NavMesh reachability from the entrance. Unreachable candidates are excluded before the placement roll.
- **Discoverability (build time):** Whether a player will actually find the candidate room is a designer responsibility — verified through playtesting, not code. Every candidate room must feel like a genuine discovery, not a dead end that players ignore.

---

# Minor Treasure Placement

- 5–7 per level
- One per distinct off-path room or dead end
- Never on the main path between entrance and typical Major Treasure candidate locations
- Varied placement — some at eye level and relatively visible, some requiring exploration of corners, floor level, or above-head niches
- Jumpscare scaling is tied to collection count per run — see docs/mechanics.md (Treasure Collection)

---

# Sidequest Placement

- 3 collectibles per level — scattered in dead ends across different zones of the level
- Player must explore broadly to find all three, not just one zone thoroughly
- Altar — mid-level placement accessible from multiple routes after all three items are found
- Hidden room — connected to the altar via trigger. Always dark on entry. Torches ignite automatically after the offering is accepted (not when the player enters).

---

# Safe Zone Placement

- 2–3 per level (the hidden room counts as the primary one)
- Always off the main path — dead ends or side corridors
- Never near Major Treasure candidate rooms — players who grab the Major Treasure and immediately hide should have to reach a safe zone under active threat, not find one adjacent
- Mythology-specific threshold markers at each entrance (carved symbol, rune, sacred rope, etc.)
- See systems/safe_zones.md for full design and monster interaction rules

---

# Light Placement

- Starting light source (candle stub or equivalent) near the entrance — player picks it up early, before encountering darkness
- Fixed light objects (wall torches, sconces, candle clusters) distributed along exploration routes — lighting the way for thorough explorers
- Lit objects decay over time but never fully extinguish — they fade to a minimum glow floor
- Decay is visually noticeable during calm exploration so players understand lights fade before it matters during escape
- The hidden room's torches ignite automatically after the altar offering — set dressing, not pickups

No area should be at literal zero visibility. The minimum glow floor, plus ambient level-appropriate light (cracks in stone, phosphorescent growth, distant embers), guarantees a visible floor. Darkness is a resource to manage, not a hard block.

---

# Lighting Philosophy and Monster Interaction

The Light Exposure system (see docs/mechanics.md) creates a continuous trade-off:
- Standing in light: calmer heartbeat, easier navigation, higher monster detection range
- Standing in darkness: elevated heartbeat over time, harder navigation, lower monster detection range

Level design should provide a mix — never forcing the player to choose one exclusively. Dead ends where Minor Treasures hide should be darker than main corridors. Safe zones should have at least a candle's worth of ambient light (a previous occupant's remnant).

---

# Related Documents

- docs/mechanics.md — Treasure Collection, Light/Torch Trail, Safe Zone/Hiding, Light Exposure
- systems/clue_system.md — sidequest structure, altar, hidden room
- systems/safe_zones.md — safe zone design and mythology thresholds
- systems/monster_system.md — how monster patrol interacts with level layout
- docs/world.md — era constraints affecting materials, set dressing, and light sources
