using UnityEngine;
using StarterAssets;

public class PlayerNoiseEmitter : MonoBehaviour
{
    // ── Scene References ──────────────────────────────────────────────────

    [SerializeField] private HeartbeatManager heartbeatManager;
    [SerializeField] private CharacterController characterController;

    // ── Tuning ────────────────────────────────────────────────────────────

    [SerializeField] private float walkNoiseLevel = 0.3f;
    [SerializeField] private float sprintNoiseLevel = 0.8f;

    // Reads sprint state directly from StarterAssets input rather than interferring from speed.
    [SerializeField] private StarterAssetsInputs starterAssetsInputs;
    [SerializeField] private float collectNoiseBurst = 0.25f;

    // ── Lifecycle ─────────────────────────────────────────────────────────

    private void Awake()
    {
        Treasure.OnCollected += HandleCollected;
    }

    private void OnDestroy()
    {
        Treasure.OnCollected -= HandleCollected;
    }

    // ── Noise Reporting ───────────────────────────────────────────────────

    // Read actual movement speed and report continuous noise to HeartbeatManager.
    // Idle = do nothing - noise decays naturally there.
    private void Update()
    {
        Vector3 horizontal = new Vector3(characterController.velocity.x, 0f, characterController.velocity.z);
        float speed = horizontal.magnitude;


        if (speed > 0.1f)
        {
            float noiseLevel = starterAssetsInputs.sprint ? sprintNoiseLevel : walkNoiseLevel;
            heartbeatManager.SetContinuousNoise(noiseLevel);
        }
    }

    // Minor Treasure pickup adds a one-time noise burst.
    // Major Treasure is handled via GameManager.OnEscapeTriggered -> HeartbeatManager directly.
    // Sidequest collectibles added at M13.
    private void HandleCollected(TreasureType type)
    {
        if (type == TreasureType.Minor) heartbeatManager.AddNoiseBurst(collectNoiseBurst);
    }
}
