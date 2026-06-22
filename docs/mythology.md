# Mythology Reference

## Purpose

This document stores the per-mythology content for each planned level — monster, theme, sidequest collectibles, safe zone threshold, hidden room knowledge, and audio profile. System design (how these elements work) lives in the relevant systems docs. This document defines the *content* that populates those systems.

Not all mythologies listed here are guaranteed to be implemented. Greek (Minotaur) is the Vertical Slice target.

---

## Era Context

All levels are experienced through the eyes of a Victorian or Edwardian era explorer (1880–1914). Each location was discovered by the secret order behind the Explorers Club. Previous expeditions have been sent — not all returned. Evidence of earlier visitors (scratched marks, abandoned kit, old candle stubs) is part of every level's environmental storytelling. See docs/world.md and docs/lore.md.

---

# Greek — Minotaur Labyrinth

## Monster

Minotaur

## Theme

Labyrinth, isolation, being hunted, the weight of ancient tribute

## Location Type

Stone labyrinth — narrow corridors, carved walls, ritual chambers, evidence of sacrificial tribute

## Sidequest Collectibles

Bone fragments of previous tribute-bearers — scattered in dead ends where earlier victims were abandoned. Finding all three suggests the Minotaur has been here a very long time, and so have the victims.

## Safe Zone Threshold

A carved Athena or Apollo symbol above the entrance to a ritual alcove. The Minotaur, bound by the labyrinth's ancient covenant, cannot violate a space dedicated to the Olympians.

## Hidden Room — Monster Weakness Knowledge

The hidden room contains a previous explorer's field notes, carved or scratched into the stone:

- The Minotaur circles its last heard position before moving on — staying still after it passes is safer than running
- The Minotaur hesitates at the threshold of lit areas — it prefers darkness
- The Minotaur's hearing is directional — sounds that echo off stone walls confuse it; the labyrinth itself can be used against it

## Environmental Clues (non-sidequest)

- Sun symbols carved at junctions — a previous explorer's attempt to mark directions (not reliable; the labyrinth shifts)
- Broken horn markings on low walls — evidence of the Minotaur's physical presence in this corridor
- Blood marks on the floor — trail of a previous explorer, not monster tracking. Following them shows where someone went. Whether they survived is unknown.

Note: The monster is sound-based only — no sight, no smell, no blood tracking. Blood marks are environmental storytelling about past victims, not monster behaviour.

## Audio Profile

- Calm: stone corridor wind, distant echo
- Alert: faint hoofbeat stomps added
- Fear: bull breathing and snorting added
- Panic: roar and heavy stomps dominate

See docs/audio_design.md — Per-Mythology Ambient Layering.

---

# Norse — Serpent Temple

## Monster

Jörmungandr

## Theme

Ancient serpent, poison, confinement, the end of things

## Location Type

Stone temple built around a subterranean rift — dripping water, low ceilings, carved serpent motifs, rune-covered columns

## Sidequest Collectibles

Carved rune stones — small tablets inscribed with bindrunes by scholars who came before. Each stone carries a fragment of knowledge that only makes sense when all three are assembled at the altar.

## Safe Zone Threshold

Isa or Algiz rune carved into the stone threshold. The world serpent's instinct does not cross rune-warded ground — an ancient ward placed by those who built this temple precisely to contain it.

## Hidden Room — Monster Weakness Knowledge

Ancient runic text translated (partially) by a previous expedition member:

- Jörmungandr detects vibration as much as sound — walking slowly on stone produces less detectable ground vibration than running
- The serpent pauses when it detects competing sounds from multiple directions — the temple's natural dripping and echo can mask movement
- Jörmungandr cannot coil in tight spaces — narrow corridors offer momentary protection if the player can reach one before it closes distance

## Environmental Clues (non-sidequest)

- Snake carvings at floor level — worn smooth, suggesting the serpent has moved through this corridor many times
- Venom pools — areas of discoloured stone where something corrosive has been present. Avoid: the serpent has rested here
- Rune stones on walls (not the sidequest collectibles) — partial translations hinting at the serpent's nature

## Audio Profile

- Calm: dripping water, low cavern hum
- Alert: distant hiss added
- Fear: scale-dragging sound added
- Panic: venom-hiss and bone-rattle dominate

See docs/audio_design.md — Per-Mythology Ambient Layering.

---

# Egyptian — The Tomb (Planned — Future Vision)

## Monster

TBD — Anubis guardian, animated ushabti, or a mummified sentinel

## Theme

Death, preservation, the afterlife, the cost of disturbing the dead

## Location Type

Multi-chamber tomb — antechambers, burial shafts, canopic shrines, false doors

## Sidequest Collectibles

Canopic jar fragments — broken pieces of the jars that once held organs of the buried dead. Reassembled at the offering altar, they complete a ritual the tomb's builders intended.

## Safe Zone Threshold

Ankh-and-scales carving at the entrance — a space under Anubis protection. The guardian creature, itself a servant of the death god, cannot violate a space already claimed by Anubis.

## Hidden Room — Monster Weakness Knowledge

TBD

## Environmental Clues (non-sidequest)

TBD — likely hieroglyphic inscriptions, canopic imagery, false door markings

## Audio Profile

TBD — likely: dry sand wind, distant chanting (Calm); stone grinding, muffled footsteps (Alert); bone percussion, deep resonant breathing (Fear/Panic)

---

# Japanese — The Shrine (Planned — Future Vision)

## Monster

TBD — Oni, a corrupted kami, or a yokai appropriate to confinement and pursuit

## Theme

Corruption of the sacred, spiritual pollution, things that should not be disturbed

## Location Type

Abandoned mountain shrine complex — torii gates, stone lanterns, overgrown paths, inner sanctum

## Sidequest Collectibles

Inscribed ofuda (paper talismans) — protection charms placed by priests to contain the creature. Degraded over time. Gathering them and placing them at the central altar restores something of the original ward.

## Safe Zone Threshold

Shimenawa (sacred rope) strung across the entrance — a purified boundary. Impure or corrupted spirits cannot cross the barrier of a ritually cleansed space.

## Hidden Room — Monster Weakness Knowledge

TBD

## Environmental Clues (non-sidequest)

TBD — likely broken torii, scattered ofuda, evidence of failed purification attempts

## Audio Profile

TBD — likely: wind through bamboo, distant temple bell (Calm); shuffling footsteps on wood, distant growl (Alert); heavy breathing, cracking wood (Fear/Panic)
