using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlaneResize : MonoBehaviour
{
    private ARPlane _arPlane;
    [SerializeField] private GameObject _planeProxyGO;
    private void Awake() 
    {
        _arPlane = GetComponent<ARPlane>();
    }

    private void Update()
    {
        if(_planeProxyGO != null && _arPlane != null)
        {
            _planeProxyGO.transform.forward = _arPlane.normal * -1f;
            _planeProxyGO.transform.localScale = _arPlane.size;

            if(_arPlane.classifications != PlaneClassifications.WallFace)
                Destroy(gameObject);
        }

    }
}
