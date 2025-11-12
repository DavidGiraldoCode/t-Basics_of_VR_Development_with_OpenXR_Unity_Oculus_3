using Unity.Collections;
using Unity.Mathematics.Geometry;
using UnityEngine;

public class RotateAroundAxis : MonoBehaviour
{
    Transform modelMatrix = null;

    float speed = 10f;
    //

    public float amplitude = 1f;         // Max displacement up/down
    public float frequency = 0.5f;         // Cycles per second

    private Vector3 startPos;
    public float angularSpeed = 45f; // degrees per second

    void Start()
    {
        modelMatrix = GetComponentInParent<Transform>();
        startPos = transform.position;
    }

    void Update()
    {
        //float angle = Time.time * frequency * 2f * Mathf.PI;
        float angle = angularSpeed * Time.time * Mathf.Deg2Rad;

        
        float newX = startPos.x;
        float newY = startPos.y * Mathf.Cos(angle) - startPos.z * Mathf.Sin(angle);
        float newZ = startPos.y * Mathf.Sin(angle) + startPos.z * Mathf.Cos(angle);
        
        Vector3 rotated = new Vector3(newX, newY, newZ);

        transform.position = rotated;
   
    }

    void Sinusoidal()
    {
        // 2*PI - > A complete sine wave cycle (from 0 → 1 → 0 → −1 → 0) occurs over 2π radians, which is equivalent to 360°
        // θ = 2π𝑓𝑡
        /*
        θ = current angle (radians),
        𝑓 f = frequency (Hz),
        𝑡 t = elapsed time (seconds).
        */

        float angle = Time.time * frequency * 2f * Mathf.PI;

        float yOffset = Mathf.Sin(angle) * amplitude;
        float zOffset = Mathf.Cos(angle) * amplitude;

        transform.position = startPos + new Vector3(0, yOffset, zOffset);
    }
    void Rotation()
    {
        speed += speed;

        if(speed > 360)
            speed = 0f;

        float ang = speed * Mathf.Deg2Rad;

        float yOffset = Mathf.Sin(ang) * amplitude;
        float newPosY = modelMatrix.position.y + (yOffset * Time.deltaTime);

        Vector3 newPosition = new Vector3(modelMatrix.position.x, newPosY, modelMatrix.position.z);

        modelMatrix.position = newPosition;
        
        //modelMatrix.position = 0.01 * Time.deltaTime;
        //modelMatrix.position += Vector3.up * Mathf.Sin(ang) * Time.deltaTime;
        /*modelMatrix.position = new Vector3(modelMatrix.position.x * 1.0f,
                                            modelMatrix.position.y * Mathf.Sin(rotationSpeed),
                                            modelMatrix.position.z * Mathf.Cos(rotationSpeed) );*/
        
    }
}
