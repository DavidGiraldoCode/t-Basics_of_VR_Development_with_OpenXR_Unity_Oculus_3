using UnityEngine;

public class XRPositionSetter: MonoBehaviour
{
    private Quaternion _previousRotation = Quaternion.identity;
    private Vector3 _previousPosition = Vector3.zero;

    private bool _isUpdatingTransform = false;

    public bool IsUpdatingTransform 
    {
        get => _isUpdatingTransform; 
        set => _isUpdatingTransform = value ;
    }

    private void Start()
    {
      _isUpdatingTransform = true;
      UpdateTransformFromXRController();
    }

    private void Update()
    {
        UpdateTransformFromXRController();
    }
    private void UpdateTransformFromXRController()
    {
        if(!_isUpdatingTransform)
        {
            transform.position = _previousPosition;
            transform.rotation = _previousRotation;
        }
        else
        {
            _previousPosition = transform.position;
            _previousRotation = transform.rotation;
        }
    }

}