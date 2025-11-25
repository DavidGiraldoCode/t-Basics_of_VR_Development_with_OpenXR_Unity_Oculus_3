using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    private Vector3 _velocity = Vector3.zero;
    private Vector3 _position = Vector3.zero;
    private Vector3 _acceleration = Vector3.zero;
    private Vector3 _force = Vector3.zero;
    private Vector2 _previousDirection = Vector2.zero;
    [Header("Movement attributes")]
    [Range(0.1f, 10f)][SerializeField] private float _maxVelocity = 5f;
    [Range(0.1f, 50f)][SerializeField] private float _forceMultiplier = 30f;
    [Range(0f, 10f)][SerializeField] private float _dragCoefficient = 1f;

    [Tooltip("If the GameObject has a RigidBody, you can use it mass")]
    private float _mass = 1f;
    [SerializeField] private bool _useRigidBodyMass = true;
    [Header("Input Bindings")]
    [Tooltip("Defines the keys that will trigger the direction to move the object")]
    [SerializeField] private InputAction _inputAction = null;
    private void Start()
    {
        Rigidbody rb = null;
        if (_useRigidBodyMass && gameObject.TryGetComponent<Rigidbody>(out rb) == true)
            _mass = rb.mass;
        
    }

    void OnEnable() => _inputAction.Enable();  
    void OnDisable() => _inputAction.Disable();  

    private void Update()
    {
        MovePosition(transform.position);
        UpdateOrientation();
    }

    /// <summary>
    /// Finds the new position vector by applying acceleration to the velocity and adding it to the previous positions vector
    /// </summary>
    /// <param name="oldPosition"></param>
    private void MovePosition(Vector3 oldPosition)
    {
        float delta = Time.deltaTime;
        _acceleration = Vector3.zero;

        // Set force
        // Checks 
        _force = GetInputDirection();

        _acceleration += _force * _forceMultiplier;
        _previousDirection = _force;

        // Compute new velocity
        _velocity += _acceleration * delta;

        // Apply drag
        _velocity *= Mathf.Max(0, (1.0f - _dragCoefficient * delta));

        // Clamp
        if(_velocity.magnitude > _maxVelocity)
            _velocity = _velocity.normalized * _maxVelocity;

        // Find the position at T1
        _position = oldPosition + _velocity * delta;
        transform.position = _position;
    }

    /// <summary>
    /// Gets the direction from the actions bindings,
    /// In this case, WASD
    /// </summary>
    /// <returns></returns>
    private Vector3 GetInputDirection()
    {
        Vector2 direction2D = _inputAction.ReadValue<Vector2>();
        Vector3 direction3D = new Vector3(direction2D.x, 0, direction2D.y);
        return direction3D;
    }

    /// <summary>
    /// Check is the velocity is not close to zero, and updates the forward vector to match orientation
    /// </summary>
    private void UpdateOrientation()
    {
        if(_velocity.magnitude > 0.01f)
            transform.forward = _velocity.normalized;
    }

}