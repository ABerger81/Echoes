// Interfaces live outside MonoBehaviour — no "using UnityEngine" needed here.
// Any class that writes ": IInteractable" must provide both members below,
// or the compiler refuses to build. That's the contract.

public interface IInteractable
{
    // Text shown in the UI when the player looks at this object.
    // "get;" means read-only from the outside — implementing classes
    // decide the value, nothing else can overwrite it.
    string InteractPrompt { get; }

    // Called by Interactor when the player presses the interact key
    // while this object is in the crosshair.
    void Interact();
}
