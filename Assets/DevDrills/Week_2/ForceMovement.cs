using UnityEngine;
using UnityEngine.InputSystem;

//! **********************************************************************************************/
//! This is the right answer, TODO, study and re-do it from scratch
//! **********************************************************************************************/
public class ForceMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveForce = 30f;
    public float drag = 5f;
    public float maxSpeed = 6f;

    private Vector3 velocity;
    private Vector3 acceleration;

    [SerializeField] private InputAction moveAction;

    void OnEnable() => moveAction.Enable();
    void OnDisable() => moveAction.Disable();

    void Update()
    {
        // --- 1) Reset acceleration
        acceleration = Vector3.zero;

        // --- 2) Read input (WASD)
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 force = new Vector3(input.x, 0, input.y) * moveForce;

        // --- 3) Add force -> acceleration
        acceleration += force;

        // --- 4) Integrate acceleration into velocity
        velocity += acceleration * Time.deltaTime;

        // --- 5) Apply drag
        velocity *= (1f - drag * Time.deltaTime);

        // --- 6) Clamp max speed
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        // --- 7) Move
        transform.position += velocity * Time.deltaTime;

        // --- 8) Face movement direction
        if (velocity.sqrMagnitude > 0.01f)
            transform.forward = velocity.normalized;
    }
}

