using UnityEngine;
[RequireComponent (typeof(Light))]
public class LightListener : MonoBehaviour
{
    private Light _light;

    void Start()
    {
        _light = GetComponent<Light>();

        //! Notice the danger of using Actions, the subscriber can modify the delegate
        // BroadcastManager.OnGameStart = null;
        //* With events, the subcribers can only listen
        //BroadcastManager.OnGameEvent = null;
    }

    void OnEnable()
    {
        BroadcastManager.OnGameStart += TurnLightGreen;
        BroadcastManager.OnGameEnd += TurnLightRed;
        BroadcastManager.OnGameAction += PrintEvent;
        BroadcastManager.OnGameEvent += ToogleIntensity;

    }

    void OnDisable()
    {
        BroadcastManager.OnGameStart -= TurnLightGreen;
        BroadcastManager.OnGameEnd -= TurnLightRed;
        BroadcastManager.OnGameAction -= PrintEvent;
        BroadcastManager.OnGameEvent -= ToogleIntensity;

    }

    private void TurnLightGreen() => _light.color = Color.green;
    private void TurnLightRed() => _light.color = Color.red;

    private void ToogleIntensity(bool state) => _light.intensity = state ? 4 : 0;

    private void PrintEvent(string a) => Debug.Log("PrintEvent: " + a.ToString());

    public void IlligalInvocation() => BroadcastManager.OnGameAction?.Invoke("This is an illigal invocation");

}
