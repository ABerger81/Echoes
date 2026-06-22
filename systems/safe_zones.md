# Safe Zones

## Purpose

Provide binary protection from monster detection. A safe zone is the only space in the level where the player is fully hidden from the monster.

---

# Design

Safe zones offer complete, binary protection — not partial cover. The player is either inside one (hidden) or outside one (detectable). There is no partial safety gradient; that role belongs to the Light Exposure trade-off (see docs/mechanics.md).

**No time limit.** The pressure to leave comes entirely from the monster's patrol behaviour — it will eventually wander back to the area. The player must judge when to move based on audio, not on a countdown. See systems/heartbeat_system.md — Breathing Down.

---

# Two-Stage Hiding Experience

Entering a safe zone does not immediately make the player safe. If the player arrived with an elevated heartbeat, their breathing continues generating detectable noise.

1. **Enter** — physically hidden. The monster cannot detect the player by position.
2. **Wait for breathing to calm** — elevated breathing is detectable at close range even from inside. The player listens to their own breathing loop; when it quiets, they are no longer generating continuous noise.

Leaving too early — while still breathing heavily — means the monster detects the sound immediately at close range. The player must judge by audio when both conditions are true: breathing is calm AND the monster sounds distant enough to risk moving.

---

# Why the Monster Cannot Enter

Each safe zone has a threshold the monster does not cross. The explanation is mythology-consistent — the creature is bound by ancient rules that predate its existence. These thresholds are never labelled or explained to the player. They are discovered through observation: the monster always stops at the same point.

| Mythology | Threshold element | Explanation |
|---|---|---|
| Greek / Minotaur | Carved Athena or Apollo symbol above the entrance | The Minotaur, creature of the labyrinth, cannot violate a space sacred to the Olympians |
| Norse / Jörmungandr | Isa or Algiz rune carved into the stone threshold | The world serpent's instinct does not cross rune-warded ground |
| Egyptian | Ankh-and-scales carving at the entrance | A space under Anubis protection — the guardian cannot violate a death god's claim |
| Japanese | Shimenawa (sacred rope) strung across the entrance | Impure spirits cannot cross the boundary of a purified space |

---

# Physical Design Rules

- **Size:** Small — enough for one person to crouch in. Not comfortable; just survivable.
- **Location:** Off the main path, in dead ends or side corridors. Never near Major Treasure candidate rooms.
- **Count:** 2–3 per level.
- **Previous occupant:** A previous explorer may have left a candle stub, a personal item, or a scratched mark on the wall — evidence that someone survived long enough to find this spot.
- **Sound design:** Quieter acoustically inside. Not a UI cue — the player feels the difference, they are not told about it.
- **No UI label.** The player is never told "You are in a Safe Zone." They learn from experience that the monster stops outside.

---

# Sidequest Hidden Room

The hidden room unlocked by the sidequest (altar offering) is also a safe zone — the most sheltered space in the level, fully lit after the offering. Completing the sidequest gives:

1. Monster weakness knowledge (carved into the walls)
2. A meta-quest lore fragment
3. The safest, best-lit hiding spot in the level

Maximum reward for maximum exploration. See systems/clue_system.md.

---

# Milestone

Expansion — Milestone 9.

---

# Related Systems

- systems/heartbeat_system.md — Breathing Down (two-stage hiding experience)
- systems/monster_system.md — monster patrol and why it does not re-check cleared safe zones
- systems/clue_system.md — sidequest hidden room as safe zone
- docs/mechanics.md — Safe Zone / Hiding (player-facing mechanic description)
- docs/level_design.md — placement rules
