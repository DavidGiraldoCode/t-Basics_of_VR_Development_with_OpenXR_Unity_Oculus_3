using UnityEngine;

public class SimulatorManeger: MonoBehaviour
{
    [SerializeField] private GameObject _xrDeviceSimulator = null;
    private void Awake()
    {
        #if UNITY_EDITOR
            if(_xrDeviceSimulator)
                _xrDeviceSimulator.SetActive(true);
        #else
            if(_xrDeviceSimulator)
                Destroy(_xrDeviceSimulator);
        #endif

    }
}