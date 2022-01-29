using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class SpiritAnimationHandler : MonoBehaviour
{
    private void Start() {
        var skeleton = GetComponent<SkeletonAnimation>();
        skeleton.AnimationState.SetAnimation(0, "idle_body", true);
        skeleton.AnimationState.SetAnimation(2, "idle_mouth", true);
        skeleton.AnimationState.SetAnimation(1, "idle_head", true);
    }
}
