using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Read-only from outside: only GameManager can change the score.
    public int Score { get; private set; }

    // UIManager (Milestone 6) will subscribe to this to update the score display.
    public static event System.Action<int> OnScoreChanged;

    private void Awake()
    {
        Treasure.OnCollected += HandleCollected;
    }

    private void OnDestroy()
    {
        // Always unsubscribe from static events in OnDestroy.
        // Static events outlive individual GameObjects — without this,
        // a destroyed GameManager would still receive calls and throw errors.
        Treasure.OnCollected -= HandleCollected;
    }

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
                // Escape Phase triggers here in Milestone 4.
                Debug.Log("[GameManager] Major treasure collected. Escape trigger placeholder.");
                break;
        }
    }
}
