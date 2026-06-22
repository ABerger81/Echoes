# World

## Purpose

Defines the era, rules, and persistent spaces of the game world. All design decisions — items, knowledge, technology, aesthetics — must be consistent with this document.

---

# Era

**Victorian and Edwardian periods: approximately 1880–1914.**

The golden age of geographical and archaeological discovery. Explorer societies, secret orders, and scientific institutions operated side by side — and often overlapped. The world still had blank spaces on the map. Ancient myths were being taken seriously as potential historical records. Secret societies (Freemasonry, the Hermetic Order of the Golden Dawn, Rosicrucianism, Theosophy) were at their peak influence.

**What exists in this era:**
- Candles, oil lamps, torches, lanterns — no electric light on expeditions
- Hand-drawn maps, expedition journals, field notes, telegrams
- Steam travel, early photography
- Service revolvers, hunting rifles — present as period detail only; this game has no combat and the player carries no weapons
- Expedition clothing: field jackets, pith helmets, leather satchels, pocket watches

**What does not exist:**
- Electric torches (flashlights were not standard expedition equipment in this period)
- Synthetic materials, plastic
- Modern UI language — no "achievement unlocked", nothing that breaks the era

---

# The Explorers Club (Hub)

**Game dev term: Hub** — the persistent central space the player returns to between expeditions.

On its surface: a respectable institution in the tradition of the Royal Geographical Society or The Explorers Club (New York, 1904). Reading rooms, trophy walls, expedition maps, learned society members. In practice: a front for something much older.

## Public Layer — visible from the start

| Element | Function |
|---|---|
| World map / globe | Expedition selection — choose which mythology to explore |
| Curio cabinet | Glass-fronted display cabinet showing artifacts collected from completed levels. Undiscovered items shown as question marks. Motivates completionism. The collection grows across expeditions. |
| NPC (senior member, archivist, or cartographer) | Briefs the next expedition, delivers story, tracks discoveries. Becomes more candid as the player returns with more lore. Knows more than they initially disclose. |
| Journal / board | The player's assembled field notes. Sidequest lore fragments from completed levels appear here, gradually building the meta-picture. |

## Hidden Layer — unlocked gradually through meta-quest lore

A restricted area of the club — not visible or accessible at the start. As the player assembles lore fragments from sidequests across multiple levels, the hidden layer opens incrementally: a cipher board, connections between artifacts that should not be connected, the order's true history revealed piece by piece.

The club reveals itself to be what it always was.

**Scope:** Public layer = Vertical Slice. Hidden layer and full secret order revelation = Future Vision.

---

# The Secret Order

The Explorers Club is a front for a secret order with a history stretching back centuries — possibly to the Crusades, possibly further. The order has been aware of the mythological locations long before the Victorian expeditions began. Their reasons for sending the player are not fully disclosed at the start.

The player's relationship to the order is a narrative decision for the full game:
- A new recruit being evaluated through the expeditions
- Someone who stumbled into the order's orbit unknowingly
- Someone who has always been a member — the expeditions were the initiation all along

All three are consistent with current design. The choice shapes the ending.

See docs/lore.md — The Meta-Quest for the full narrative framework.

---

# Light Sources (Era-Appropriate)

The player reads light level from the scene. No UI light meter.

| Source | Properties | Context |
|---|---|---|
| Candle | Weak radius, barely pushes back darkness | Player starting light — carried throughout the level |
| Oil lamp | Wider radius, steadier light | Environmental object in levels; possible found item in certain mythologies |
| Torch | Strong, wide radius | Hidden room set dressing — ignites after sidequest offering is accepted. Not carried by the player. |
| Lantern | Covered flame, portable, wider than candle | Potential level-specific variant for certain mythologies or conditions |

The candle-to-lit-room contrast in the sidequest hidden chamber is intentional: the flickering candle the player carries versus the fully illuminated space that responded to the ritual signals that what is inside is significant.

---

# Traps

Environmental hazards tied to each mythology's construction logic — Daedalus mechanisms in the Greek labyrinth, counterweight systems in Egyptian tombs, rune-activated wards in Norse ruins.

**Design rule:** Traps never deal damage or kill the player. This game has no HP. All trap consequences feed into the noise and heartbeat system:
- A triggered mechanism makes loud noise, alerting the monster
- A closing corridor removes exits, forcing the player into a smaller and more dangerous space
- Rising water forces faster and therefore louder movement

The threat is never the trap itself — it is what the trap does to noise level and available options.

Sidequest knowledge may reveal trap locations or disarm mechanisms, giving clue-finders a meaningful advantage during exploration.

**Scope:** Expansion / Vertical Slice.
