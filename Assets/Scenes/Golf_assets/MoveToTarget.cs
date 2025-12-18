using System;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))] 
public class MoveToTarget : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform = null;
    private Rigidbody _rigidBody;

    private void Awake()
    {
      _rigidBody = GetComponent<Rigidbody>();  
      _rigidBody.isKinematic = true;
      _rigidBody.interpolation = RigidbodyInterpolation.Interpolate;
    }

    private void FixedUpdate()
    {
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        if(_targetTransform)
        {
           _rigidBody.MovePosition(_targetTransform.position);
           _rigidBody.MoveRotation(_targetTransform.rotation);  
        }
        //_rigidBody.Move(_targetTransform.position, _targetTransform.rotation);
    }
}
