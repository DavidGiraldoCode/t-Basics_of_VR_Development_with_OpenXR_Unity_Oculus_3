using System;
using Unity.VisualScripting;
using UnityEngine;

public class Projection : MonoBehaviour
{
    [SerializeField] private Transform _plane = null;
    [SerializeField] private Transform _pointA = null;
    [SerializeField] private Transform _pointB = null;
    [SerializeField] private float _scalarAlpha = 0f;
    [SerializeField] private float _normalizeDotProduct = 0f;
    private Vector3 _scaledB = new Vector3();
    private Vector3 _scaledUnity = new Vector3();
    private Vector3 _crossUnity = new Vector3();
    private Vector3 _crossVector = new Vector3();

    [SerializeField] bool _myMethod = true;
    [SerializeField] bool _UnitysMethod = false;
    
    /// <summary>
    /// Return a normalized scalar value, representing how close vector a is to b, and the direction
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    private float DotProduct(Vector3 a, Vector3 b)
    {
        //* Normalization vn = v / ||V||
        Vector3 aNorm = a / Mathf.Sqrt((a.x*a.x) + (a.y*a.y) + (a.z*a.z));
        Vector3 bNorm = b / Mathf.Sqrt((b.x*b.x) + (b.y*b.y) + (b.z*b.z));

        return (aNorm.x * bNorm.x) + (aNorm.y * bNorm.y) + (aNorm.z * bNorm.z);
    }

    /// <summary>
    /// Returns a pojected version of vector a along the vector b
    /// </summary>
    /// <param name="a">Vector that get projected</param>
    /// <param name="b">Vector to project onto</param>
    /// <param name="scalar">The scalar value of the projection</param>
    /// <returns>The projected vector</returns>
    private Vector3 ProjectOnto(Vector3 a, Vector3 b, out float  scalar)
    {
        //* Normalization vn = v / ||V||
        Vector3 bNorm = b / Mathf.Sqrt((b.x*b.x) + (b.y*b.y) + (b.z*b.z));
        scalar = (a.x * bNorm.x) + (a.y * bNorm.y) + (a.z * bNorm.z);

        return new Vector3( bNorm.x * scalar,
                            bNorm.y * scalar,
                            bNorm.z * scalar);
    }

    private Vector3 CrossProuct(Vector3 a, Vector3 b)
    {
        float x = (a.y * b.z) - (a.z * b.y);
        float y = (a.z * b.x) - (a.x * b.z);
        float z = (a.z * b.y) - (a.y * b.x);

        return new Vector3(x, y, z);
    }
    void Update()
    {

        Debug.DrawLine(Vector3.zero, _pointA.position, Color.blueViolet);
        Debug.DrawLine(Vector3.zero, _pointB.position, Color.aquamarine);

        _normalizeDotProduct = DotProduct(_pointA.position, _pointB.position);

        _scaledB = ProjectOnto(_pointA.position, _pointB.position, out _scalarAlpha);
        if( _myMethod )
            Debug.DrawLine(Vector3.zero, _scaledB, Color.red);

        _scaledUnity = Vector3.Project(_pointA.position, _pointB.position);
        if(_UnitysMethod )
            Debug.DrawLine(Vector3.zero, _scaledUnity, Color.magenta);

        // Cross product
        _crossVector = CrossProuct(_pointA.position, _pointB.position);
        if(_myMethod)
            Debug.DrawLine(Vector3.zero, _crossVector, Color.yellow);

        _crossUnity = Vector3.Cross(_pointA.position, _pointB.position);
        if(_UnitysMethod )
            Debug.DrawLine(Vector3.zero, _crossUnity, Color.gray);

        _plane.up = _pointB.position;
        _plane.right = _crossVector;

        //TODO: Orient a quad base on the plane that the cross product of A X B created and te projetion of A onto the plane.
        // Check the link for a reference.
        //? https://immersivemath.com/ila/ch04_vectorproduct/ch04.html#ch_vp

    }
}
