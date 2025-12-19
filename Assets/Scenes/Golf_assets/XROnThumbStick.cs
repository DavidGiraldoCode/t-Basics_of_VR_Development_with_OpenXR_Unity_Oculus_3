using UnityEngine;
using UnityEngine.InputSystem;

public class XROnThumbStick: MonoBehaviour
{
    [SerializeField] private InputAction _inputAction;

    private void OnEnable()
    {
        _inputAction.Enable();
        _inputAction.started += NotifyInputStarted;
        _inputAction.canceled += NotifyInputEnded;

    }
    private void OnDisable()
    {
        _inputAction.started -= NotifyInputStarted;
        _inputAction.canceled -= NotifyInputEnded;
        _inputAction.Disable();
    } 
        

    private void Update()
    {
        Vector2 inputValue = _inputAction.ReadValue<Vector2>();
        Debug.Log("inputValue: " + inputValue); 
        if(inputValue != Vector2.zero)
            EventManager.EmitThumbStickMove(inputValue.x, inputValue.y); 
    }

    private void NotifyInputStarted(InputAction.CallbackContext context) => EventManager.EmitThumbStickStart();
    private void NotifyInputEnded(InputAction.CallbackContext context) => EventManager.EmitThumbStickRelease();
}