using UnityEngine;

public class CustomFrame : MonoBehaviour
{
    [SerializeField] private Transform _point = null;
    void Update()
    {
        // Frist try:
        // Getting the point position from a given GameObject 'B' set on the Editor
        Vector3 point = _point.position;
        Vector3 worldUp = transform.up;
        Vector3 worldRight = transform.right;
        Vector3 worldForward = transform.forward;

        // Defining the position of "this" GameObject as the origin
        Vector3 origin = transform.position;
        // Creating a relative vector from the world to the origin to help me build the basis vectors
        // of my custom frame
        Vector3 relativeFrameToWorld = Vector3.zero - origin;
        Vector3 local = Vector3.zero;

        // Define my custom up vector, in this case it matches the world up
        Vector3 up = new Vector3(0,1,0);
        // Created the forward and the Up using Cross Product.
        // This is making the vector rotate when I move the origin around the worl, that should not happen, shouldn't it?
        Vector3 forward = Vector3.Cross(up, relativeFrameToWorld).normalized;
        Vector3 right = Vector3.Cross(up, forward).normalized;

        Debug.DrawLine(origin, origin+up * 0.5f, Color.green);
        Debug.DrawLine(origin, origin+forward * 0.5f, Color.blue);
        Debug.DrawLine(origin, origin+right * 0.5f, Color.red);
        
        Debug.DrawLine(Vector3.zero, origin, Color.grey);
        Debug.DrawLine(Vector3.zero, relativeFrameToWorld, Color.orange);

        // Creating the relative of the point to the origin.
        Vector3 relative = origin - point;

        // Creating the local.
        // But is not matching the location of the GameObject 'B' on the world.
        // Also, it points up when I put the object down in the world Y-axis.
        local.x = Vector3.Dot(relative, right);
        local.y = Vector3.Dot(relative, up);
        local.z = Vector3.Dot(relative, forward);

        Debug.DrawLine(origin, local, Color.magenta);
    }
    private void WorldToLocal(Vector3 point, Vector3 up, Vector3 right, Vector3 forward, Vector3 origin)
    {
        
    }

    private void LocalToWorld(Vector3 point, Vector3 up, Vector3 right, Vector3 forward)
    {
        
    }
}
