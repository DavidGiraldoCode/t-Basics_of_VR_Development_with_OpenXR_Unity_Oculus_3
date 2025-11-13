using UnityEngine;
using System;
using Broadcaster;
public class BroadcatsController : MonoBehaviour
{
    public static BroadcatsController Instance { get; private set; }

    //* Actions groups for GamePlay
    public static Action GamePlayStarted;
    public static Action GamePlayEnded;
    public static Action GamePlayPaused;

    //* Provide menthods to trigger the events
    public static void GamePlayStart()
    {
        GamePlayStarted?.Invoke();
    }
    public static void GamePlayEnd()
    {
        GamePlayEnded?.Invoke();
    }
    public static void GamePlayPause()
    {
        GamePlayPaused?.Invoke();
    }

    private void InitSingleton()
    {
        if (Instance == null || Instance != this)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }
    private void Awake()
    {
        InitSingleton();
    }
    private void OnEnable()
    {
        InitSingleton();
    }

     private void OnDisable()
    {
        //? Here, we are clearing the event's invocation list once this MonoBehaviour is off load of the scene.
        BroadcastMessenger.GamePlayStarted = null;
        BroadcastMessenger.GamePlayEnded = null;
        BroadcastMessenger.GamePlayPaused = null;
    }


}
