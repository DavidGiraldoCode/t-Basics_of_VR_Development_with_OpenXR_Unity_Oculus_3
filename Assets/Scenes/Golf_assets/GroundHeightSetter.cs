using UnityEngine;

public class GroundHeightSetter : MonoBehaviour
{
    private void OnEnable() => EventManager.OnThumbStickMoved += UpdateGroundHeight;
    private void OnDisable() => EventManager.OnThumbStickMoved -= UpdateGroundHeight;
    private void UpdateGroundHeight(float x, float y)
    {
        int direction = y > 0 ? 1 : -1; 
        float updateStep = 0.5f;
        transform.position += direction * updateStep * Time.deltaTime * Vector3.up;
    }
}
