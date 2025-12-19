using System.Collections.Generic;
using UnityEngine;

public class VirtualProxyVisibility : MonoBehaviour
{
    [SerializeField] private List<GameObject> _visualProxies;

    private void OnEnable() => EventManager.OnToggleVirtualProxyVisibility += ToggleVisibility;
    private void OnDisable() => EventManager.OnToggleVirtualProxyVisibility -= ToggleVisibility;
    private void ToggleVisibility()
    {
        if(_visualProxies.Count == 0) return;

        foreach(var proxy in _visualProxies)
        {
            proxy.SetActive(!proxy.activeSelf);
        }
    }
}
