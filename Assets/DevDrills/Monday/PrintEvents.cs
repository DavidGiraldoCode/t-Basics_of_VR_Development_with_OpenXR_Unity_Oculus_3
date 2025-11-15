using UnityEngine;

public class PrintEvents : MonoBehaviour
{
    [SerializeField] //TODO Change to Singleton
    private GameManager _gameManager = null;
    public void PrintEvent()
    {
        Debug.Log("Event!");
    }

    //? Note that the event handdler needs to the type of the event as a parameter
    private void PrintDifficultyEvent(GameDifficulty gameDifficulty)
    {
        Debug.Log("Difficulty changed: " + gameDifficulty);
    }

    #region Unity MonoBehaviour
    void Awake()
    {
        // TODO Make Singleton
        _gameManager = FindFirstObjectByType<GameManager>();
    }
    void Start()
    {
        if(_gameManager)
        {
            Debug.Log("Found Manager");
            _gameManager.gameDifficultyChanged.AddListener(PrintDifficultyEvent);
        }
        
    }
    void Update()
    {
        
    }
    #endregion
}
