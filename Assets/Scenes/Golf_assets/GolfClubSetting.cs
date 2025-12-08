using UnityEngine;

[CreateAssetMenu(fileName = "GolfClubSetting", menuName = "Scriptable Objects/GolfClubSetting")]
public class GolfClubSetting : ScriptableObject
{
    [Header("Tracking velocity")]    
    [Range(0f, 20f)][SerializeField] private float _smoothPositionAmount = 10f;
    [Range(0f, 1f)][SerializeField] private float _tightenPosition = 0.5f;
    [Range(0f, 1f)][SerializeField] private float _velocityDamping = 0.5f;

    [Header ("Tracking rotation")]
    [Range(0f, 20f)][SerializeField] private float _smoothRotationAmount = 10f;
    [Range(0f, 1f)][SerializeField] private float _tightenRotation = 0.5f;
    [Range(0f, 1f)][SerializeField] private float _angularVelocityDamping = 0.5f;

    public float SmoothPositionAmount { get => _smoothPositionAmount; set => _smoothPositionAmount = value; }
    public float TightenPosition { get => _tightenPosition; set => _tightenPosition = value; }
    public float VelocityDamping { get => _velocityDamping; set => _velocityDamping = value; }

    public float SmoothRotationAmount { get => _smoothRotationAmount; set => _smoothRotationAmount = value; }
    public float TightenRotation { get => _tightenRotation; set => _tightenRotation = value; }
    public float AngularVelocityDamping { get => _angularVelocityDamping; set => _angularVelocityDamping = value; }


    // GUI
    public void GUISetSmoothPositionAmount(float sliderValue)
    {
        _smoothPositionAmount = sliderValue;
    }
}
