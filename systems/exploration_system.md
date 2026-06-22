# Exploration System

## Purpose

Support discovery and learning.

---

# Player Goals

- Navigate
- Observe
- Learn
- Find Treasure

---

# Core Principles

- Curiosity drives progression
- Information is reward
- Knowledge becomes skill

---

# Exploration Loop

Explore
↓
Observe
↓
Learn
↓
Discover Clue
↓
Continue Exploration

---

# Open Questions

- ~~Minimum number of clues/discoveries required before Major Treasure becomes reachable?~~ — **Resolved:** Soft lock only. No hard gate. Major Treasure placement at high graph distance from the entrance guarantees traversal of most of the level. The sidequest rewards exploration without requiring it. Hard locks break player agency — discovery must feel like initiative, not a checklist.

- ~~Is there a hint system for players who get stuck?~~ — **Resolved:** No hint system. "Stuck" is acceptable and intentional. The torch trail (player-built navigation aid), sidequest hidden room knowledge, and level design quality are the only aids. A hint system breaks immersion and undermines the core fantasy. Placement discoverability is a level design responsibility verified through playtesting, not a runtime system.

- ~~Do discoveries persist in a player-facing log, or remembered from memory?~~ — **Resolved:** Discoveries persist. Sidequest lore fragments appear in the hub journal (see docs/world.md — The Explorers Club). Monster weakness knowledge from hidden rooms is not logged — it is meant to be remembered and applied, consistent with the "knowledge becomes skill" principle. Players who forget must replay the sidequest.

---

# Stuck Player — Last Way Out

No special stuck-detection system. Two standard mechanisms cover every scenario:

**1. Pause menu Restart** — always accessible during play. Player can restart the level or quit to menu at any time. Standard game UX; planned for a later UI milestone.

**2. Exit always present** — the exit exists in the level before escape is triggered. A player who gives up can walk to it at any time:
- Before escape triggered: "Leave empty-handed? You will receive no score." → confirms → level ends, score 0.
- During escape: normal win condition applies.

No player is ever trapped. The only question is whether they find the Major Treasure.

---

# Major Treasure Placement — Reachability

At level load, each candidate room is checked for reachability from the entrance (NavMesh pathfinding or graph distance). Unreachable candidates are excluded from the pool automatically before placement rolls.

Discoverability — whether the player will actually find the candidate room — is a level design responsibility, not a runtime guarantee. Verified through playtesting at build time.
