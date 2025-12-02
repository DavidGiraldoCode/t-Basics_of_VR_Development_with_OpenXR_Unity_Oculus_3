using System;
using UnityEngine;

public class GameEventsManager: MonoBehaviour
{
    public static GameEventsManager Instance { get; private set;}

    // Define the evnts

    public static event Action OnGameStarted;
    public static event Action OnGameEnded;


    private void Awake()
    {
        InitSingleton();
        
    }

    private void OnEnable()
    {
        InitSingleton();
    }

    void OnDisable()
    {
        OnGameStarted = null;
        OnGameEnded = null;
    }

    private void InitSingleton() //! TODO: Study
    {
        if(Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    // Provide public methods to activate the events

    public void OnGameStart() => OnGameStarted?.Invoke();
    public void OnGameEnd() => OnGameEnded?.Invoke();

}