using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SteerControllerTwo: MonoBehaviour
{
    // Inputs
    [SerializeField] InputAction _inputAction;

    // Settings
    [Range(0,50)][SerializeField] private float _maxForce = 20;
    [Range(0,50)][SerializeField] private float _rotationSpeed = 20f;
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

        // You initilize the steering direction as the forward vector of te object.
        _steeringDirection = transform.forward;
    }

    void Update()
    {

        // The General algorithm is before:

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

    /// <summary>
    /// This method translates the input from the keyboard in instructions to define the `_sign` of the rotation anglee
    /// Is also takes the `A` and `D` keys as `_gass` and break. 
    /// </summary>
    private void GetSteeringInstructions()
    {
        Vector2 controlVec = _inputAction.ReadValue<Vector2>();
        // (A) |<--------- 30 degrees | -30 degress ---------->| (D)
        _sign = controlVec.x < 0 ? 1 : controlVec.x > 0 ? -1 : 0;

        // If the objects needs to accelerate or not
        _gass = controlVec.y < 0 ? -1 : controlVec.y > 0 ? 1 : 0;

        // This removes the mirrowing effect when turning backwards
        _sign *= _gass;

        // Right or left
        _steeringAngle = _maxSteeringAngle * _sign;
    }

    private float DTR(float degree) => degree * ((2f * Mathf.PI) / 360f);

    float d = 0f;
    private void RotateOnY()
    {   

        // By storig the delta in time bwteen freame, you can scale down the size of the radian that controls the rotation.
        float dt = Time.deltaTime;

        // All of this is not really working, as it 
        // Here every time the _sing is 0, meaning, none of the direction keys (A, D) are being press
        // the accumulation of the degrees in d will reset.
        //d = _sign == 0f ? 0f : d + _steeringAngle * dt;
        
        // You are keeping track of how much the steering anglee has change since the user pressed the key (A,D)
        // And if it excedess the max angle, you stop rotation.
        //if((d*d) > _maxSteeringAngle * _maxSteeringAngle)
        //    return;

        //? Notes on the intuition.
        // There is no need to set a limit to how much the object rotates.
        // From an Isometric view (TOP), the pressing W+A you can see how the obejct draw a circle, and the radious of that
        // circle is determined by the "_steeringAngle". The key is the update of the steeringDirection.
        // This match with a controller that moves the wheels until a 30 degress gives the rigt illusion of steering.

        _steeringAngle *= _rotationSpeed;
        // Build basis vectors                  x                y                      z               w
        Vector4 i = new Vector4( Mathf.Cos(DTR(_steeringAngle * dt)),   0,  Mathf.Sin(DTR(_steeringAngle * dt)),   0 );    
        Vector4 j = new Vector4( 0                             ,        1,                               0,        0 );   
        Vector4 k = new Vector4( -Mathf.Sin(DTR(_steeringAngle * dt)),  0,  Mathf.Cos(DTR(_steeringAngle * dt)),   0 );    
        Vector4 w = new Vector4( 0,                                     0,                              0,         1 );   

        // Build the matrix and apply transformation
        _rotationY = new Matrix4x4(i,j,k,w);

        // When apply the transformation to the steeringDirection, you are
        // transforming an already transformed vector. That why it will create the perception of spining
        _steeringDirection = _rotationY.MultiplyPoint(_steeringDirection).normalized; 
        // So far the transformation will continue to happen. To control how much the object can rotate,
        // You need to set a limit
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
        //if(_velocity.magnitude > 0.01f ) transform.forward = _velocity.normalized;
        // By updating the forward vector using the velocity, this means that only when the
        // object if moving, the forward vecto will update. This causes the object to only update
        // drastically if the steering direction has been changed while the obejct was stabding still
                
        // When using the steeing direction, it does not matter if the object is still or
        // moving, it will update the orientation.
        transform.forward = _steeringDirection.normalized;
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

    //? Notes on the camera movement:
    // It requieres a more complex system of stabilization, follow and look at.
    // The naive approach of attaching it to the object transform matrix create strong chnages in direction
    // that makes diffcult to control.

}