using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance {get; private set;}
    // Define the events
    public static event Action<float, float> OnThumbStickMoved;
    private void Awake()
    {   
        if(Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    // Expose ways to call the events
    public static void EmitThumbStickMove(float x, float y) => OnThumbStickMoved?.Invoke(x,y);
}
