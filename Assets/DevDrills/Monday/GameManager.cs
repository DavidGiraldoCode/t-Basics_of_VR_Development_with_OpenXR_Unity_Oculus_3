using UnityEngine;
using UnityEngine.Events;

public enum GameState
{
    NONE,
    ON_GOING,
    WIN,
    LOSE
}
public enum GameDifficulty
{
    EASY,
    HARD
}
// TODO: A GameManager only exist once, thus it should be a Sigleton class
public class GameManager : MonoBehaviour
{
    [SerializeField] // 
    [Tooltip ("It only accepts positive signed values. Changing this at runtime will not trigger events")]
    private uint score = 0;

    [SerializeField]
    private  GameState gameState = GameState.NONE;

    [SerializeField]
    private GameDifficulty gameDifficulty = GameDifficulty.EASY;

    #region Game events
    //* Unity Event declaration
    public UnityEvent<uint> OnScoreUpdated;
    public UnityEvent<GameState> gameStateChanged;
    public UnityEvent<GameDifficulty> gameDifficultyChanged;
    
    #endregion

    // Properties and accessros
    public uint Score
    {
        get { return score; }
        set 
        {
            score = value; 
            //* Event invocation
            OnScoreUpdated.Invoke(score);
        }
    }

    public GameDifficulty Difficulty
    {
        get => gameDifficulty;
        set
        {
            gameDifficulty = value; 
            gameDifficultyChanged.Invoke(gameDifficulty);
        } 
    }

    #region Monobehaviour
    void Awake()
    {
        //* Events initiallization
        //? Note that initializing the event will erase all the manual addition of event listeners done in the editor
        //scoreChanged = new UnityEvent<uint>();

        gameDifficultyChanged = new UnityEvent<GameDifficulty>();
    }
    void Start()
    {
        Debug.Log("Hello Manager!");
    }

    // Update is called once per frame
    void Update()
    {
        //! This works to validate the event BUT, the Update function executes extremely fast,
        // Many times per second, so the condition holds for many frames, causing multiple event invocations.
        int seconds = (int)Time.realtimeSinceStartup;
        if(seconds % 5 == 0)
        {
            Score+= 5;
            Difficulty = GameDifficulty.HARD;
            
        }
    }
    #endregion
}
