using UnityEngine;

public class BallRemover : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.activeSelf)
            other.gameObject.SetActive(false);
    }
}
