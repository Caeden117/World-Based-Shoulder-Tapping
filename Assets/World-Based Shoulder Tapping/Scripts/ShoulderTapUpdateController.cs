#pragma warning disable IDE1006 // "_" prefixed methods, used by CyanPlayerObjectPool
using System;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class ShoulderTapUpdateController : UdonSharpBehaviour
{
    [SerializeField] private ShoulderTapRemotePlayerUpdateController leftTapScript;
    [SerializeField] private Text leftTapName;
    [SerializeField] private ShoulderTapRemotePlayerUpdateController rightTapScript;
    [SerializeField] private Text rightTapName;
    [SerializeField] private Animator uiAnimator;
    [SerializeField] private ShoulderTapLocalPlayerController localPlayerController;

    [NonSerialized]
    public VRCPlayerApi Owner;

    [NonSerialized, UdonSynced]
    public bool EnabledByPlayer = true;

    private GameObject leftTapObject;
    private GameObject rightTapObject;
    private VRCPlayerApi localPlayer;
    private bool isTracking = true;
    private string lastTappedBy = "Unknown";

    private void Start()
    {
        localPlayer = Networking.LocalPlayer;
    }

    public void _OnOwnerSet()
    {
        leftTapObject = leftTapScript.gameObject;
        rightTapObject = rightTapScript.gameObject;

        leftTapScript.InteractionText = rightTapScript.InteractionText = Owner.displayName;

        if (Owner.isLocal)
        {
            leftTapObject.SetActive(false);
            rightTapObject.SetActive(false);
        }
        else
        {
            EnableShoulderTapping();
        }
    }

    public void _OnCleanup() => DisableShoulderTapping();

    public void DisableShoulderTapping()
    {
        EnabledByPlayer = false;
        RequestSerialization();
    }

    public void EnableShoulderTapping()
    {
        EnabledByPlayer = true;
        RequestSerialization();
    }

    public void LeftShoulderTapped()
    {
        if (Owner.isLocal)
        {
            leftTapName.text = lastTappedBy;
            uiAnimator.SetTrigger("Left Shoulder Tapped");
        }
    }

    public void RightShoulderTapped()
    {
        if (Owner.isLocal)
        {
            rightTapName.text = lastTappedBy;
            uiAnimator.SetTrigger("Right Shoulder Tapped");
        }
    }

    /*
     * So this has to be the most stupid, big brain idea I've come up with in Udon:
     * 
     * THE PROBLEM: You cannot directly tell a remote player "Hey, *I* specifically tapped you"
     * There is no way to send custom events with Udon and pass the calling VRCPlayerApi as a parameter.
     * 
     * THE SOLUTION: Attempt to change the owner of the object!
     * This gives us the player who requested the change in ownership.
     * To not conflict with CyanPlayerObjectPool, having an invalid current owner will still be approved.
     * BUT, if we already have an Owner, we set the tapping user and deny the ownership request.
     * 
     * GOD THIS IS SO STUPID.
     */
    public override bool OnOwnershipRequest(VRCPlayerApi requestingPlayer, VRCPlayerApi newOwner)
    {
        lastTappedBy = newOwner.displayName;

        return !Utilities.IsValid(Owner) || Owner.playerId != localPlayer.playerId;
    }

    private void Update()
    {
        if (!Utilities.IsValid(Owner) || !Utilities.IsValid(localPlayer) || Owner.isLocal) return;

        // Enable interactables iff:
        //   1) Remote player has shoulder tapping enabled
        //   2) Remote player is within local tracking distance
        //   3) Local player is not facing towards remote player (cant tap shoulder from the front)
        var shouldBeTracked = EnabledByPlayer
            && Vector3.Distance(Owner.GetPosition(), localPlayer.GetPosition()) < localPlayerController.TrackingRadius
            && Quaternion.Angle(Owner.GetRotation(), localPlayer.GetRotation()) < 105;

        if (shouldBeTracked != isTracking)
        {
            isTracking = shouldBeTracked;
            leftTapObject.SetActive(shouldBeTracked);
            rightTapObject.SetActive(shouldBeTracked);
        }
    }
}
#pragma warning restore IDE1006 // Naming Styles