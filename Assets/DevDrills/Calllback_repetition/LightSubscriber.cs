using UnityEngine;

public class LightSubscriber : MonoBehaviour
{
    [SerializeField] private ButtonPublisher _buttonPublisher;
    [SerializeField] private Light _light;
    //* Subscribe to the event
    private void OnEnable()
    {
        if(_buttonPublisher)
            _buttonPublisher.OnButtonPressed += OnButtonPressed;
    }
    //* Unsubscribe to the event
    private void OnDisable()
    {
        if(_buttonPublisher)
            _buttonPublisher.OnButtonPressed -= OnButtonPressed;
    }
    //* Define an event handler (callback)

    private void OnButtonPressed()
    {
        // Something happends
        Debug.Log("Got event from publisher");
        if(_light)
            _light.enabled = !_light.enabled;
    }
}
