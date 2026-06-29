# Asset List — Echoes

## Imported Packages (root Assets/ folder)

| Package | Content | Primary use |
|---|---|---|
| `Caves and Dungeons/` | 11 dark atmospheric tracks — each has full + Loop A + Loop B | Music arc (Alert → Panic layers), labyrinth ambient |
| `Nature - Essentials/` | Fire, wind, water, rain, night, cicadas — all looping WAV | Torch fire loops, cave drip, ambient wind |
| `Voices - Essentials/` | Male + Female: breathing loops, gasps, sighs, shocked breaths, single breaths, effort, pain | Sneak SFX, breathing loops (already partially wired) |
| `TextMesh Pro/` | Unity UI text rendering | Already in use |
| `Settings/` | URP render pipeline config | Already in use |

---

## What Currently Exists (all locations)

| Asset | File | Location |
|---|---|---|
| Labyrinth calm ambience loops (×2) | The_Labyrinth_Loop A.wav, Loop B.wav | `_Game/Audio/Ambience/Labyrinth/` |
| Cave ambient (×2) | Ambiance_Cave_Deep_Loop, Cave_Drips_Loop | `_Game/Audio/Ambience/Labyrinth/` |
| Breathing loops (×3) | Normal, Moderate, Strong | `_Game/Audio/Breathing/` |
| Stray SFX (×4) | CreakDoor, HighKeyPiano, LockedDoor, MetalBang | `_Game/Audio/SFX/` |
| AudioMixer | EchoesAudioMixer.mixer | `_Game/Audio/` |
| Dark atmospheric tracks (×11) | An Unwelcome Presence, Ash and Dust, Call of the Depths, Chasm of the Serpent, Decrepit Cathedral, Ethereal Rites, Forbidden Temple, Halls of the Damned, Kothar's Iron Tomb, The Labyrinth, The Void | `Caves and Dungeons/` |
| Fire ambient loops (×3) | Fire_Big_Loop, Firecamp_Medium_Loop, Firecamp_Small_Loop | `Nature - Essentials/` |
| Wind loops (×2) | Wind_Calm_Loop, Wind_Forest_Loop | `Nature - Essentials/` |
| Water ambient loops (×7) | River, Stream, Waterfall ×2, Sea, Rain ×2 | `Nature - Essentials/` |
| Night ambient | Ambiance_Night_Loop | `Nature - Essentials/` |
| Male breath loops (×2) | Breath_Frozen_Loop, Breath_Mouth_Moderate_Sequence | `Voices - Essentials/Voice_Male/Voice_Male_Breath/` |
| Male single breaths (×13) | Breath_Single_Mono_01–13 | `Voices - Essentials/Voice_Male/Voice_Male_Breath/` |
| Male breath gasps (×11) | V1 × 5 + V2 × 6 | `Voices - Essentials/Voice_Male/Voice_Male_Breath/` |
| Male shocked breaths (×10) | V1 × 5, V2 × 5 | `Voices - Essentials/Voice_Male/Voice_Male_Breath/` |
| Male sighs (×10) | V1 × 4, V2 × 6 | `Voices - Essentials/Voice_Male/Voice_Male_Expressions/` |
| Male effort sounds | V1 × 15, V2 × 12 | `Voices - Essentials/Voice_Male/Voice_Male_Effort/` |
| Male pain sounds | V1 × 15, V2 × 4 | `Voices - Essentials/Voice_Male/Voice_Male_Pain/` |
| Female breath (full set) | Loops, gasps, shocked, single × 13 | `Voices - Essentials/Voice_Female/Voice_Female_Breath/` |
| Female sighs, expressions | Multiple | `Voices - Essentials/Voice_Female/Voice_Female_Expressions/` |

---

## License Note

**CC0** = Public Domain — no attribution needed, safe for commercial release.
**CC BY** = free to use but requires credit in game or readme.
**CC BY-NC** = non-commercial only — avoid if planning to sell the game.

On Freesound, filter by **Creative Commons 0** before downloading. On OpenGameArt, look for the CC0 tag. Kevin MacLeod (Incompetech) uses CC BY — keep a credits file.

---

## Asset Matrix — M1 through M17

`✓` = exists and usable | `〜` = exists, needs evaluation | `⬜` = missing, can placeholder | `🔴` = missing, blocks milestone | `—` = not needed

