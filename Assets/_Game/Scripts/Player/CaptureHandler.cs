using UnityEngine;
using StarterAssets;

// Disables player movement and interaction when the level ends (win or capture).
// Wired in the Inspector — no scene search needed.
public class CaptureHandler : MonoBehaviour
{
    // FirstPersonController lives on FPC/PlayerCapsule.
    // Disabling it stops Update() and LateUpdate(), freezing movement and look.
    [SerializeField] private FirstPersonController playerController;

    // Interactor lives on FPC/MainCamera.
    // Disabling it stops the raycast and E-key check.
    [SerializeField] private Interactor interactor;

    private void Awake()
    {
        GameManager.OnTimerExpired += HandleCapture;
        GameManager.OnLevelComplete += HandleCapture;
    }

    private void OnDestroy()
    {
        GameManager.OnTimerExpired -= HandleCapture;
        GameManager.OnLevelComplete -= HandleCapture;
    }

    private void HandleCapture()
    {
        playerController.enabled = false;
        interactor.enabled = false;
    }
}
