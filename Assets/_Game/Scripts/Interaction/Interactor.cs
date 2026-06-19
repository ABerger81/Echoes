using UnityEngine;

// RequireComponent prevents this script being added to a GameObject without
// a Camera — you get a clear editor error instead of a confusing null reference
// at runtime.
[RequireComponent(typeof(Camera))]
public class Interactor : MonoBehaviour
{
    // Shown in the Inspector so you can tune the reach without touching code.
    [SerializeField] private float interactRange = 3f;

    // C# events: other scripts subscribe to these to react to what the player
    // is looking at. The '?' in '?.Invoke()' means "only call if at least one
    // listener is subscribed" — prevents a NullReferenceException when nothing
    // is listening yet.
    public event System.Action<IInteractable> OnInteractableFound;
    public event System.Action OnInteractableLost;

    private Camera _camera;
    private IInteractable _currentInteractable;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        CheckForInteractable();

        if (_currentInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            _currentInteractable.Interact();
        }
    }

    private void CheckForInteractable()
    {
        // ViewportPointToRay(0.5, 0.5, 0) fires from the exact centre of the
        // screen regardless of resolution — this is our crosshair ray.
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, interactRange))
        {
            // Ask the hit object if it has an IInteractable component.
            // GetComponent<IInteractable>() works with interfaces, not just
            // concrete types. Returns null if the object doesn't implement it.
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                // Guard: only fire the event when we move to a new target,
                // not every frame while staring at the same object.
                if (interactable != _currentInteractable)
                {
                    _currentInteractable = interactable;
                    OnInteractableFound?.Invoke(_currentInteractable);
                }
                return; // Found something — skip the "lost" logic below.
            }
        }

        // Ray hit nothing with an IInteractable. If we were tracking one,
        // clear it and let listeners know so the UI can hide its prompt.
        if (_currentInteractable != null)
        {
            _currentInteractable = null;
            OnInteractableLost?.Invoke();
        }
    }
}
