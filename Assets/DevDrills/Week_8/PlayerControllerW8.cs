using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControllerW8: MonoBehaviour
{
    // Movement parammeters
    [Range(1, 50)]
    [SerializeField] private float _maxVelocity = 10f;
    [Range(1, 50)]
    [SerializeField] private float _maxForce = 10f;
    [Range(0, 1)]
    [SerializeField] private float _dragFactor = 1f;
    [Range(0, 180)]
    [SerializeField] private float _rotationAngle = 15f;
    [Range(0, 180)]
    [SerializeField] private float _rotationSpeed = 15f;

    private int _gass = 0;
    private int _sign = 0;
    
    private Rigidbody _rigidBody = null;
    // Movement vectors
    private Vector3 _velocity = Vector3.zero;
    private Vector3 _acceleration = Vector3.zero;
    private Vector3 _orientation = Vector3.zero;
    private Vector3 _position = Vector3.zero;

    // Rotation Matrix;
    private Matrix4x4 _rotationY = Matrix4x4.identity;
    private float _rotationDegree = 0f;

    // GameObjects to Update
    [SerializeField] private GameObject[] wheels = new GameObject[2];

    private void Awake()
    {
        _position = transform.position;  
        _orientation = transform.forward;

        if(!_rigidBody)
            _rigidBody = GetComponentInChildren<Rigidbody>();
    }
    private void OnEnable()
    {
        InputEventManagerW8.OnNavigationInputActivated += FetchInputCoordinate;
        InputEventManagerW8.OnNavigationInputCancelled += FetchInputCoordinate;
    }

    private void Update()
    {
        // Get coordinates

        // Update movement forces
        MovePlayer();

        // Update GameObjectOrientation
        RotatePlayer();
        UpdateOrientation();

        DebbugRender();
    } 

    private void OnDisable()
    {
        Unsubscribed();

    }

    void OnDestroy()
    {
        Unsubscribed();
    }

    private void Unsubscribed()
    {
        InputEventManagerW8.OnNavigationInputActivated -= FetchInputCoordinate;
        InputEventManagerW8.OnNavigationInputCancelled -= FetchInputCoordinate;
    }

    private void FetchInputCoordinate(Vector2 coordinates)
    {
        Debug.Log("Got event coordinates: "+ coordinates);
        //_gass = (int) coordinates.y;
        _gass = coordinates.y > 0 ? 1 : coordinates.y < 0 ? -1 : 0;
        //_sign = (int) coordinates.x;
        _sign = coordinates.x > 0 ? -1 : coordinates.x < 0 ? 1 : 0;

        _sign *= _gass;

    }

    private void FetchInputCoordinate()
    {
        _gass = 0;
        _sign = 0;
    }

    private void MovePlayer()
    {
        // Update force
        _acceleration = Vector3.zero;
        float dt = Time.deltaTime;

        Vector3 force = _orientation * _maxForce;
        float mass = _rigidBody.mass;
        _acceleration += (force / mass) * _gass;

        // Add acceleratio to velocity
        _velocity += _acceleration * dt;

        // Clamp velocity
        if(_velocity.magnitude > _maxVelocity)
            _velocity = _velocity.normalized * _maxVelocity;

        // Drag
        _velocity = _velocity * Mathf.Max(0, 1f - _dragFactor * dt);
        
        // find position at dt+1
        _position += _velocity * dt;

        transform.position = _position;
        
    }

    private void RotatePlayer()
    {

        // Define the angle to rotate:
        float dt = Time.deltaTime;

        // Cast to radians
        _rotationDegree = _rotationAngle * _sign * dt;
        //_rotationDegree *= _rotationSpeed;

        float theta = _rotationDegree * (2f*Mathf.PI / 360f);

        // Define the transformation of the basis vectors
        Vector4 i = new Vector4(Mathf.Cos(theta),   0,  Mathf.Sin(theta),   0);
        Vector4 j = new Vector4(0,                  1,                 0,   0);
        Vector4 k = new Vector4(-Mathf.Sin(theta),  0,  Mathf.Cos(theta),   0);
        Vector4 w = new Vector4(0,                  0,                 0,   1);

        _rotationY      = new Matrix4x4(i,j,k,w);
        _orientation    = _rotationY.MultiplyPoint3x4(_orientation).normalized;
    }

    private void UpdateOrientation()
    {
        //if(_velocity.magnitude > 0.01f)
        //    transform.forward = _velocity.normalized;
        transform.forward = _orientation;
        UpdateWheels();

    }

    private void UpdateWheels()
    {
        if(_velocity.magnitude >0.01f)
        {
            foreach(var wheel in wheels)
            {
                wheel.transform.forward = _velocity * -1f;
            }
        }
    }

    private void DebbugRender()
    {
        Debug.DrawLine(_position, _position + _orientation * 1f, Color.yellow);
    }


}