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

# Implementation (M9)

**Scripts:**
- `Assets/_Game/Scripts/Environment/SafeZone.cs` — trigger volume; calls `HeartbeatManager.EnterSafeZone()` / `ExitSafeZone()` on player enter/exit

**HeartbeatManager additions:**
- `public static bool IsInSafeZone` — read by monster system (M12) to stop pursuit at the threshold
- `public void EnterSafeZone()` — sets flag; blocks `SetContinuousNoise()` so movement no longer raises noise pressure
- `public void ExitSafeZone()` — clears flag; movement noise resumes immediately

**How noise suppression works:** `SetContinuousNoise()` early-returns when `IsInSafeZone` is true. Existing `_noisePressure` continues to decay via `noiseDecayRate` in `Update()` — no extra code. The player's breathing calms naturally as state steps down through Fear → Alert → Calm.

**Monster threshold (M12):** `IsInSafeZone` is the hook. Monster AI reads this flag and stops pursuit when true. Not implemented until M12.

**Scene:**
- `SafeZone_01` — empty GameObject with Box Collider (Is Trigger, ~1.5 × 2 × 1.5), placed in a dead end off the main path

**Visual children on SafeZone_01 (added after M9 trigger was verified):**
- `MarkerDecal` — Quad (X rot 90°, scale 1.2×1.2, Y = 0.01) with `SafeZoneMarker` material (URP/Unlit, Transparent, Base Map = `Decal_SafeZone_Lyre`)
- `MarkerLight` — Point Light, amber `#C8A45A`, Range 2.5, Intensity 0.8, Realtime, No Shadows. Requires enclosed geometry to be visible — placeholder until M17 adds walls.
- `MarkerParticles` — Particle System, Rate 3/s, Disk shape R=0.5, slow upward drift, white fade to transparent over lifetime
- AudioSource on SafeZone_01 root — `SFX_sacredAudio`, Loop, Play On Awake, Spatial Blend 1, Linear rolloff, Min 1m / Max 5m. 3D proximity fade handled automatically by Unity distance rolloff — no script needed.

**Verified in M9:**
- Trigger fires correctly on enter and exit
- Movement noise suppressed while inside; state decays to Calm with no player input
- State on exit resumes from current pressure, not reset to zero
- Minor treasure burst (`AddNoiseBurst`) still fires inside — design intent: don't collect treasure while hiding
- `IsInSafeZone` resets cleanly on Play/Stop (Awake + OnDestroy reset)

**Not yet verified (requires M12):**
- Monster stops at the threshold
- Player learns "position hidden" through monster behaviour

---

# Related Systems

- systems/heartbeat_system.md — Breathing Down (two-stage hiding experience)
- systems/audio_system.md — breathing loops are the primary audio feedback for the two-stage hiding experience
- systems/monster_system.md — monster patrol and why it does not re-check cleared safe zones
- systems/clue_system.md — sidequest hidden room as safe zone
- docs/mechanics.md — Safe Zone / Hiding (player-facing mechanic description)
- docs/level_design.md — placement rules
