# Treasure System

## Purpose

Describes how treasure collection is implemented: the scripts involved, how they connect, and what is deliberately deferred to later milestones.

For gameplay rules (what treasure does from the player's perspective), see docs/mechanics.md — Treasure Collection.

---

## Files

| File | Role |
|---|---|
| `Assets/_Game/Scripts/TreasureType.cs` | Enum — Minor or Major. Determines behavior on pickup. |
| `Assets/_Game/Scripts/Treasure.cs` | MonoBehaviour + IInteractable. Fires a static event and destroys itself. |
| `Assets/_Game/Scripts/Managers/GameManager.cs` | Subscribes to Treasure.OnCollected. Tracks score. Fires OnScoreChanged. |

---

## Event Flow

```
Player presses E while looking at Treasure
  → Interactor.Update() calls Interact() on the IInteractable
    → Treasure.Interact() fires Treasure.OnCollected(TreasureType)
      → GameManager.HandleCollected(type) runs
          Minor → Score += 100, fires GameManager.OnScoreChanged(Score)
          Major → Escape trigger placeholder (wired in Milestone 4)
```

---

## Pattern: Static Event on Treasure

`Treasure.OnCollected` is a static event — it lives on the class, not on any instance.

This means:
- Any Treasure in the scene fires the same event regardless of where it is in the Hierarchy
- GameManager subscribes once in `Awake()` and receives all pickups automatically
- Treasure never imports or references GameManager
- Adding a new treasure to the level requires zero wiring

The tradeoff: static events outlive scene objects. GameManager must unsubscribe in `OnDestroy()` or a destroyed GameManager will still receive calls and throw a MissingReferenceException.

---

## Pattern: TreasureType over Subclasses

All treasure objects use the same `Treasure.cs` script. Behavior is selected by the `TreasureType` field in the Inspector, not by creating `MinorTreasure.cs` and `MajorTreasure.cs`.

This satisfies the Milestone 3 Definition of Done: "new treasure types can be added without modifying the interaction system." To add a third type, add a value to the enum and a case to `GameManager.HandleCollected()`.

---

## Deferred

| Feature | Where it belongs |
|---|---|
| Jumpscare chance on Minor Treasure pickup | Expansion — Milestone 7+ |
| Escape Phase trigger on Major Treasure | Milestone 4 |
| Score displayed on screen | Milestone 6 — UIManager subscribes to GameManager.OnScoreChanged |
