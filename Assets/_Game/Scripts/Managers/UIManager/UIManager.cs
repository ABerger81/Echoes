using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // ── HUD ───────────────────────────────────────────────────────────────

    // Visible during gameplay — hides when an end panel appears.
    [SerializeField] private TMP_Text scoreText;

    // ── End Screens ────────────────────────────────────────────────────────

    [SerializeField] private GameObject winPanel;
    [SerializeField] private TMP_Text winScoreText;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text gameOverScoreText;

    // ── Audio ──────────────────────────────────────────────────────────────
    [SerializeField] private AudioClip _restartClip;
    [SerializeField] private AudioSource _uiAudioSource;

    // ── State ──────────────────────────────────────────────────────────────

    // Cached so panels can display the final score when they appear.
    private int _currentScore;

    // ── Lifecycle ──────────────────────────────────────────────────────────

    private void Awake()
    {
        GameManager.OnScoreChanged += UpdateScore;
        GameManager.OnLevelComplete += ShowWinPanel;
        GameManager.OnTimerExpired += ShowGameOverPanel;

        winPanel.SetActive(false);
        gameOverPanel.SetActive(false);

        UpdateScore(0);
    }

    private void OnDestroy()
    {
        GameManager.OnScoreChanged -= UpdateScore;
        GameManager.OnLevelComplete -= ShowWinPanel;
        GameManager.OnTimerExpired -= ShowGameOverPanel;
    }

    // ── Handlers ───────────────────────────────────────────────────────────

    private void UpdateScore(int score)
    {
        _currentScore = score;
        scoreText.text = $"Score: {score}";
    }

    private void ShowWinPanel()
    {
        scoreText.gameObject.SetActive(false);
        winScoreText.text = $"Score: {_currentScore}";
        winPanel.SetActive(true);
        ReleaseCursor();
    }

    private void ShowGameOverPanel()
    {
        scoreText.gameObject.SetActive(false);
        gameOverScoreText.text = $"Score: {_currentScore}";
        gameOverPanel.SetActive(true);
        ReleaseCursor();
    }

    private void ReleaseCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // ── Restart ────────────────────────────────────────────────────────────

    public void Restart()
    {
        StartCoroutine(RestartAfterSound());
    }

    private IEnumerator RestartAfterSound()
    {
        if (_uiAudioSource != null && _restartClip != null)
        {
            _uiAudioSource.PlayOneShot(_restartClip);
            yield return new WaitForSeconds(_restartClip.length);
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
