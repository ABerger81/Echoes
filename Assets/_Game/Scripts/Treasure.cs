using UnityEngine;

public class Treasure : MonoBehaviour, IInteractable
{
    // Set these per-object in the Inspector.
    // itemName lets each treasure have a unique name without a separate script.
    // treasureType determines what happens on pickup.
    [SerializeField] private string itemName = "Treasure";
    [SerializeField] private TreasureType treasureType;

    // Static event: any Treasure instance anywhere in the scene can fire this,
    // and GameManager subscribes once without needing a reference to each object.
    public static event System.Action<TreasureType> OnCollected;

    // IInteractable: builds the prompt from the item's name at runtime.
    public string InteractPrompt => $"Pick up {itemName}";

    // IInteractable: called by Interactor when the player presses E.
    public void Interact()
    {
        // Notify listeners (GameManager) before destroying —
        // if we destroyed first, the event payload would still be valid but
        // it's cleaner to fire while the object still exists.
        OnCollected?.Invoke(treasureType);
        Destroy(gameObject);
    }
}
