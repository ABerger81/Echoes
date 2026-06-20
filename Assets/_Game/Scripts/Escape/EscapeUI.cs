using System.Collections;
using UnityEngine;
using TMPro;

// Shows the escape prompt when the Major Treasure is picked up, then hides it.
// Follows the same event-subscription pattern as InteractionUI.
public class EscapeUI : MonoBehaviour
{
    [SerializeField] private TMP_Text escapeText;

    // How long the prompt stays visible before disappearing.
    [SerializeField] private float displayDuration = 4f;

    private void Awake()
    {
        GameManager.OnEscapeTriggered += ShowEscapePrompt;
        escapeText.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameManager.OnEscapeTriggered -= ShowEscapePrompt;
    }

    private void ShowEscapePrompt()
    {
        escapeText.text = "ESCAPE! Find the exit.";
        escapeText.gameObject.SetActive(true);
        StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        escapeText.gameObject.SetActive(false);
    }
}
