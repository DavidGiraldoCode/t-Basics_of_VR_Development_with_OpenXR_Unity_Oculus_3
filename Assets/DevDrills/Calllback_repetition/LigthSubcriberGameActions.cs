using UnityEngine;
using Broadcaster;
public class LigthSubcriberGameActions : MonoBehaviour
{
    [SerializeField] private Light _light;
    private void Awake() 
    {
        _light = GetComponent<Light>();
    }
    //* Handler
    private void TurnLightRed()
    {
        _light.color = Color.red;
    }
    private void TurnLightGreen()
    {
        _light.color = Color.green;
    }
    //* Subscribe to the messages

    private void OnEnable()
    {
      //BroadcatsController.GamePlayEnded += TurnLightRed;  
      //BroadcatsController.GamePlayStarted += TurnLightGreen;  
      BroadcastMessenger.GamePlayEnded += TurnLightRed;  
      BroadcastMessenger.GamePlayStarted += TurnLightGreen;  
    }
    //* Unsubscribe to the messages
    private void OnDisable()
    {
      //BroadcatsController.GamePlayEnded -= TurnLightRed;  
      //BroadcatsController.GamePlayStarted -= TurnLightGreen;  
      BroadcastMessenger.GamePlayEnded -= TurnLightRed;  
      BroadcastMessenger.GamePlayStarted -= TurnLightGreen;  
    }
}
