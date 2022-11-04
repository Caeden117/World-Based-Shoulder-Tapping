
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class LocalTapToggleController : UdonSharpBehaviour
{
    [SerializeField] private Toggle uiToggle;
    [SerializeField] private ShoulderTapLocalPlayerController localPlayerController;

    public void OnToggle()
    {
        var isTracking = uiToggle.isOn;

        localPlayerController.IsTracking = isTracking;

        if (!isTracking)
        {
            localPlayerController.TrackingRadius = 0f;
        }
    }
}
