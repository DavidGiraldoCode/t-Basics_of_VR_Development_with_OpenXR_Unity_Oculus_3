using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputEventManagerW8 : MonoBehaviour
{
    public static InputEventManagerW8 Instance { get; private set; }

    [SerializeField] private InputAction _inputActionWasd;

    public static event Action<Vector2> OnNavigationInputActivated;
    public static event Action OnNavigationInputCancelled;

    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        _inputActionWasd.Enable();
        _inputActionWasd.performed += OnWasdInputActivated;
        _inputActionWasd.canceled += OnWasdInputCancelled;
    }
    private void OnDisable()
    {
        _inputActionWasd.performed -= OnWasdInputActivated;
        _inputActionWasd.canceled -= OnWasdInputCancelled;

        _inputActionWasd.Disable();
        OnNavigationInputActivated = null;
    }
    private void Init()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    #region WASD Inputs Public Events

    private void OnWasdInputActivated(InputAction.CallbackContext context)
    {
        Vector2 coordinates = context.ReadValue<Vector2>();
        OnNavigationInputActivated?.Invoke(coordinates);
    }

    private void OnWasdInputCancelled(InputAction.CallbackContext context)
    {
        OnNavigationInputCancelled?.Invoke();
    }

    #endregion
}