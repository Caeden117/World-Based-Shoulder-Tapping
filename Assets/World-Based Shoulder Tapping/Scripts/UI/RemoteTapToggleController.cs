using Cyan.PlayerObjectPool;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class RemoteTapToggleController : UdonSharpBehaviour
{
    [SerializeField] private Toggle uiToggle;
    [SerializeField] private CyanPlayerObjectAssigner playerTapPoolAssigner;

    private VRCPlayerApi localPlayer;

    private void Start()
    {
        localPlayer = Networking.LocalPlayer;
    }

    public void OnToggle()
    {
        var remoteTapObject = playerTapPoolAssigner._GetPlayerPooledObject(localPlayer);

        // god udonsharp please support generic GetComponent<T>
        var remoteTapComponent = (ShoulderTapUpdateController)remoteTapObject.GetComponent(typeof(UdonBehaviour));

        if (uiToggle.isOn)
        {
            remoteTapComponent.EnableShoulderTapping();
        }
        else
        {
            remoteTapComponent.DisableShoulderTapping();
        }
    }
}
