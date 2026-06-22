# Monster System

## Purpose

Define the monster's behaviour across Exploration and Escape phases. The monster is the primary threat. Its design must be consistent with the core fantasy: the player cannot fight or outrun it — survival requires knowledge, composure, and noise discipline.

---

# Core Rules

- **No sight.** No vision cone, no line-of-sight checks. The monster cannot see the player at any distance.
- **Noise-based detection only.** The monster detects the player through sound — action noise (footsteps, collection, sprint) and breathing (heartbeat-driven continuous noise).
- **Present from level start.** The monster wanders the level passively during Exploration. The heartbeat and noise system matter from the first step.
- **Cannot enter safe zones.** A mythology-consistent threshold prevents entry. Once the monster has checked an area and found nothing, it moves on. See systems/safe_zones.md.

---

# Detection

The monster detects noise at a radius proportional to the noise level. Higher noise = larger detection radius.

**Detection inputs:**
- Player action noise — walk (low), collect (medium burst), sprint (high continuous). See systems/heartbeat_system.md — Noise Hierarchy.
- Player breathing — continuous, fades as heartbeat drops. Loudest at Panic (150 BPM), near-zero at Calm (60 BPM). Persists after the player stops moving until heartbeat calms.

**Detection output:** Monster moves toward the most recently detected noise position — the location it heard the sound, not the player's current position.

---

# Exploration Phase — Passive Wandering

During Exploration (before Major Treasure is collected), the monster wanders passively.

**Behaviour:**
- Moves through the level on a semi-random patrol weighted toward areas not recently visited
- Pauses briefly at intervals to "listen" — orients toward any recent noise
- If noise detected: moves toward the noise source. On arrival, if nothing is detected, resumes wandering.
- Does not actively pursue — it investigates, then moves on

**Player consequence:**
- Reckless noise (sprinting, rapid collecting) during Exploration may bring the monster close
- Proximity escalates heartbeat → jumpscares become more likely → Panic state possible → accidental escape trigger possible
- The monster being nearby is not immediately fatal; it becomes a threat if the player makes more noise

---

# Escape Phase — Active Hunt

Triggered by `GameManager.OnEscapeTriggered` (Major Treasure pickup OR Panic state accidental trigger).

**On trigger:**
- Monster's first act: extinguish or significantly dim the player's currently carried light. Scripted event attached to `OnEscapeTriggered`. See systems/escape_system.md.
- Monster transitions from passive wandering to active hunter.

**Chase behaviour:**
- Moves toward the last heard noise source continuously
- If no noise detected for several continuous seconds: loses the scent, resumes a wandering-while-hunting state (still alert, no fixed target)
- "Losing the scent" is not instant — requires sustained silence. Freezing for one second is not enough.

**Speed:**
- Slightly faster than the player's sprint speed during active chase
- Outrunning in a straight line is not viable — survival requires composure, safe zones, and knowledge from the clue system

---

# Losing the Monster

**Reliable method:** Enter a safe zone, wait for breathing to calm. The monster investigates the area, detects no sound, and moves on. It does not immediately re-check a cleared safe zone.

**Risky method:** Find a position where several continuous seconds of complete stillness and silence cause the monster to lose the scent. Any noise resets the clock. Breathing at Fear or Panic state can betray the player even while standing still.

---

# Per-Mythology Presentation

Detection behaviour and state-machine logic are identical across all mythologies. Only the content changes — appearance, sound, movement style. This keeps the system mythology-agnostic: one implementation, different assets per level.

See docs/audio_design.md (Per-Mythology Ambient Layering) and docs/mythology.md.

---

# Milestone

Expansion — Milestone 12. Requires Heartbeat System (M7) to exist first — monster detection feeds the heartbeat state machine, and the heartbeat state machine drives the monster's consequence on the player.

---

# Related Systems

- systems/heartbeat_system.md — noise hierarchy and breathing (detection inputs)
- systems/safe_zones.md — where the monster cannot enter
- systems/escape_system.md — escape phase trigger and event flow
- docs/mechanics.md — Escape/Chase (player-facing description)
- docs/audio_design.md — per-mythology monster audio
- docs/level_design.md — how level layout affects patrol behaviour
