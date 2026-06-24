using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class HeartbeatVisuals : MonoBehaviour
{

    // ── Scene References ──────────────────────────────────────────────────

    // Global post-process Volume. Vignette intensity driven by heartbeat state.
    [SerializeField] private Volume postProcessVolume;
    private Vignette _vignette;
    [SerializeField] private float vignetteBlendSpeed = 0.3f;
    private float _targetIntensity;

    // ── Lifecycle ─────────────────────────────────────────────────────────
    private void Awake()
    {
        HeartbeatManager.OnStateChanged += HandleStateChanged;
        postProcessVolume.profile.TryGet(out _vignette);
    }

    private void OnDestroy()
    {
        HeartbeatManager.OnStateChanged -= HandleStateChanged;
    }

    private void Update()
    {
        if (_vignette == null) return;
        _vignette.intensity.value = Mathf.MoveTowards(_vignette.intensity.value, _targetIntensity, vignetteBlendSpeed * Time.deltaTime);
    }


    // ── Visual Response ───────────────────────────────────────────────────

    private void HandleStateChanged(HeartbeatState state)
    {
        _targetIntensity = state switch
        {
            HeartbeatState.Alert => 0.25f,
            HeartbeatState.Fear => 0.45f,
            HeartbeatState.Panic => 0.65f,
            _ => 0f
        };
    }
}