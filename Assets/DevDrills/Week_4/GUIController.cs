using UnityEngine;

public class GUIController : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverTextGUI = null;

    void Start()
    {
        if (_gameOverTextGUI != null)
            _gameOverTextGUI.SetActive(false);
    }
    void OnEnable() => GameEventsManager.OnGameEnded += ShowGameOverMessage;
    void OnDisable() => GameEventsManager.OnGameEnded -= ShowGameOverMessage;

    private void ShowGameOverMessage()
    {
        if (_gameOverTextGUI != null)
            _gameOverTextGUI.SetActive(true);

    }
    
}
