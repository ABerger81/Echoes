# Interaction

## Purpose

Define how the player interacts with the world — from the MVP raycast implementation through the Vertical Slice hands design.

---

# MVP — Raycast + E Key

Centre-screen raycast. The player looks at an object; if it is within range and implements `IInteractable`, pressing E triggers the interaction.

**Implemented in:** Milestone 2. See `Assets/_Game/Scripts/Interaction/Interactor.cs`.

This is sufficient for MVP and Expansion. The raycast approach is invisible, reliable, and allows level design to place objects at any scale without requiring precise hand animation. Do not replace it before the Vertical Slice is otherwise feature-complete.

---

# Vertical Slice — Two Visible Hands

Two first-person hands visible at the bottom of the screen, consistent with the Victorian/Edwardian explorer identity.

## Left Hand — Light Source

Holds the candle, lantern, or era-appropriate flame. This hand IS the light source. The light moves with the hand, creating dynamic shadows as the player moves. More immersive than a head-mounted beam, and physically accurate to the era.

The player's candle is always in their left hand throughout the level. It is never set down, never swapped out, never extinguished by the player. At the moment escape is triggered, the monster extinguishes or dims it as its first act.

## Right Hand — Interaction Hand

Reaches toward interactable objects, picks up treasures, places offerings at the altar, turns mechanisms. All interactions that currently trigger on E key are visualised through this hand.

**No weapon in either hand — ever.** This game has no combat. The right hand is never a fist or a tool of force. See docs/vision.md.

---

# Optional Right-Hand Tools (Vertical Slice)

Tools enrich the feel of *being an explorer* rather than a first-person camera pressing a button. Not required for MVP or Expansion.

| Tool | Use | Era accuracy |
|---|---|---|
| Magnifying glass | Examine inscriptions and clues — slight zoom, helps read carved text | Victorian / Edwardian — detective and naturalist standard kit |
| Small brush or trowel | Reveal items partially buried in dust or debris — makes discovery a physical act | Edwardian archaeology field kit |

One tool at a time in the right hand. The player does not carry both simultaneously. Context determines which is active (approaching a carved inscription vs. approaching a partially buried artifact).

---

# Interactable Object Feedback

No floating "Press E" UI text. The object itself signals it is interactable.

When the player looks at an interactable object from within interaction range, the object responds with a subtle visual effect — a shimmer, an edge glow, a slight brightening. The effect is diegetic in feel: like the object catching the candlelight differently. Not a UI element overlaid on the screen.

The player learns what is interactable through the world's response, not through labels. This is consistent with the "environmental only, no UI prompts" principle across the rest of the design. See systems/clue_system.md.

---

# Era Rule

The left-hand light source is always diegetic and era-appropriate:
- Candle (default starting light)
- Oil lantern (found in level or context-specific)
- Era-specific flame appropriate to the mythology (a burning relic fragment, a ritual torch stub)

No flashlight. No electric torch. See docs/world.md (Light Sources) for the full era constraint list.

---

# Milestone

- MVP raycast + E key: Milestone 2 (complete)
- Two hands + optional tools: Milestone 15 (Vertical Slice)

---

# Related Systems

- docs/mechanics.md — Interaction (player-facing mechanic description)
- docs/world.md — Light Sources (era constraints on what the left hand can hold)
- systems/clue_system.md — sidequest collectibles and altar interaction
- docs/level_design.md — how interactable objects are placed in the level
