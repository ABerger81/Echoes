using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ── Score ──────────────────────────────────────────────────────────────

    // Read-only from outside: only GameManager can change the score.
    public int Score { get; private set; }

    // UIManager (Milestone 6) subscribes to update the score display.
    public static event System.Action<int> OnScoreChanged;

    // ── Escape Phase ───────────────────────────────────────────────────────

    // True once the Major Treasure is picked up.
    // Static so ExitPoint can read it without a scene reference.
    public static bool IsEscapeTriggered { get; private set; }

    // True after the level ends (win or capture). Prevents duplicate event firing.
    private static bool _isLevelOver;

    // Fires when escape phase begins. Milestone 7 hooks this for heartbeat escalation.
    public static event System.Action OnEscapeTriggered;

    // Fires when the player reaches the Exit. Milestone 6 hooks this for the win screen.
    public static event System.Action OnLevelComplete;

    // Fires when the countdown hits zero. Milestone 5 hooks this for death / capture.
    public static event System.Action OnTimerExpired;

    // Set in the Inspector. Lower it to 5 during testing. Set to 0 to disable the timer.
    [SerializeField] private float escapeTimerDuration = 60f;

    private float _escapeTimer;

    // ── Lifecycle ──────────────────────────────────────────────────────────

    private void Awake()
    {
        Treasure.OnCollected += HandleCollected;
    }

    private void OnDestroy()
    {
        // Static fields survive stopping Play in the editor.
        // Reset here so the next run doesn't start with stale state.
        IsEscapeTriggered = false;
        _isLevelOver = false;
        Treasure.OnCollected -= HandleCollected;
    }

    private void Update()
    {
        // Timer only runs after escape is triggered, while duration remains, and before the level ends.
        if (!IsEscapeTriggered || _isLevelOver || escapeTimerDuration <= 0f) return;

        _escapeTimer -= Time.deltaTime;

        if (_escapeTimer <= 0f)
        {
            _isLevelOver = true;
            escapeTimerDuration = 0f; // prevent repeat firing
            OnTimerExpired?.Invoke();
            Debug.Log("[GameManager] Timer expired. Capture triggered.");
        }
    }

    // ── Handlers ───────────────────────────────────────────────────────────

    private void HandleCollected(TreasureType type)
    {
        switch (type)
        {
            case TreasureType.Minor:
                Score += 100;
                OnScoreChanged?.Invoke(Score);
                Debug.Log($"[GameManager] Minor treasure collected. Score: {Score}");
                break;

            case TreasureType.Major:
                IsEscapeTriggered = true;
                _escapeTimer = escapeTimerDuration;
                OnEscapeTriggered?.Invoke();
                Debug.Log("[GameManager] Escape triggered! Reach the exit.");
                break;
        }
    }

    // Called by ExitPoint when the player reaches the exit during escape phase.
    public static void TriggerLevelComplete()
    {
        if (_isLevelOver) return;
        _isLevelOver = true;
        OnLevelComplete?.Invoke();
        Debug.Log("[GameManager] Level complete!");
    }
}
