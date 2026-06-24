using UnityEngine;

// ": IInteractable" is the compiler contract. If InteractPrompt or Interact()
// are missing, the build fails with a clear error naming the missing member.
public class TestInteractable : MonoBehaviour, IInteractable
{
    // Expression-bodied property: the shortest way to write a get-only property
    // that always returns the same value. Equivalent to:
    //   public string InteractPrompt { get { return "Test Cube"; } }
    public string InteractPrompt => "Test Cube";

    public void Interact()
    {
        // Log confirms Interact() was reached. The prefix makes it easy to
        // filter in the Console when other scripts are also logging.
#if UNITY_EDITOR
        Debug.Log("[TestInteractable] Interact() called.");
#endif

        // Colour change gives a visual confirmation without needing the UI
        // to be built yet. GetComponent here is fine — Interact() is not
        // called every frame, only on a keypress.
        GetComponent<Renderer>().material.color = Color.green;
    }
}
