using UnityEngine;
using TMPro;

public class InteractionUI : MonoBehaviour
{
    // Both set by dragging objects into these slots in the Inspector.
    // No Find() calls, no singletons — explicit wiring is easier to debug.
    [SerializeField] private Interactor interactor;
    [SerializeField] private TMP_Text promptText;

    private void Awake()
    {
        // '+=' subscribes our method to the event. From this point on,
        // whenever Interactor fires OnInteractableFound, ShowPrompt runs.
        interactor.OnInteractableFound += ShowPrompt;
        interactor.OnInteractableLost += HidePrompt;

        // Hide immediately so the prompt doesn't flash on screen at startup.
        HidePrompt();
    }

    private void OnDestroy()
    {
        // '-=' unsubscribes when this object is destroyed.
        // Skipping this causes Unity to call a method on a dead object
        // the next time the event fires — a MissingReferenceException.
        interactor.OnInteractableFound -= ShowPrompt;
        interactor.OnInteractableLost -= HidePrompt;
    }

    private void ShowPrompt(IInteractable interactable)
    {
        // InteractPrompt comes from whatever IInteractable the player is looking at.
        // InteractionUI never needs to know if it's a cube, door, or treasure.
        promptText.text = $"[E]  {interactable.InteractPrompt}";
        promptText.gameObject.SetActive(true);
    }

    private void HidePrompt()
    {
        promptText.gameObject.SetActive(false);
    }
}
