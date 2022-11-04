using UdonSharp;
using VRC.SDKBase;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class ShoulderTapUIController : UdonSharpBehaviour
{
    private VRCPlayerApi localPlayer;

    private void Start()
    {
        localPlayer = Networking.LocalPlayer;
    }

    private void Update()
    {
        if (!Utilities.IsValid(localPlayer)) return;

        var headTracking = localPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head);

        transform.SetPositionAndRotation(headTracking.position, headTracking.rotation);
    }
}
