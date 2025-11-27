using System;
using System.Collections.Generic;
using UnityEngine;

public class BroadcastManager: MonoBehaviour
{
    //TODO Singleton class
    public static BroadcastManager Instance {get; private set;}

    //* =========================================== 1. Declare the events, but there is a mistake, read the note
    public static Action OnGameStart;
    public static Action OnGameEnd;
    public static Action<string> OnGameAction;
    public static event Action<bool> OnGameEvent; //* THIS IS THE RIGHT WAY OF DECLARING AN EVENT USING ACTIONS

// TODO store events in a dictionary
    private Dictionary<string, Action> Actions;

    //!NOTE:
    /**
    * The `event` key word protects the delegate from mutations outside the enclosing class.
    * For example, a Light or GUI GameObject could set to `null` or invoke any of the actions that
    * are not protected as events.
    * When an event is defined, only the class thats owns it can invoke it, thats why we
    * offer public method to enable external classes to invoke the events
    */

    private void Awake()
    {
        if(Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this; 

//TODO:
        Actions["Start"] = OnGameStart;  
        Actions["End"] = OnGameEnd;
    }

    //* =========================================== 2.  Offer a way to trigger the actions
    public static void BroadcastGameStart() => OnGameStart?.Invoke();
    public static void BroadcastGameOver() => OnGameEnd?.Invoke();
    public static void BroadcastGameAction(string msn) => OnGameAction?.Invoke(msn);
    public static void BroadcastGameEvent(bool state) => OnGameEvent?.Invoke(state);

    //* =========================================== 3.  Clear the invocation list

    void OnDisable()
    {
        OnGameStart = null;
        OnGameEnd   = null;
        OnGameAction = null;
        OnGameEvent = null;
    }

}
