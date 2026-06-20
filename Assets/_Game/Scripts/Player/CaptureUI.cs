using UnityEngine;
using TMPro;

// Shows the capture message when the escape timer runs out.
// Follows the same event-subscription pattern as EscapeUI and InteractionUI.
public class CaptureUI : MonoBehaviour
{
    [SerializeField] private TMP_Text captureText;

    private void Awake()
    {
        GameManager.OnTimerExpired += ShowCaptureMessage;

        // Hidden by default — only shown on capture.
        captureText.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameManager.OnTimerExpired -= ShowCaptureMessage;
    }

    private void ShowCaptureMessage()
    {
        // Message stays on screen — no auto-hide.
        // Milestone 6 will replace this with a proper Game Over screen.
        captureText.text = "You were captured.";
        captureText.gameObject.SetActive(true);
    }
}
