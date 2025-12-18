using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public static BallSpawner Instance {get; private set;}
    [SerializeField] private GameObject _golfBallPrefab = null;
    public void SpawnGolfBall()
    {
        if(_golfBallPrefab != null)
            Instantiate(_golfBallPrefab, Vector3.up * 2f,Quaternion.identity);
    }
}
