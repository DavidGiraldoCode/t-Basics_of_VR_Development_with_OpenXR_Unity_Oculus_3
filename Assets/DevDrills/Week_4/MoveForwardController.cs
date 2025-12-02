using UnityEngine;
using UnityEngine.InputSystem;

/*
45 min: Manage to:
- Set Singleton manager
- Created simple control to move forward with velocity
- Reister to the events

Extra time: 10min
- Create a button to start the game and enable the controllers
- Added a GUI manager to display a Game over text
*/

public class MoveForwardController : MonoBehaviour
{
    // Settings
    [Range(0, 50)][SerializeField] private float _maxForce = 30;
    [Range(0, 50)][SerializeField] private float _maxVelocity = 30;
    [Range(0, 10)][SerializeField] private float _dragFactor = 5;

    private float _gass = 0f;

    // Inputs
    [SerializeField] private InputAction _inputAction = null;


    // Vectors
    private Vector3 _velocity = Vector3.zero;
    private Vector3 _acceleration = Vector3.zero;
    private Vector3 _force = Vector3.zero;
    private Vector3 _position = Vector3.zero;

    //
    void OnEnable()
    {
        GameEventsManager.OnGameStarted += ActivateInputs;
        GameEventsManager.OnGameEnded += DeactivateInputs;
    }
    void OnDisable()
    {
        GameEventsManager.OnGameStarted -= ActivateInputs;
        GameEventsManager.OnGameEnded -= DeactivateInputs;
    }

    void Start()
    {
        _position = transform.position;
    }

    void Update()
    {
        // Get inputs
        GetInput();
        Debug.Log(_gass);
        // Compute velocity
        Accelerate();
        transform.position = _position;
    }

    private void GetInput()
    {
        if (_inputAction.IsPressed())
        {
            float y = _inputAction.ReadValue<Vector2>().y;
            _gass = y > 0 ? 1 : 0;
        }
        else
            _gass = 0f;
    }

    private void ActivateInputs() => _inputAction.Enable();

    private void DeactivateInputs() => _inputAction.Disable();

    private void Accelerate()
    {
        // Set forces
        float delta = Time.deltaTime;
        _acceleration = Vector3.zero;
        _force = transform.forward * _maxForce * _gass;
        // Compute acceleration
        float mass = 1;
        _acceleration = (_force / mass);
        // Compute velocity
        _velocity = _velocity + _acceleration * delta;
        // Clamp it
        if (_velocity.magnitude > _maxVelocity)
            _velocity = _velocity.normalized * _maxVelocity;
        // Apply friction
        _velocity = _velocity * Mathf.Max(0f, 1f - _dragFactor * delta);
        // Compute position in t+1

        _position = _position + _velocity * delta;
    }


}