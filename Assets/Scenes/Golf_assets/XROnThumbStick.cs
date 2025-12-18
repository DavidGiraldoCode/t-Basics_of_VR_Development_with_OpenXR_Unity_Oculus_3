using UnityEngine;
using UnityEngine.InputSystem;

public class XROnThumbStick: MonoBehaviour
{
    [SerializeField] private InputAction _inputAction;

    private void OnEnable() => _inputAction.Enable();
    private void OnDisable() => _inputAction.Disable();

    private void Update()
    {
        Vector2 inputValue = _inputAction.ReadValue<Vector2>();
        Debug.Log("inputValue: " + inputValue); 
        if(inputValue != Vector2.zero)
            EventManager.EmitThumbStickMove(inputValue.x, inputValue.y); 
    }
}