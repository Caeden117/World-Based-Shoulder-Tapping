using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class ShoulderTapLocalPlayerController : UdonSharpBehaviour
{
    [NonSerialized]
    public float TrackingRadius = 5f;

    [NonSerialized]
    public bool IsTracking = true;

    private VRCPlayerApi localPlayer;

    private void Start()
    {
        localPlayer = Networking.LocalPlayer;
    }

    // To my knowledge, this has to be updated every frame to account for changes in avatar and hand movement
    private void Update()
    {
        if (!Utilities.IsValid(localPlayer) || !IsTracking) return;

        // Radius will be distance to longest finger point
        // NOTE: It is assumed that the middle finger is the longest finger in a hand
        var headPosition = localPlayer.GetBonePosition(HumanBodyBones.Head);

        TrackingRadius = Mathf.Max(
            Vector3.Distance(headPosition, localPlayer.GetBonePosition(HumanBodyBones.LeftMiddleDistal)),
            Vector3.Distance(headPosition, localPlayer.GetBonePosition(HumanBodyBones.RightMiddleDistal)));
    }
}
