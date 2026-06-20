using UnityEngine;

// Attach to a trigger volume in the scene.
// When the Player enters it during the escape phase, the level ends.
public class ExitPoint : MonoBehaviour
{
    // The PlayerCapsule has both a CapsuleCollider and a CharacterController —
    // Unity fires OnTriggerEnter for each. This flag prevents double-firing.
    private bool _triggered;

    private void OnTriggerEnter(Collider other)
    {
        if (_triggered) return;

        // Ignore anything that isn't the Player.
        // Ignore the trigger if escape hasn't started yet — walking into the
        // exit before picking up Major Treasure does nothing.
        if (other.CompareTag("Player") && GameManager.IsEscapeTriggered)
        {
            _triggered = true;
            GameManager.TriggerLevelComplete();
        }
    }
}
