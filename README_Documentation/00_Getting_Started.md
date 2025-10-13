# Getting started

## Packages needed:
- OpenXR
- Unversal Render Pipeline
- XR Plugin Management
- XR Interaction Toolkit

## Setup steps 
(If the `XR Origin` component is already setup with the controller, the next steps are)
- Add a `LocomotionMediator` component to enable transformation on the model matrix of the `XROrigin`
- Provide a `SnapTurnProvider` component to enable *Snapped Truns*
- Include a `TeleportationProvider` component to enable *Teleportation* to ether areas or anchors.
- For an area: Select a GameObject that has a collider and add the `TeleportationArea` component, which communicates with the `XRInteractionManager` and the `TeleportationProvider`. Define the orientation at the destination to be **World Space Up**, which will align the Up vector of the `XROrigin` with the Up vector of the world.
- For an anchor: Select a GameObject that has a collider and add the `TeleportationArea` component, which communicates with the `XRInteractionManager` and the `TeleportationProvider`. Define the orientation at the destination to be **Target Up and Forward**, which will align the Up and Forward vector of the `XROrigin` with the Up and Forward vector of the GameObject the user is teleporting to.