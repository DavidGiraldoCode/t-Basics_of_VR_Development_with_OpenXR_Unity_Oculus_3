using UnityEngine;
using UnityEngine.InputSystem;

public class SteeringController: MonoBehaviour
{
// 70 min
    [SerializeField] private InputAction _inputAction;
    [Range(0,45)][SerializeField] private float _maxAnglee = 30f;
    
    private Matrix4x4 _rotationY = Matrix4x4.identity;

    [Range(0,50)][SerializeField] private float _maxForce = 30f;
    [Range(0,50)][SerializeField] private float _maxVelocity = 10f;
    [Range(0,10)][SerializeField] private float _dragFactor = 1f;

    private Vector3 _acceleration;
    private Vector3 _velocity;
    private Vector3 _steeringDirection;
    
    private float _steeringAnglee = 0f;
    private float _gass = 0f;
    
    private void Update() 
    {
        ControlDirection();  
        RotateY();
        Move();  
        DebuggingVectors();
    }

    private void OnEnable() => _inputAction.Enable();
    private void OnDisable() => _inputAction.Disable();

    private float DTR(float degree) => degree * ((2f*Mathf.PI)/360f);

    private void ControlDirection()
    {   
        Vector2 controlVector = _inputAction.ReadValue<Vector2>();

        float sign = controlVector.x > 0 ? -1 : controlVector.x < 0 ? 1 : 0; // Coordinates along X
        _gass = controlVector.y > 0 ? 1 : controlVector.y < 0 ? -1 : 0; // This is because pressing A+W gives y = 0.74
        _steeringAnglee = _maxAnglee * sign;
        Debug.Log("sign: " + sign);
        Debug.Log("gass: " + _gass);
        Debug.Log("_steeringAnglee: " + _steeringAnglee);
    }


    private void Move()
    {
        // Scale by the frame time
        float delta = Time.deltaTime;

        // Set forces for acceleration
        float mass = 1f;
        _acceleration = (_steeringDirection * _maxForce * _gass) / mass;

        // Compute new velocity
        _velocity = _velocity + _acceleration * delta;

        // Add drag force
        //! Re-study
        _velocity = _velocity * Mathf.Max(0, (1f - _dragFactor * delta));

        // Clamp velocity
        if(_velocity.magnitude > _maxVelocity)
            _velocity = _velocity.normalized * _maxVelocity;

        // Find new position at t+1
        transform.position = transform.position + _velocity * delta;
    }

    private void RotateY()
    {
        Vector4 i = new Vector4(    Mathf.Cos(DTR(_steeringAnglee)), 0, Mathf.Sin(DTR(_steeringAnglee)), 0);
        Vector4 j = new Vector4(    0, 1, 0, 0);
        Vector4 k = new Vector4(    -Mathf.Sin(DTR(_steeringAnglee)), 0, Mathf.Cos(DTR(_steeringAnglee)), 0);
        Vector4 w = new Vector4(    0, 0, 0, 1);

        _rotationY = new Matrix4x4(i,j,k,w);
        _steeringDirection = _rotationY.MultiplyPoint(transform.forward).normalized;
    }

    private void DebuggingVectors()
    {
        Vector3 pos = transform.position;
        Vector3 forward = pos + transform.forward * 1f;
        //Debug.DrawLine(pos,forward,Color.blue);
        Debug.DrawLine(pos, pos + _steeringDirection, Color.magenta);

    }
}