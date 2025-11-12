using UnityEngine;

public class LightController : MonoBehaviour
{
    public ButtonController buttonController;
    private Light _light;

    private void ToggleLight()
    {
        if(_light)
            _light.enabled = !_light.enabled;
    }

    private void Awake()
    {
        _light = GetComponent<Light>();
    }
    private void OnEnable()
    {
        if(buttonController)
        {
            buttonController.buttonPressed += ToggleLight;
            buttonController.OnButtonActionPressed += ToggleLight;
        }
    }

    void OnDisable()
    {
        if(buttonController)
        {
            buttonController.buttonPressed -= ToggleLight;
            buttonController.OnButtonActionPressed -= ToggleLight;  
        }
    }
}
