using UnityEngine;

public class FallingObjectDestructor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.gameObject.GetComponentInParent<Rigidbody>();
        Destroy(rb.gameObject);
    }
}
