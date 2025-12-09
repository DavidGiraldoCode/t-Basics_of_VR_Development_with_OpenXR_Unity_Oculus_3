using UnityEngine;
using UnityEngine.InputSystem;


public class MoveController: MonoBehaviour
{
    // Input instance
    [SerializeField] private InputAction _inputAction;
    // Movement settings
    [Range(5, 30)][SerializeField] private float _maxForce = 30f;
    [Range(5, 50)][SerializeField] private float _maxVelocity = 10f;
    [Range(0, 10)][SerializeField] private float _dragFactor = 5f;
    [Range(1,90)][SerializeField]  private float _maxDegree = 30f;
    [Range(1,90)][SerializeField]  private float _rotationSpeed = 30f;

    private float _gass = 0; // the gass pedal
    private float _sign = 0; // Left of right
    // Vectors
    private Vector3 _acceleration = Vector3.zero;
    private Vector3 _force = Vector3.zero;
    private Vector3 _velocity = Vector3.zero;
    private Vector3 _orientation = Vector3.zero;
    private Vector3 _position = Vector3.zero;

    // Rotation Matrix
    private Matrix4x4 _rotationY = Matrix4x4.identity;
    private float _rotationDegree = 0f;
    private float _rotationAngle = 0f;

    // Usign and freeing resouces from memory
    private void OnEnable() => _inputAction.Enable();
    private void OnDisable() => _inputAction.Disable();

    private void Start()
    {
        _position = transform.position;
        _orientation = transform.forward;
    }

    private void GetUserInput()
    {
        Vector2 input = _inputAction.ReadValue<Vector2>();
        float x = input.x;
        float y = input.y;

        _gass = y > 0 ? 1 : y < 0 ? -1 : 0; // Forward, or negate vector
        _sign = x > 0 ? -1 : x < 0 ? 1 : 0; // Right (negative rotation), or left (positive)

        _sign *= _gass; // mirror effect
        _rotationAngle = _sign * _maxDegree;

    }

    private void ComputeDirection()
    {
        float dt = Time.deltaTime;
        //Vector3 _oldOrientation = _orientation;
        // Compute rotation Matrix around Y axis.
        // Compute the coordinate where the basis vector will land after the given transformation
        _rotationAngle *= _rotationSpeed * dt; // Scale by te speed and time
        float theta = _rotationAngle * (2f * Mathf.PI / 360f);
                                
        Vector4 i = new Vector4(    Mathf.Cos(theta),   0,  Mathf.Sin(theta),    0);
        Vector4 j = new Vector4(    0,                  1,                 0,    0);
        Vector4 k = new Vector4(    -Mathf.Sin(theta),  0,  Mathf.Cos(theta),    0);
        Vector4 w = new Vector4(    0,                  0,                 0,    1);

        // Insanciate the Matix
        _rotationY = new Matrix4x4(i,j,k,w);

        // Apply the transformation
        _orientation = _rotationY.MultiplyPoint(_orientation).normalized;

        //if(Mathf.Abs(_rotationDegree) > 360)
        //    _rotationDegree = 0f;
    }

    private void Move()
    {
        float dt = Time.deltaTime;
        // Set forces
        _acceleration = Vector3.zero;
        _force = _orientation * _maxForce * _gass;

        // Compute acceleration
        float mass = 1f;
        _acceleration = _force / mass;
        
        // Compute velocity
        _velocity += _acceleration * dt;

        // Clamp velocity
        if(Vector3.Dot(_velocity, _velocity) > (_maxVelocity * _maxVelocity))
            _velocity = _velocity.normalized * _maxVelocity;

        // Apply drag
        _velocity = _velocity * Mathf.Max(0f, (1f - _dragFactor * dt));

        // Find new position at t+1
        _position += _velocity * dt;
    }


    private void Update()
    {
        // Get the inputs
        GetUserInput();

        // Compute direction
        ComputeDirection();
        
        // Compute forces
        Move();

        // Update position
        transform.position = _position;

        // Update Orientation
        //if(_velocity.magnitude > 0.01f)
        //    transform.forward = _velocity.normalized;
        transform.forward = _orientation.normalized;

        Debuggers();

    }

    private void Debuggers()
    {
        Debug.Log("_gass: " + _gass);
        Debug.DrawLine(_position, _position + _orientation * _maxVelocity, Color.magenta);

    }
}