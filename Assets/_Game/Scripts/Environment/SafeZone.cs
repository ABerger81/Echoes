using UnityEngine;

public class SafeZone : MonoBehaviour
{
    [SerializeField] private HeartbeatManager heartbeatManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) heartbeatManager.EnterSafeZone();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) heartbeatManager.ExitSafeZone();
    }
}
