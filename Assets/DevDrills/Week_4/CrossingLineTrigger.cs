using UnityEngine;

public class CrossingLineTrigger: MonoBehaviour
{
    [SerializeField] private Collider _collider;

    void Start()
    {
      if( _collider == null )
        _collider = GetComponentInChildren<BoxCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        GameEventsManager.Instance.OnGameEnd();   
        Debug.Log("Game Over!");
    }

}