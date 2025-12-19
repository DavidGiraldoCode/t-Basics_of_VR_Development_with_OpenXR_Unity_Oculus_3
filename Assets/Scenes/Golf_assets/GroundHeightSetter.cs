using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundHeightSetter : MonoBehaviour
{
    enum Visibility
    {
        INVISIBLE,
        GHOST
    }
    private MeshRenderer _planeRenderer = null;
    private List<Material> _materials = new List<Material>();
    private void OnEnable()
    {
        EventManager.OnThumbStickMoved += UpdateGroundHeight;
        EventManager.OnThumbStickStarted += ShowGround;
        EventManager.OnThumbStickRealeased += HideGround;
    }
    private void OnDisable()
    {
        EventManager.OnThumbStickMoved -= UpdateGroundHeight;
        EventManager.OnThumbStickStarted -= ShowGround;
        EventManager.OnThumbStickRealeased -= HideGround;
        
    }

    private void Awake()
    {
        _planeRenderer = GetComponentInChildren<MeshRenderer>();
        _materials.Add(_planeRenderer.materials[0]);
        Color c = _materials[0].color;
        c.a = 0.5f;
        _materials[0].SetColor("_BaseColor",c); 
        HideGround();
    }
    private void UpdateGroundHeight(float x, float y)
    {
        int direction = y > 0 ? 1 : -1; 
        float updateStep = 0.1f;
        transform.position += direction * updateStep * Time.deltaTime * Vector3.up;
    }

    private void ShowGround() => SetMaterialVisbility(Visibility.GHOST);
    private void HideGround() => SetMaterialVisbility(Visibility.INVISIBLE);

    private void SetMaterialVisbility(Visibility type)
    {
        if(!_planeRenderer) return;

        if(type == Visibility.GHOST)
            _planeRenderer.SetMaterials(_materials);
        else if(type == Visibility.INVISIBLE)
            _planeRenderer.SetMaterials(new List<Material>());       
    }
}
