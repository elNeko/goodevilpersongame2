using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class SpiritAnimationHandler : MonoBehaviour {
    private SkeletonAnimation _skeleton;

    private void Start() {
        _skeleton = GetComponent<SkeletonAnimation>();
        _skeleton.AnimationState.SetAnimation(0, "idle_body", true);
        _skeleton.AnimationState.SetAnimation(1, "idle_mouth", true);

        // _skeleton.AnimationState.SetAnimation(1, "idle_head", true);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            _skeleton.AnimationState.SetAnimation(1, "bite", false);
            _skeleton.AnimationState.AddAnimation(1, "idle_mouth", true, 1f);
        }
    }
}