| Milestone 				| Ambient / Music 	| SFX 					| Voice / Breath 	| Monster Audio | 3D Models 		| UI Sprites 	|
|---						|---				|---					|---				|---			|---				|---			|
| M1 – Movement 			| — 				| — 					| — 				| — 			| ✓ 				| — 			|
| M2 – Interaction 			| — 				| — 					| — 				| — 			| — 				| — 			|
| M3 – Treasure 			| — 				| ✓ pickup chime 		| — 				| — 			| ✓ treasure prop 	| — 			|
| M4 – Escape 				| — 				| — 					| — 				| — 			| — 				| — 			|
| M5 – Capture 				| — 				| — 					| — 				| — 			| — 				| — 			|
| M6 – UI 					| — 				| ✓ button click 		| — 				| — 			| — 				| — 			|
| M7 – Heartbeat 			| — 				| ✓ heartbeat clip 		| ✓ loops exist 	| — 			| — 				| — 			|
| M8 – Audio 				| ✓ C&D->Alert–Panic; ✓ Victorian melody | ✓ heartbeat 	| ✓ loops exist | —		| — 				| — 			|
| M9 – Safe Zones 			| — 				| ✓ sacred audio 		| — 				| — 			| ⬜ threshold marker | — 			|
| **M10 – Clues** 			| — 				| ⬜ examine sound 		| — 				| — 			| ⬜ clue props 		| — 			|
| **M11 – Pause/Sneak/HUD** | — 				| ✓ in Voices pack 		| ✓ inhale, exhale, gasp | — 		| — 				| ✓ icons × 3 	|
| **M12 – Monster AI** 		| — 				| ⬜ light extinguish 	| — 				| 🔴 roar one-shot; ⬜ footsteps | ⬜ capsule | — 	|
| **M13 – Sidequest** 		| — 				| 🔴 altar tone; ✓ fire loops | — 			| — 			| 🔴 collectibles + altar | — 		|
| **M14 – Phobia Pool** 	| — 				| 🔴 jumpscare sting × 1 min | ✓ breath in Voices | — 		| ⬜ eye glow sprite | — 			|
| **M15 – Player Hands** 	| — 				| — | — | — | 🔴 hands + candle | — |
| **M16 – High Score** 		| — 				| ✓ UI click | — | — | — | — |
| **M17 – Level Design** 	| ✓ Victorian melody; ✓ C&D layers | 🔴 stone footsteps | — | 🔴 full Minotaur set | 🔴 environment kit + props | — |

---

## Milestone-by-Milestone Detail

---

### Key additions:

- **Freesound searches** — each SFX entry links directly to a pre-filled search URL. You still need to filter by CC0 in the Freesound UI once you land there.
- **game-icons.net** — direct tag links for lungs, footprint, and run icons. No drawing required: browse, pick one, click Download, set white on transparent, done.
- **Incompetech + Musopen** — both linked for the Victorian melody. Musopen is fully public domain (no attribution), Incompetech needs a credits line.
- **Mixamo** — linked for hand animations in case the M15 hands package doesn't include them.
- **Full source table** at the bottom with links to all seven sites.

---

### M1–M6 (Complete — minor gaps, fill during M17 polish)

