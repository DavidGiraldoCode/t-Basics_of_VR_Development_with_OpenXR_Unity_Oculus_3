using UnityEngine;
using UnityEngine.InputSystem;

public class VelocityController : MonoBehaviour
{
    [Range(0.0f, 5f)]
    [SerializeField] private float _newVelocityMagnitud = 0f;
    private float _velocityMagnitud = 0f;

    [Range(0.0f, 5f)]
    [SerializeField] private float _newSpeed = 0f;
    private float _speed = 0f;


    private Vector3 _velocity;
    //Is acceleration a vector?
    private Vector3 _acceleration;
    private Vector3 _drag;
    [Range (0f, 1f)]
    [SerializeField] private float _dragFactor = 0f;

    //* Input Actions
    [SerializeField] private InputAction _moveInputAction;
    private bool _isVelocityUpdated = false;
    //* Input Actions are not Enbaled by default
    void OnEnable() =>_moveInputAction.Enable();
    void OnDisable() =>_moveInputAction.Disable();

    void Start()
    {
        _velocity = transform.forward;
        //_moveInputAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        if (!Mathf.Approximately(0.01f, Mathf.Abs(_newSpeed - _speed)))
            _speed = _newSpeed;

        if (!Mathf.Approximately(0.01f, Mathf.Abs(_newVelocityMagnitud - _velocityMagnitud)))
            _velocityMagnitud = _newVelocityMagnitud;

        if (_moveInputAction.inProgress)
        {
            Vector2 move = _moveInputAction.ReadValue<Vector2>();
            Vector2 vel2D = new Vector2(_velocity.normalized.x, _velocity.normalized.z);

            if (Vector2.Dot(move, vel2D) != 1)
            {
                //! INCORRECT: Here you override the velocity directly by the input
                //! instead of using the input as the force that will be added
                //! This is against the idea of velocity = addition of forces, and becomes
                //! just velocity = input.
                _velocity.x = move.x;
                _velocity.z = move.y;
                _speed = _newSpeed;
            }
        }


        // Set Forward as the velocity
        transform.forward = _velocity.normalized;


        // Accelerating --------------------------------------------
        //! This creates a boomb, and need a los of clampling.
        _speed *= _speed; // Exponential acceleration
        
        if(_speed > 50f)
            _speed = 50f;

        _acceleration = _velocity.normalized * _speed;

        // Adding drag
        //! You forgot to negate the direction.
        //! Heres you are just increasing it
        //! The idea of drag is to slow down
        _drag = _velocity.normalized * _dragFactor;
        //* the correct from is velocity *= (1 - drag * dt)
        //* OR velocity -= velocity.normalized * drag * dt;

         
        _velocity += _acceleration + _drag;

        // Clamp velocity
        if (_velocity.magnitude > 5f)
            _velocity = _velocity.normalized * 5f;

        transform.position += _velocity * _velocityMagnitud * Time.deltaTime;

        //! The correct pipeline is Force → Acceleration → Velocity → Position
        /**
        You wrongly treated:
        _speed as acceleration, which neeeds to be a vector
        _velocityMagnitud as a displacement scale
        _velocity as both direction + input
        _acceleration as simply the velocity * the scalar value in _speed

        So the model is everything but correct

        */
        //! Check ForceMovement for the right answer

        Debugging();
    }

    private void Debugging()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward * 1f, Color.blue);
        Debug.DrawLine(transform.position, transform.position + transform.forward * _speed, Color.yellow);
        Debug.Log("Velocity magnitud: " + _velocity.magnitude);
        //Debug.Log("Input Vector: " + _moveInputAction.ReadValue<Vector2>());
    }
}