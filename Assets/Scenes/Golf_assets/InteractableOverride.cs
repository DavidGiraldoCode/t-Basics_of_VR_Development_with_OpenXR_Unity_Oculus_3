using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class InteractableOverride : MonoBehaviour
{
    [SerializeField] private GolfClubSetting _settings;
    private XRGrabInteractable xRGrabInteractable;
    private void Awake()
    {
        if (xRGrabInteractable == null)
            xRGrabInteractable = GetComponent<XRGrabInteractable>();

        if (_settings == null)
            throw new System.Exception("The InteractableOverride needs the ScritbleObject GolfClubSetting");
    }

    private void Update()
    {
        if (xRGrabInteractable.trackPosition)
        {
            if (xRGrabInteractable.smoothPosition)
            {
                xRGrabInteractable.smoothPositionAmount = _settings.SmoothPositionAmount;
                xRGrabInteractable.tightenPosition = _settings.TightenPosition;
            }
            xRGrabInteractable.velocityDamping = _settings.VelocityDamping;
        }
        
        if (xRGrabInteractable.trackRotation)
        {
            if (xRGrabInteractable.smoothRotation)
            {
                xRGrabInteractable.smoothRotationAmount = _settings.SmoothRotationAmount;
                xRGrabInteractable.tightenRotation = _settings.TightenRotation;
            }
            xRGrabInteractable.angularVelocityDamping = _settings.AngularVelocityDamping;
        }
    }
}
