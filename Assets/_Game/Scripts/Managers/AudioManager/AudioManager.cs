using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // ── Mixer References ──────────────────────────────────────────────────
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioMixerSnapshot snapCalm;
    [SerializeField] private AudioMixerSnapshot snapAlert;
    [SerializeField] private AudioMixerSnapshot snapFear;
    [SerializeField] private AudioMixerSnapshot snapPanic;

    // ── Tuning ────────────────────────────────────────────────────────────
    [SerializeField] private float escalateBlend = 0.3f;
    [SerializeField] private float deEscalateBlend = 3.0f;

    // ── SFX Clips ─────────────────────────────────────────────────────────
    [SerializeField] private AudioClip _pickupChime;
    [SerializeField] private AudioSource _sfxSource;

    // ── Heartbeat ──────────────────────────────────────────────────────────
    [SerializeField] private AudioSource _HeartbeatSource;

    // ── Melody ─────────────────────────────────────────────────────────────
    [SerializeField] private AudioSource _melodySource;

    // ── State Tracking ────────────────────────────────────────────────────
    private HeartbeatState _currentState;

    // ── Lifecycle ─────────────────────────────────────────────────────────
    private void Awake()
    {
        HeartbeatManager.OnStateChanged += HandleStateChanged;
        Treasure.OnCollected += HandleTreasureCollected;
        snapCalm.TransitionTo(0f);
        if (_HeartbeatSource != null)
            _HeartbeatSource.Play();
#if UNITY_EDITOR
        else
            Debug.LogWarning("[AudioManager] _HeartbeatSource is null — heartbeat will not play");
#endif
        if (_melodySource != null)
            _melodySource.Play();
#if UNITY_EDITOR
        else
            Debug.LogWarning("[AudioManager] _melodySource is null — melody will not play");
#endif
        _currentState = HeartbeatState.Calm;
    }

    private void OnDestroy()
    {
        HeartbeatManager.OnStateChanged -= HandleStateChanged;
        Treasure.OnCollected -= HandleTreasureCollected;
    }

    // ── Snapshot Control ──────────────────────────────────────────────────
    private void HandleStateChanged(HeartbeatState newState)
    {
#if UNITY_EDITOR
        Debug.Log($"[AudioManager] State: {newState}");
#endif
        // Escalation is fast (sudden danger); de-escalation is slow (earned calm).
        float blendTime = newState > _currentState ? escalateBlend : deEscalateBlend;

        AudioMixerSnapshot snapshot = newState switch
        {
            HeartbeatState.Alert => snapAlert,
            HeartbeatState.Fear => snapFear,
            HeartbeatState.Panic => snapPanic,
            _ => snapCalm
        };

        snapshot.TransitionTo(blendTime);
        _currentState = newState;
    }

    // ── SFX Handlers ──────────────────────────────────────────────────────
    private void HandleTreasureCollected(TreasureType type)
    {
        if (type == TreasureType.Minor && _pickupChime != null && _sfxSource != null)
            _sfxSource.PlayOneShot(_pickupChime);
    }

    // ── Public API ────────────────────────────────────────────────────────
    public void PlayJumpscare(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
