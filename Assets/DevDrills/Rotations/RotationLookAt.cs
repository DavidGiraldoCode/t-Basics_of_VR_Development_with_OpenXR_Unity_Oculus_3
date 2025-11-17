using UnityEngine;

public class RotationLookAt : MonoBehaviour
{
    [SerializeField] Transform _targetTransform = null;
    Vector3 lookingDiretion = Vector3.zero;

    void Update()
    {
        lookingDiretion = _targetTransform.position - transform.position;
        transform.up = lookingDiretion;
    }
}
