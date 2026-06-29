using UnityEngine;

public class ClueObject : MonoBehaviour, IInteractable
{
    [SerializeField] private string _clueIntent = "[Placeholder] describe what this clue teaches";
    [SerializeField] private string _examinedPrompt = "Re-examine inscription";
    [SerializeField] private AudioClip _examineSound;

    private AudioSource _audioSource;
    private bool _examined;

    public string InteractPrompt => _examined ? _examinedPrompt : "Examine inscription";

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Interact()
    {
        if (!_examined)
        {
            _examined = true;
            if (_audioSource != null && _examineSound != null)
                _audioSource.PlayOneShot(_examineSound);
#if UNITY_EDITOR
            Debug.Log($"[ClueObject] Examined: {gameObject}");
#endif
        }
    }
}
