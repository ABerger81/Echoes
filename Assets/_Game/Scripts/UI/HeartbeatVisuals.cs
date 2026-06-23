using UnityEngine;


public class HeartbeatVisuals : MonoBehaviour
{

    // ── Scene References ──────────────────────────────────────────────────

    // Fullscreen dark image with CanvasGroup. Alpha controlled by heartbeat state.
    [SerializeField] private CanvasGroup vignetteOverlay;

    // ── Lifecycle ─────────────────────────────────────────────────────────
    private void Awake()
    {
        HeartbeatManager.OnStateChanged += HandleStateChanged;

        // Overlay starts invisible and never blocks clicks or raycasts underneath.
        vignetteOverlay.alpha = 0f;
        vignetteOverlay.interactable = false;
        vignetteOverlay.blocksRaycasts = false;
    }

    private void OnDestroy()
    {
        HeartbeatManager.OnStateChanged -= HandleStateChanged;
    }


    // ── Visual Response ───────────────────────────────────────────────────

    // Sets vignette darkness based on current state. Replace with URP Volume effect in M8.
    private void HandleStateChanged(HeartbeatState state)
    {
        vignetteOverlay.alpha = state switch
        {
            HeartbeatState.Alert => 0.1f,
            HeartbeatState.Fear => 0.25f,
            HeartbeatState.Panic => 0.45f,
            _ => 0f
        };
    }
}