| Asset 					| Status | Link 																							|
|---						|---	 |---																								|
| M3 Treasure prop			| ✓		 | Unity My Assets																					|
| M3 Treasure pickup chime 	| ✓ 	 | [freesound.org — search "coin pickup"](https://freesound.org/search/?q=coin+pickup) — filter CC0 |
| M6 UI button click 		| ✓ 	 | [freesound.org — search "ui click"](https://freesound.org/search/?q=ui+click) — filter CC0 		|

---

### M7–M8 (Complete — all audio found)

The Caves and Dungeons pack covers Alert → Panic atmospheric layers. Victorian melody = Holst St. Paul's Suite Op. 29 No. 2, III. Intermezzo (Musopen — public domain, no attribution needed).

| Asset 									| Status | Link / Notes 																			|
|---										|---	 |---																						|
| Heartbeat audio loop 						| ✓ 	 | [freesound.org — search "heartbeat loop"](https://freesound.org/search/?q=heartbeat+loop) — filter CC0; pick ~60 BPM, pitched up per state 					   																			   |
| Victorian exploration melody (Calm state) | ✓ 	 | [incompetech.com](https://incompetech.com/music/royalty-free/) — search "Victorian", "Edwardian", "exploration"; CC BY (add to credits). Also try [musopen.org](https://musopen.org) for public domain recordings of period composers (Elgar, Stanford, Holst early works) 								 																			 |
| Alert ambient layer 						| ✓ 	 | `Caves and Dungeons/` — evaluate Forbidden Temple Loop or Ash and Dust Loop 				|
| Fear ambient layer 						| ✓ 	 | `Caves and Dungeons/` — evaluate Halls of the Damned Loop or An Unwelcome Presence Loop 	|
| Panic ambient layer 						| ✓ 	 | `Caves and Dungeons/` — evaluate The Void Loop or Kothar's Iron Tomb Loop 				|
| Musical texture layer 					| ✓ 	 | `Caves and Dungeons/` — evaluate Ethereal Rites Loop or Decrepit Cathedral Loop 			|

---

### M9 (Complete — prop missing, deferred to M17)

| Asset 									 | Status | Link / Notes 																	|
|---										 |---	  |---																			|
| Safe zone threshold marker (carved symbol) | ⬜ 	  | M17 level design pass. May be included in the environment kit purchase 		|
| Sacred audio (bronze resonance) 			 | ✓ 	  | `SFX_sacredAudio` — imported. 3D AudioSource, Linear rolloff, Min 1 / Max 5m 	|

---

### M10 (Mythology Clue System — no blocking assets)

| Asset 					| Status 		| Link 																								|
|---						|---			|---																								|
| Examine sound (whisper)	| ⬜ optional 	| use `Voice_Male_V1_Breath_Single_Mono_01` from Voices - Essentials as a faint whisper 			|
| Clue visual props 		| ⬜ optional 	| Empty GameObjects now; M17 pass 											|

---

### M11 (Pause Menu, Sneak Mode & HUD — blocked on 3 icons only)

All audio exists in Voices - Essentials. Icons must be created or sourced.

| Asset 							| Status | Link / File to use																				|
|---								|---	 |---|
| Deep inhale (sneak activation) 	| ✓ 	 | `Voices - Essentials/Voice_Male/Voice_Male_Breath/Voice_Male_V1_Breath_Single_Mono_` — listen to 01–13, pick the deepest one 																													|
| Quiet exhale (voluntary release)	| ✓ 	 | `Voices - Essentials/Voice_Male/Voice_Male_Expressions/Voice_Male_V1_Sigh_Mono_01` — pick the most subtle sigh 																																	  |
| Sharp gasp (forced exhale) 		| ✓ 	 | `Voices - Essentials/Voice_Male/Voice_Male_Breath/Voice_Male_V1_Breath_Shocked_Mono_01` or `Breath_Gasp_Mono_01` 																														  |
| Movement icon — Sneak 			| ✓ 	 | `UISprite_mute.png` from game-icons.net — CC BY, add to credits 				|
| Movement icon — Walk 				| ✓ 	 | `UISprite_walk.png` from game-icons.net — CC BY, add to credits 				|
| Movement icon — Sprint 			| ✓ 	 | `UISprite_run.png` from game-icons.net — CC BY, add to credits 				|

**Using game-icons.net:** Browse to the icon, click Download, choose PNG, set colour to white and background to transparent. Done. No drawing required.

---

### M12 (Monster AI — blocked on Minotaur roar only)

| Asset 						| Status | Link / Notes																							|
|---							|---	 |---																									|
| Minotaur roar one-shot 		| 🔴 	 | [freesound.org — search "bull roar"](https://freesound.org/search/?q=bull+roar) — filter CC0. Also try "creature roar deep" or "monster growl" 																										  |
| Monster wandering footsteps 	| ⬜ 	 | [freesound.org — search "heavy footsteps stone"](https://freesound.org/search/?q=heavy+footsteps+stone) — filter CC0 																																   |
| Monster heavy breathing 		| ⬜ 	 | `Voices - Essentials/Voice_Male/Voice_Male_Breath/Voice_Male_V1_Breath_Mouth_Moderate_Sequence_Mono` — test as creature breathing; or [freesound.org — search "heavy breathing"](https://freesound.org/search/?q=heavy+breathing) 					  |
| Player light extinguish 		| ⬜ 	 | [freesound.org — search "candle extinguish"](https://freesound.org/search/?q=candle+extinguish) — filter CC0 																																		   |

---

### M13 (Sidequest — blocked on props and altar tone)

| Asset | Status | Link / Notes |
|---|---|---|
| 3× sidequest collectible props | 🔴 | [Unity Asset Store](https://assetstore.unity.com) — search "ancient artifacts", "archaeological props", "Greek artifacts". Look for URP-compatible packs |
| Altar prop | 🔴 | [Unity Asset Store](https://assetstore.unity.com) — search "stone altar", "Greek altar", "offering pedestal". May be included in M17 environment kit — buy that first |
| Altar acceptance tone | 🔴 | [freesound.org — search "singing bowl"](https://freesound.org/search/?q=singing+bowl) — filter CC0. Also try "gong resonance" or "tibetan bowl" |
| Torch ignition (whomp) | 🔴 | [freesound.org — search "torch ignite"](https://freesound.org/search/?q=torch+ignite) — filter CC0. Also try "fire whomp" or "flame catch" |
| Burning torch loop | ✓ | `Nature - Essentials/Ambiance_Firecamp_Small_Loop_Mono.wav` — test this first |
| Hidden room stone grind | ⬜ | [freesound.org — search "stone grinding"](https://freesound.org/search/?q=stone+grinding) — filter CC0. Also "stone door mechanism" |
| Wall torch props | ✓ | Unity My Assets — Lanterns and candles package |

---

### M14 (Phobia Pool — blocked on one jumpscare sting)

Minimum viable = 3 phobia types. Acousticophobia sting is the only blocker.

| Asset | Status | Link / Notes |
|---|---|---|
| Acousticophobia jumpscare sting | 🔴 | [freesound.org — search "jumpscare"](https://freesound.org/search/?q=jumpscare) — filter CC0. Also try "horror sting" or "scare stinger". Must be a sharp, sudden loud hit |
| Scopophobia breath from direction | ✓ | `Voices - Essentials/Voice_Male/Voice_Male_Breath/Voice_Male_V1_Breath_Single_Mono` series — play spatially positioned as 3D audio |
| Nyctophobia audio | ⬜ | [freesound.org — search "tension riser"](https://freesound.org/search/?q=tension+riser) — or approximate by ducking ambience in the mixer |
| Thanatophobia creak | 〜 | `CreakDoor.wav` already imported — usable placeholder |
| Entomophobia skitter | ⬜ | [freesound.org — search "insect skitter"](https://freesound.org/search/?q=insect+skitter) — filter CC0 |
| Chiroptophobia wing burst | ⬜ | [freesound.org — search "bat burst"](https://freesound.org/search/?q=bat+burst) — filter CC0. Also "sudden wings" |
| Musophobia scrabble | ⬜ | [freesound.org — search "rat scrabble"](https://freesound.org/search/?q=rat+scrabble) — filter CC0 |

---

### M15 (Player Hands — blocked on hand model)

| Asset | Status | Link / Notes |
|---|---|---|
| First-person hands model (rigged, period-appropriate) | 🔴 | [Unity Asset Store](https://assetstore.unity.com) — search "first person hands", "FPS arms". Confirm: URP compatible, Unity 6, includes animations. Victorian gloves preferred but any period neutral hands work |
| Candle (handheld, Victorian) | 🔴 | Often sold with hand packages. If separate: [Unity Asset Store](https://assetstore.unity.com) — search "handheld candle", "Victorian candle prop" |
| Animations (reach, idle, walk with candle) | 🔴 | May come with hand model. If not: [Mixamo](https://www.mixamo.com) — free animated hand/arm rigs, Adobe account required |

---

### M16 (High Score — no blocking assets)

| Asset | Status | Link |
|---|---|---|
| UI click sound | ✓ | Reuse M6 button click (`SFX_restartButton`) — same feel, no new file needed |

---

### M17 (Level Design Pass — main purchases)

| Asset | Status | Link / Notes |
|---|---|---|
| Greek / ancient dungeon modular environment kit | 🔴 | [Unity Asset Store](https://assetstore.unity.com) — search "Greek labyrinth", "ancient dungeon modular", "stone corridor kit". Filter: URP, Unity 6 compatible |
| Minor treasure props × 5–7 | 🔴 | [Unity Asset Store](https://assetstore.unity.com) — search "ancient artifacts", "Greek props", "museum relics". Need visual variety (urns, coins, jewellery, relics) |
| Major Treasure hero prop | 🔴 | [Unity Asset Store](https://assetstore.unity.com) — search "golden idol", "ancient artifact centerpiece". One standout, impressive piece |
| Minotaur monster model (final) | 🔴 | [Unity Asset Store](https://assetstore.unity.com) — search "minotaur", "bull creature", "Greek monster". Confirm: URP, NavMesh-compatible, rigged |
| Monster audio — full Minotaur set | 🔴 | [freesound.org — search "minotaur"](https://freesound.org/search/?q=minotaur) — filter CC0. Also try "bull sounds", "creature stomp", "monster breath loop" |
| Stone footsteps — walk cadence | 🔴 | [freesound.org — search "boots stone footstep"](https://freesound.org/search/?q=boots+stone+footstep) — filter CC0 |
| Stone footsteps — sprint cadence | 🔴 | [freesound.org — search "running stone footstep"](https://freesound.org/search/?q=running+stone+footstep) — filter CC0 |
| Victorian exploration melody (Calm music) | 🔴 | [incompetech.com](https://incompetech.com/music/royalty-free/) — search "Victorian" or browse Adventure/Orchestral; CC BY. Also [musopen.org](https://musopen.org) — public domain recordings, no attribution needed |
| Previous explorer remains / bones | ⬜ | [Unity Asset Store](https://assetstore.unity.com) — search "scattered bones", "skeleton remains". Or check [opengameart.org](https://opengameart.org) — search "bones" with CC0 filter |
| Altar acceptance tone | Already in M13 | — |
| Safe zone threshold marker | 〜 | May be in environment kit |
| Wall torches | 〜 | May be in environment kit |

---

## What to Source Before Each Milestone

| Milestone | Must have before starting | Where |
|---|---|---|
| M10 | Nothing — start now | — |
| M11 | Draw / download 3 icons | [game-icons.net](https://game-icons.net) — 5 minutes per icon |
| M12 | One Minotaur roar clip | [freesound.org — "bull roar"](https://freesound.org/search/?q=bull+roar) |
| M13 | Collectible props + altar + altar tone + torch ignition | Asset Store + Freesound links above |
| M14 | One jumpscare sting | [freesound.org — "jumpscare"](https://freesound.org/search/?q=jumpscare) |
| M15 | FPS hands model with animations | Asset Store |
| M16 | Nothing | — |
| M17 | Environment kit + treasure props + footsteps + Victorian music | Asset Store + Freesound + Incompetech |

---

## Source Reference

| Source | Best for | Link |
|---|---|---|
| Freesound | SFX, ambient, creature sounds — filter CC0 | https://freesound.org |
| game-icons.net | UI icons — CC BY (add to credits) | https://game-icons.net |
| Incompetech / Kevin MacLeod | Royalty-free music — CC BY (add to credits) | https://incompetech.com/music/royalty-free/ |
| Musopen | Public domain classical recordings — free, no attribution | https://musopen.org |
| OpenGameArt | CC0 sprites, audio, some 3D | https://opengameart.org |
| Unity Asset Store | 3D models, environment kits, hand packages | https://assetstore.unity.com |
| Mixamo | Free animated character/hand rigs — Adobe account | https://www.mixamo.com |
| Pixabay Audio | SFX and short music — CC0 | https://pixabay.com/music/ |

## What assets have been found or have a accepted solution / placeholder

| Status | Milestone  | Asset | What to use / File name | New filename / folder |
| [x] | M3 - Treasure | pickup Chime | yossirafa100 - Coins bounce inside cloth | SFX_pickupChime |
| [x] | M3 - Treasure | Treasure prop | Unity My Assets | DavePixel
| [x] | M6 - UI | UI restart button | knufds - typewriter-bell-carriage-reset | SFX_restartButton |
| [x] | M7 - Heartbeat | Heartbeat clip ~60bpm | FenrirFangs - Human Heartbeat (60BPM) | SFX_heartbeat_60_bpm |
| [x] | M8 - Audio | Victorian exploration melody | St. Paul's Suite, Op. 29 no. 2 - III. Intermezzo. Andante con Moto | Ambient_calmMelody_St. Paul's Suite, Op. 29 no. 2 |
| [x] | M9 Safe Zones | Safe Zone Marker - visual | Use the "Decal Projector" idea |
| [x] | M9 Safe Zones | Sacred Audio - Bronze Resonance Audio | ? | 
| [x] | M9 Safe Zones | 3D Carved symbol | Placeholder from game-icons.net | 
| [x] | M10 - Clues | Examine Sound, whisper | `Voice_Male_V1_Breath_Single_Mono_01` from Voices - Essentials as a faint whisper |
| [ ] | M10 - Clues | Clues | Use a decal projector here for mythology related symbols, i.e. sunRays, broken horn, blood marks, etc. Using game-icons.net? | 
| [x] | M11 – Pause/Sneak/HUD | Icons: Sneak, Walk, Sprint | game-icons.net | mute.png, walk.png, run.png | UISprite_mute, UISprite_run, UISprite_walk|
| [x] | M11 – Pause/Sneak/HUD| Sneak - in Voices pack inhale | `Voices - Essentials/Voice_Male/Voice_Male_Breath/Voice_Male_V1_Breath_Single_Mono_` — listen to 01–13, pick the deepest one |
| [x] | M11 – Pause/Sneak/HUD| Sneak - in Voices pack exhale | `Voices - Essentials/Voice_Male/Voice_Male_Expressions/Voice_Male_V1_Sigh_Mono_01` — pick the most subtle sigh |
| [x] | M11 – Pause/Sneak/HUD| Sneak - in Voices pack  gasp | `Voices - Essentials/Voice_Male/Voice_Male_Breath/Voice_Male_V1_Breath_Shocked_Mono_01` or `Breath_Gasp_Mono_01` |
| [ ] | M12 -  Monster AI | light extinguish | [freesound.org — search "candle extinguish"](https://freesound.org/search/?q=candle+extinguish) — filter CC0 |
| [ ] | M12 - Monster AI | roar one-shot | [freesound.org — search "bull roar"](https://freesound.org/search/?q=bull+roar) — filter CC0. Also try "creature roar deep" or "monster growl" |
| [ ] | M12 - Monster AI | footsteps | [freesound.org — search "heavy footsteps stone"](https://freesound.org/search/?q=heavy+footsteps+stone) — filter CC0 |
| [ ] | M12 - Monster AI | Heavy breathing | `Voices - Essentials/Voice_Male/Voice_Male_Breath/Voice_Male_V1_Breath_Mouth_Moderate_Sequence_Mono` — test as creature breathing; or [freesound.org — search "heavy breathing"](https://freesound.org/search/?q=heavy+breathing) |
| [ ] | M12 - Monster AI | eye glow sprite | ? |
| [ ] | M12 - Monster AI | Silhouette / Shadow | ? |
| [ ] | M12 - Monster AI | capsule | ? |
| [ ] | M13 - Side quest | 3× side quest collectible props | [Unity Asset Store](https://assetstore.unity.com) — search "ancient artifacts", "archaeological props", "Greek artifacts". Look for URP-compatible packs |
| [x] | M13 - Side quest | Wall torch props | Unity My Assets | Lanterns and candles |
| [ ] | M13 - Side quest| Altar prop | [Unity Asset Store](https://assetstore.unity.com) — search "stone altar", "Greek altar", "offering pedestal". May be included in M17 environment kit — buy that first |
| [ ] | M13 - Side quest| Altar acceptance tone | freesound.org — search "singing bowl"](https://freesound.org/search/?q=singing+bowl) — filter CC0. Also try "gong resonance" or "Tibetan bowl" |
| [ ] | M13 - Side quest| Torch ignition (whomp) | [freesound.org — search "torch ignite"](https://freesound.org/search/?q=torch+ignite) — filter CC0. Also try "fire whomp" or "flame catch" |
| [x] | M13 - Sidequest | Burning torch loop | `Nature - Essentials/Ambiance_Firecamp_Small_Loop_Mono.wav` — test this first |
| [ ] | M13 - Sidequest| Hidden room stone grind | [freesound.org — search "stone grinding"](https://freesound.org/search/?q=stone+grinding) — filter CC0. Also "stone door mechanism" |
| [ ] | M14 - Phobia Pool | Acousticophobia Jumpscare sting | [freesound.org — search "jumpscare"](https://freesound.org/search/?q=jumpscare) — filter CC0. Also try "horror sting" or "scare stinger". Must be a sharp, sudden loud hits |
| [x] | M14 - Phobia Pool| Scopophobia breath from direction | `Voices - Essentials/Voice_Male/Voice_Male_Breath/Voice_Male_V1_Breath_Single_Mono` series — play spatially positioned as 3D audio |
| [ ] | M14 - Phobia Pool| Nyctophobia audio | [freesound.org — search "tension riser"](https://freesound.org/search/?q=tension+riser) — or approximate by ducking ambience in the mixer |
| [x] | M14 - Phobia Pool| Thanatophobia creak | `CreakDoor.wav` already imported — usable placeholder |
| [ ] | M14 - Phobia Pool| Entomophobia skitter | [freesound.org — search "insect skitter"](https://freesound.org/search/?q=insect+skitter) — filter CC0 |
| [ ] | M14 - Phobia Pool| Chiroptophobia wing burst | [freesound.org — search "bat burst"](https://freesound.org/search/?q=bat+burst) — filter CC0. Also "sudden wings" |
| [ ] | M14 - Phobia Pool | Musophobia scrabble | [freesound.org — search "rat scrabble"](https://freesound.org/search/?q=rat+scrabble) — filter CC0 |
| [ ] | M15 - Player Hands| First-person hands model (rigged, period-appropriate) | [Unity Asset Store](https://assetstore.unity.com) — search "first person hands", "FPS arms". Confirm: URP compatible, Unity 6, includes animations. Victorian gloves preferred but any period neutral hands work |
| [ ] | M15 - Player Hands| Candle (handheld, Victorian) | Often sold with hand packages. If separate: [Unity Asset Store](https://assetstore.unity.com) — search "handheld candle", "Victorian candle prop" |
| [ ] | M15 - Player Hands| Animations (reach, idle, walk with candle) | May come with hand model. If not: [Mixamo](https://www.mixamo.com) — free animated hand/arm rigs, Adobe account required |
| [ ] | M16 - High Score | UI click sound | [freesound.org — search "ui click"](https://freesound.org/search/?q=ui+click) — same as M6. Filter CC0 |
| [ ] | M17 - Level Design | Greek / ancient dungeon modular environment kit | [Unity Asset Store](https://assetstore.unity.com) — search "Greek labyrinth", "ancient dungeon modular", "stone corridor kit". Filter: URP, Unity 6 compatible |
| [ ] | M17 - Level Design | Minor treasure props × 5–7 | [Unity Asset Store](https://assetstore.unity.com) — search "ancient artifacts", "Greek props", "museum relics". Need visual variety (urns, coins, jewellery, relics) |
| [ ] | M17 - Level Design | Major Treasure hero prop | [Unity Asset Store](https://assetstore.unity.com) — search "golden idol", "ancient artifact centerpiece". One standout, impressive piece |
| [ ] | M17 - Level Design | Minotaur monster model (final) | [Unity Asset Store](https://assetstore.unity.com) — search "minotaur", "bull creature", "Greek monster". Confirm: URP, NavMesh-compatible, rigged |
| [ ] | M17 - Level Design | Monster audio — full Minotaur set | [freesound.org — search "minotaur"](https://freesound.org/search/?q=minotaur) — filter CC0. Also try "bull sounds", "creature stomp", "monster breath loop" |
| [ ] | M17 - Level Design | Stone footsteps — walk cadence | [freesound.org — search "boots stone footstep"](https://freesound.org/search/?q=boots+stone+footstep) — filter CC0 |
| [ ] | M17 - Level Design | Stone footsteps — sprint cadence | [freesound.org — search "running stone footstep"](https://freesound.org/search/?q=running+stone+footstep) — filter CC0 |
| [ ] | M17 - Level Design | Victorian exploration melody (Calm music) | [incompetech.com](https://incompetech.com/music/royalty-free/) — search "Victorian" or browse Adventure/Orchestral; CC BY. Also [musopen.org](https://musopen.org) — public domain recordings, no attribution needed |
| [ ] | M17 - Level Design | Previous explorer remains / bones | [Unity Asset Store](https://assetstore.unity.com) — search "scattered bones", "skeleton remains". Or check [opengameart.org](https://opengameart.org) — search "bones" with CC0 filter |
| [ ] | M17 - Level Design | Altar acceptance tone | Already in M13 |
| [ ] | M17 - Level Design | Safe zone threshold marker | May be in environment kit |
| [ ] | M17 - Level Design | Wall torches | May be in environment kit |

