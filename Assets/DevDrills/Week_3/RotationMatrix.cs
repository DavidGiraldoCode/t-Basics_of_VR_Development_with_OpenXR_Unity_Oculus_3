using UnityEngine;

// TODO: Validate math
public class RotationMatrix: MonoBehaviour
{
    private Matrix4x4 _rationXAxis = Matrix4x4.identity;
    private Matrix4x4 _rationYAxis = Matrix4x4.identity;
    private Matrix4x4 _rationZAxis = Matrix4x4.identity;
    private Vector3 _lastPosition = Vector3.zero;
    private float PI = Mathf.PI;
    [Range(0,360)] [SerializeField] private float _angleX;
    [Range(0,360)] [SerializeField] private float _angleY;
    [Range(0,360)] [SerializeField] private float _angleZ;

    private void Start()
    {
        _lastPosition = transform.position;
    }

    private void Update()
    {
        
        //UpdatePosition();
        Matrix4x4 transformation = RotateAroundZ(_angleZ) * RotateAroundY(_angleY) * RotateAroundX(_angleX);
        transform.position = transformation.MultiplyPoint3x4(_lastPosition);
    }

    private void UpdatePosition()
    {
        bool hasSimilarX = Mathf.Approximately(transform.position.x, _lastPosition.x);
        bool hasSimilarY = Mathf.Approximately(transform.position.y, _lastPosition.y);
        bool hasSimilarZ = Mathf.Approximately(transform.position.z, _lastPosition.z);
        
        if(!hasSimilarX || !hasSimilarY || !hasSimilarZ)
        {
            _lastPosition = transform.position;
            Debug.Log("Updating positon! to" + _lastPosition);
        }
    }
    /// <summary>
    /// Utility function to transform degrees to radians
    /// </summary>
    private float D2R(float angle) => angle * (2*PI/360f);
    private Matrix4x4 RotateAroundZ(float angle)
    {
        // Form degrees to radians
        float radians = D2R(angle);

        // Build the Transformation matrix at a given angle
        Vector4 i = new Vector4(  Mathf.Cos(radians), Mathf.Sin(radians), 0, 0);
        Vector4 j = new Vector4( -Mathf.Sin(radians), Mathf.Cos(radians), 0, 0);
        Vector4 k = new Vector4(0,0,1,0);
        Vector4 w = new Vector4(0,0,0,1);
        
        _rationZAxis = new Matrix4x4(i, j, k, w);

        // Multiply the Matrix by the position vector.
        //transform.position = _rationZAxis.MultiplyPoint(_lastPosition);
        return _rationZAxis;
    }

    private Matrix4x4 RotateAroundX(float angle)
    {
        // Form degrees to radians
        float radians = D2R(angle);

        // Build the Transformation matrix at a given angle
        Vector4 i = new Vector4(1,0,0,0);
        Vector4 j = new Vector4(0,-Mathf.Sin(radians), Mathf.Cos(radians), 0);
        Vector4 k = new Vector4(0, Mathf.Cos(radians), Mathf.Sin(radians), 0);
        Vector4 w = new Vector4(0,0,0,1);
        
        _rationXAxis = new Matrix4x4(i, j, k, w);

        // Multiply the Matrix by the position vector.
        //transform.position = _rationXAxis.MultiplyPoint(_lastPosition);
        return _rationXAxis;

    }

    private Matrix4x4 RotateAroundY(float angle)
    {
        // Form degrees to radians
        float radians = D2R(angle);

        // Build the Transformation matrix at a given angle
        Vector4 i = new Vector4(  Mathf.Cos(radians), 0, Mathf.Sin(radians), 0);
        Vector4 j = new Vector4(0,1,0,0);
        Vector4 k = new Vector4( -Mathf.Sin(radians), 0, Mathf.Cos(radians), 0);
        Vector4 w = new Vector4(0,0,0,1);
        
        _rationYAxis = new Matrix4x4(i, j, k, w);

        // Multiply the Matrix by the position vector.
        //transform.position = _rationYAxis.MultiplyPoint(_lastPosition);
        return _rationYAxis;

    }
}
