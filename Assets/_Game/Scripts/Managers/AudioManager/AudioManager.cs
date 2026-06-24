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

    // ── State Tracking ────────────────────────────────────────────────────
    private HeartbeatState _currentState;

    // ── Lifecycle ─────────────────────────────────────────────────────────
    private void Awake()
    {
        HeartbeatManager.OnStateChanged += HandleStateChanged;
        _currentState = HeartbeatState.Calm;
    }

    private void OnDestroy()
    {
        HeartbeatManager.OnStateChanged -= HandleStateChanged;
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

    // ── Public API ────────────────────────────────────────────────────────
    public void PlayJumpscare(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
