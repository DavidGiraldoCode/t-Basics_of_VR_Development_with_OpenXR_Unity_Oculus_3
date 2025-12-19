using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public static BallSpawner Instance {get; private set;}
    [SerializeField] private Transform _spawnCoordinates;
    [SerializeField] private GameObject _golfBallPrefab = null;
    public void SpawnGolfBall()
    {
        if(_golfBallPrefab != null && _spawnCoordinates != null)
            Instantiate(_golfBallPrefab, _spawnCoordinates.position, _spawnCoordinates.rotation);
    }
}
