using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SteerControllerTwo: MonoBehaviour
{
    // Inputs
    [SerializeField] InputAction _inputAction;

    // Settings
    [Range(0,50)][SerializeField] private float _maxForce = 20;
    [Range(0,50)][SerializeField] private float _maxVelocity = 20;
    [Range(0,10)][SerializeField] private float _dragFactor = 5;
    [Range(0,360)][SerializeField] private int _maxSteeringAngle = 30;
    private float _steeringAngle = 0;
    private float _sign = 0;
    private float _gass = 0;

    // Vectors and movement attributes

    private Vector3 _acceleration = Vector3.zero;
    private Vector3 _velocity = Vector3.zero;
    private Vector3 _steeringDirection = Vector3.zero;
    private Vector3 _position = Vector3.zero;

    // Rotation Matrix
    private Matrix4x4 _rotationY = Matrix4x4.identity;

    // Methods

    private void OnEnable() => _inputAction.Enable();
    private void OnDisable() => _inputAction.Disable();

    private void Start()
    {
        _position = transform.position;
    }

    void Update()
    {

        // Get the input
        GetSteeringInstructions();
        // Update rotation
        RotateOnY();
        // Accelerate
        ApplyMovement();
        // Update forward

        transform.position = _position;
        UpdateOrientation();


        DebugginVectors();
    }

    private void GetSteeringInstructions()
    {
        Vector2 controlVec = _inputAction.ReadValue<Vector2>();
        // (A) |<--------- 30 degrees | -30 degress ---------->| (D)
        _sign = controlVec.x < 0 ? 1 : controlVec.x > 0 ? -1 : 0;

        // If the objects needs to accelerate or not
        _gass = controlVec.y < 0 ? -1 : controlVec.y > 0 ? 1 : 0;

        // Right or left
        _steeringAngle = _maxSteeringAngle * _sign;
    }

    private float DTR(float degree) => degree * ((2f * Mathf.PI) / 360f);
    private void RotateOnY()
    {
        // Build basis vectors                  x                y                      z               w
        Vector4 i = new Vector4( Mathf.Cos(DTR(_steeringAngle)), 0,  Mathf.Sin(DTR(_steeringAngle)),    0 );    
        Vector4 j = new Vector4( 0                             , 1,                               0,    0 );   
        Vector4 k = new Vector4( -Mathf.Sin(DTR(_steeringAngle)), 0,  Mathf.Cos(DTR(_steeringAngle)),   0 );    
        Vector4 w = new Vector4( 0,0,0,1);   

        // Build the matrix and apply transformation
        _rotationY = new Matrix4x4(i,j,k,w);
        _steeringDirection = _rotationY.MultiplyPoint(transform.forward); 
    }

    private void ApplyMovement()
    {
        float delta = Time.deltaTime;
        // Set forces and direction
        _acceleration = Vector3.zero;
        float mass = 1f;
        _acceleration = (_steeringDirection * _maxForce) / mass * _gass;

        // Compute velocity
        _velocity = _velocity + _acceleration * delta;

        // Clamp
        if(Vector3.Dot(_velocity, _velocity) > (_maxVelocity * _maxVelocity))
            _velocity = _velocity.normalized * _maxVelocity;

        // Apply drag
        //_velocity = _velocity - ((-1f * _velocity.normalized) * _dragFactor) * delta;
        _velocity = _velocity * Mathf.Max(0, (1f - _dragFactor * delta)); //! STUDY

        // Find new poition at t+1
        _position = _position + _velocity * delta;
    }

    private void UpdateOrientation()
    {
        if(_velocity.magnitude > 0.01f )
            transform.forward = _velocity.normalized;
    }


    private void DebugginVectors()
    {
        Vector3 pos = transform.position;
        Vector3 fwd = transform.forward;
              
        //Debug.Log(_inputAction.ReadValue<Vector2>());  
        Debug.Log("gass: " + _gass);
        Debug.Log("sign: " + _sign);
        Debug.DrawLine(pos, pos + _steeringDirection.normalized * _maxForce,Color.magenta);
    }

    

}