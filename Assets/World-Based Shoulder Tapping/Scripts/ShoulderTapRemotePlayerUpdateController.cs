using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon.Common.Interfaces;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class ShoulderTapRemotePlayerUpdateController : UdonSharpBehaviour
{
    [SerializeField] private ShoulderTapUpdateController updateController;
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private bool isLeft;

    private Vector3 zero = Vector3.zero;
    private GameObject updateControllerGameObject;
    private VRCPlayerApi localPlayer;
    private HumanBodyBones targetUpperArm;
    private HumanBodyBones targetShoulder;

    private void Start()
    {
        updateControllerGameObject = updateController.gameObject;
        localPlayer = Networking.LocalPlayer;
        targetUpperArm = isLeft ? HumanBodyBones.LeftUpperArm : HumanBodyBones.RightUpperArm;
        targetShoulder = isLeft ? HumanBodyBones.LeftShoulder : HumanBodyBones.RightShoulder;
    }

    public override void Interact()
    {
        var shoulderTapEvent = isLeft
            ? nameof(ShoulderTapUpdateController.LeftShoulderTapped)
            : nameof(ShoulderTapUpdateController.RightShoulderTapped);

        Networking.SetOwner(localPlayer, updateControllerGameObject);
        updateController.SendCustomNetworkEvent(NetworkEventTarget.Owner, shoulderTapEvent);
    }

    private void Update()
    {
        var owner = updateController.Owner;

        if (!Utilities.IsValid(owner)|| !updateController.EnabledByPlayer) return;

        var neck = owner.GetBonePosition(HumanBodyBones.Neck);
        
        // Prefer upper arm but fallback to shoulder (since that is required for Humanoid rigs)
        var shoulder = owner.GetBonePosition(targetUpperArm);
        if (shoulder == zero)
        {
            shoulder = owner.GetBonePosition(targetShoulder);
        }

        var halfPoint = Vector3.Lerp(neck, shoulder, 0.5f);
        transform.position = halfPoint;
        transform.LookAt(neck);

        var height = Vector3.Distance(neck, shoulder);
        capsuleCollider.height = height;
        capsuleCollider.radius = height / 2f;
    }
}
