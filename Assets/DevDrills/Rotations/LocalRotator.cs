using UnityEngine;

public class LocalRotator : MonoBehaviour
{
    [Range(0, 360)]
    [SerializeField] int degree = 0;

    private Vector3 _startingPosition;
    void Start()
    {
        _startingPosition = transform.forward;    
    }
    float radian(int degree)
    {
        return (float)degree * (2f * Mathf.PI / 360f);
    }

    void Update()
    {
        float x = _startingPosition.x;
        float y = (_startingPosition.y * Mathf.Cos(radian(degree))) + (_startingPosition.z * Mathf.Sin(radian(degree)));
        float z = (_startingPosition.y * -Mathf.Sin(radian(degree))) + (_startingPosition.z * Mathf.Cos(radian(degree)));

        transform.forward = new Vector3(x, y, z);        
    }
}
