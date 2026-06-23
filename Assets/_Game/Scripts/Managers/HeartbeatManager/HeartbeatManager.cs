using UnityEngine;
using System;

public enum HeartbeatState { Calm, Alert, Fear, Panic }

public class HeartbeatManager : MonoBehaviour
{
    // ── State ─────────────────────────────────────────────────────────────

    // Read-only from outside. Calm -> Alert -> Fear -> Panic.
    public static HeartbeatState CurrentState { get; private set; }

    // 0= silent, 1 = heavy. Rises fast with heartbeat, decays slowly.
    // Monster system (M12) reads this - elevated breathing is detectable even when still.
    public static float BreathingLevel { get; private set; }

    // ── Events ────────────────────────────────────────────────────────────

    // Fires when state changes. HeartbeatVisuals and AudioManager (M8) subscribe here.
    public static event Action<HeartbeatState> OnStateChanged;

    // ── Tuning ────────────────────────────────────────────────────────────

    // How fast noise drains when the player is quiet. Lower = longer tension.
    [SerializeField] private float noiseDecayRate = 0.15f;

    // Breathing rises fast (instant feedback) but decays slow (hiding tension).
    [SerializeField] private float breathingRiseRate = 3f;
    [SerializeField] private float breathingDecayRate = 0.1f;

    // ── Runtime ───────────────────────────────────────────────────────────

    // Hidden 0-1 value driven by player actions. State machine reads this each frame.
    private float _noisePressure;

    // ── Lifecycle ─────────────────────────────────────────────────────────

    // Reset static state so each Play session starts clean.
    private void Awake()
    {
        CurrentState = HeartbeatState.Calm;
        BreathingLevel = 0f;
        GameManager.OnEscapeTriggered += HandleEscapeTrigger;
    }

    // Static fields survive stopping Play in the editor - reset here to prevent
    // stale state on the next run.
    private void OnDestroy()
    {
        CurrentState = HeartbeatState.Calm;
        BreathingLevel = 0f;
        GameManager.OnEscapeTriggered -= HandleEscapeTrigger;
    }

    private void Update()
    {
        // 1. Drain noise pressure. 2. Recalculate state. 3. Move breathing toward target.
        _noisePressure = Mathf.Max(0f, _noisePressure - noiseDecayRate * Time.deltaTime);
        UpdateState();
        UpdateBreathing();
    }

    // ── State Machine ─────────────────────────────────────────────────────

    // Reads current noise pressure and transitions to matching state.
    private void UpdateState()
    {
        HeartbeatState target;

        if (_noisePressure >= 0.75f) target = HeartbeatState.Panic;
        else if (_noisePressure >= 0.5f) target = HeartbeatState.Fear;
        else if (_noisePressure >= 0.25f) target = HeartbeatState.Alert;
        else target = HeartbeatState.Calm;

        if (target != CurrentState) ChangeState(target);
    }

    // Smoothly moves BreathingLevel toward the target for the current state.
    // Rise rate is fast (instant feedback); decay rate is slow (hiding tension).
    private void UpdateBreathing()
    {
        float target = CurrentState switch
        {
            HeartbeatState.Alert => 0.2f,
            HeartbeatState.Fear => 0.6f,
            HeartbeatState.Panic => 1.0f,
            _ => 0f
        };

        float rate = BreathingLevel < target ? breathingRiseRate : breathingDecayRate;
        BreathingLevel = Mathf.MoveTowards(BreathingLevel, target, rate * Time.deltaTime);
    }

    // Applies the new state and notifies all subscribers.
    private void ChangeState(HeartbeatState newState)
    {
        CurrentState = newState;
        OnStateChanged?.Invoke(newState);
    }

    // Major Treasure picked up - force Panic immediately regardless of current noise.
    private void HandleEscapeTrigger()
    {
        _noisePressure = 1f;
        if (CurrentState != HeartbeatState.Panic) ChangeState(HeartbeatState.Panic);
    }

    // ── Public API ────────────────────────────────────────────────────────

    // Called every frame by PlayerNoiseEmitter while the player is moving.
    // Uses Max so it only raises pressure - decay happens in Update().
    public void SetContinuousNoise(float level)
    {
        _noisePressure = Mathf.Max(_noisePressure, Mathf.Clamp01(level));
    }

    // One-time spike - treasure collection, jumpscare triggers.
    public void AddNoiseBurst(float amount)
    {
        _noisePressure = Mathf.Clamp01(_noisePressure + amount);
    }

    // Called by jumpscare system (M14) to raise state by exactly one step.
    public void PushUpOneStep()
    {
        if (CurrentState < HeartbeatState.Panic) ChangeState((HeartbeatState)((int)CurrentState + 1));
    }
}