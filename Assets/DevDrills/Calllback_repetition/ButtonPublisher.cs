using Unity.VisualScripting;
using UnityEngine;
using Broadcaster;
public class ButtonPublisher : MonoBehaviour
{
    //* Create the delegate and an intance of it as event
    public delegate void ButtonPressed();
    public event ButtonPressed OnButtonPressed;

    //* A machanism to trigger it from the button
    public void TriggerEvent()
    {
        OnButtonPressed?.Invoke();
    }

    //* Trigger Gamestart
    public void GameStart()
    {
        //if(BroadcatsController.Instance != null)
        //BroadcatsController.GamePlayStart();
        BroadcastMessenger.GamePlayStart();
    }
    public void GameEnd()
    {
        BroadcastMessenger.GamePlayEnd();
    }
}
