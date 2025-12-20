using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARBoundingResize : MonoBehaviour
{

    [SerializeField] private GameObject _boundingBoxGO; 
    private ARBoundingBox _arBoundingBox;

    private void Awake() 
    {
        _arBoundingBox = GetComponent<ARBoundingBox>();   
    }
    void Update()
    {
        if(_boundingBoxGO != null && _arBoundingBox != null)
            _boundingBoxGO.transform.localScale = _arBoundingBox.size;
    }
}
