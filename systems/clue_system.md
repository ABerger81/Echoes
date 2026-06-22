# Clue System

## Purpose

Teach players how to survive. Reward attention and thorough exploration. Create replayability through knowledge layers — both within a level and across the whole game.

---

# Design Philosophy

Clues are earned, not given.

**Environmental only — no UI prompts.** No "hint acquired" notification. No text overlay explaining what the player just learned. The player finds, interprets, and acts. A player who misses every clue can still complete the level — they are simply less prepared and must rely on instinct during the escape phase.

The knowledge gap between a clue-finder and a clue-misser is the core skill dimension of the game. Experienced players read the room. Inexperienced players get surprised.

---

# Sidequest Structure

Clues are locked behind a three-stage optional sidequest. Completing it is never required — always rewarding.

## Stage 1 — Find 3 collectibles

Three IInteractable objects hidden across the level — in dark corners, off-path areas, places that require the player to explore rather than rush. Each pickup triggers a subtle heartbeat tick and a faint atmospheric sound. By the third, the player understands: someone was here before them.

**Number 3 is mythologically resonant across every planned mythology:**
- Greek: three Fates, three Furies, three-headed Cerberus
- Norse: three roots of Yggdrasil, three Norns
- Egyptian: the Osirian triad
- Japanese: the Three Sacred Treasures

Players may never consciously register this. It feels correct.

**Content varies per mythology — system is identical:**
- Greek / Minotaur: bone fragments of previous tribute-bearers
- Norse: carved rune stones
- Egyptian: canopic jar fragments
- Japanese: inscribed ofuda (paper talismans)

## Stage 2 — Offer at the altar

One IInteractable altar per level. Placing all three items triggers a ritual acceptance sound — loud enough that the monster may hear it. Heartbeat jumps one state.

The moment of completion is simultaneously a moment of danger. Success and exposure arrive together.

## Stage 3 — Hidden area opens

A section of the level that was always present but inaccessible — a wall that looked solid, a door with no visible mechanism. After the offering it opens. The space responds to the ritual.

This breaks the player's mental map: if one wall can move, can others?

**The hidden room is dark on entry.** Torches (or the mythology-appropriate light source) ignite automatically after the offering is accepted — the room responds to the ritual, not to the player's arrival. The player's candle is not what lit it.

---

# The Reward — Monster Weakness Knowledge

Not an item. Not a stat. Behavioral knowledge: weakness facts about the monster, carved into the walls or written by a previous explorer who did not survive to use what they found.

**Examples for Minotaur:**
- The Minotaur circles its last heard position before moving on — staying still after it passes is safer than running
- The Minotaur hesitates at the threshold of lit areas
- The Minotaur's hearing is directional — sounds that echo confuse it

A player who read the room plays the escape phase differently. A player who skipped the sidequest relies on instinct. Both experiences are valid. Neither is blocked.

---

# Lore Layer — The Meta-Quest

Each hidden room contains more than survival knowledge. It contains a fragment of intertwined lore — a piece of a larger mystery connecting across mythologies. A single fragment is suggestive. Three or four fragments from different levels begin to reveal a pattern. See docs/lore.md — The Meta-Quest.

The fragment is always written in a form consistent with the Victorian/Edwardian era: a previous explorer's field notes, a carved inscription, a symbol sequence. Never a UI display. The player who finds all fragments across all levels has assembled the meta-picture — and earned initiation.

---

# Candle and the Hidden Room

The player starts each level with a weak light source (candle) — consistent with the Victorian/Edwardian era. No flashlights. The candle barely pushes back darkness.

The hidden room's torches ignite after the offering. They are set dressing — not pickups, not carried items. Their purpose is to illuminate the knowledge on the walls. The player's candle remains their only carried light.

The contrast — a single flickering candle versus a fully lit chamber — signals that what is inside matters.

---

# System Design (Mythology-Agnostic)

The system is identical every level. The content changes. The code does not.

```
3x IInteractable collectibles
  → IInteractable altar (offering accepted, monster alerted)
    → hidden room trigger (wall/door opens)
      → room dark on entry, lights up from ritual
        → lore object (weakness knowledge + meta-quest fragment)
```

Build the system once. Configure content per mythology. Every future level is supported without modifying the underlying implementation.

---

# Open Questions

- ~~How explicit is the connection between a learned clue and its in-game payoff?~~ — **Resolved:** Environmental only, no UI prompt. Player interprets and acts. The game never confirms they understood correctly.
- ~~Should clues be mythology-specific only, or can some clue types generalise?~~ — **Resolved:** Content is mythology-specific. Meta-lore fragments create transferable cross-level knowledge — players who assembled all fragments are more prepared for what comes next. The clue *type* (sidequest structure) is transferable; the *content* is always specific.
- ~~What happens if a player misses a clue entirely?~~ — **Resolved:** Level is still completable. Missing all clues means relying on instinct during escape. Intentional and acceptable — clues are a reward for attention, not a prerequisite for progress.
