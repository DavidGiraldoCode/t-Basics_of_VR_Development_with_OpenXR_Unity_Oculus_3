using UnityEngine;

public class Rotator : MonoBehaviour
{
    [Range(0, 360)]
    [SerializeField] int degree = 0;

    private Vector3 _startingPosition;

    void Start()
    {
        _startingPosition = transform.localPosition;    
    }

    /// <summary>
    /// Divides the entire circumference 2PI by 360, to get the 
    /// size of 1 degree in radians and mutiplies that radian by 
    /// the input degree to return the equivalent radian
    /// </summary>
    /// <param name="degree"> Degree of the rotation</param>
    /// <returns>The equivalent degree rotation in radian</returns>
    float radian(int degree)
    {
        return (float)degree * (2f * Mathf.PI / 360f);
    }

    void Update()
    {
        float xNew = _startingPosition.x;
        float yNew = (_startingPosition.y * Mathf.Cos(radian(degree))) + (_startingPosition.z * Mathf.Sin(radian(degree)));
        float zNew = (_startingPosition.y * -Mathf.Sin(radian(degree))) + (_startingPosition.z * Mathf.Cos(radian(degree)));

        transform.localPosition = new Vector3(xNew, yNew, zNew);
    }

}
