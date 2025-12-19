using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance {get; private set;}
    // Define the events
    public static event Action<float, float> OnThumbStickMoved;
    public static event Action OnThumbStickRealeased;
    public static event Action OnThumbStickStarted;
    public static event Action OnToggleVirtualProxyVisibility;
    
    private void Awake()
    {   
        if(Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    // Expose ways to call the events
    public static void EmitThumbStickMove(float x, float y) => OnThumbStickMoved?.Invoke(x,y);
    public static void EmitThumbStickRelease() => OnThumbStickRealeased?.Invoke();
    public static void  EmitThumbStickStart() => OnThumbStickStarted?.Invoke();
    public static void EmitToggleVirtualProxyVisibility() => OnToggleVirtualProxyVisibility?.Invoke();
}
