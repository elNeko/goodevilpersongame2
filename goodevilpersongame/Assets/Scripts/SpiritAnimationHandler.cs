using Spine.Unity;
using UnityEngine;
using Animation = Spine.Animation;

public class SpiritAnimationHandler : MonoBehaviour {
    private SkeletonAnimation _skeleton;
    private Animation _animationIdleScale;
    private Animation _animationIdleMouth;
    private Animation _animationBite;
    private Animation _animationBiteIdle;

    private void Start() {

        _skeleton = GetComponent<SkeletonAnimation>();

        _animationIdleScale = _skeleton.skeleton.Data.FindAnimation("idle_scale");
        _animationIdleMouth = _skeleton.skeleton.Data.FindAnimation("idle_mouth");
        _animationBite = _skeleton.skeleton.Data.FindAnimation("bite");
        _animationBiteIdle = _skeleton.skeleton.Data.FindAnimation("bite_idle");

        SetAnimation(0, _animationIdleScale, true);
        SetAnimation(1, _animationIdleMouth, true);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            SetAnimation(1, _animationBite, false);
        }

        var current = _skeleton.AnimationState.GetCurrent(1);

        if (current == null || current.Animation == _animationBite && current.IsComplete) {
            SetAnimation(1, _animationIdleMouth, true);
        }
    }

    private void SetAnimation(int track, Animation anim, bool loop) {
        var current = _skeleton.AnimationState.GetCurrent(track);
        if (current == null || current.Animation != anim) {
            _skeleton.AnimationState.SetAnimation(track, anim, loop);
        }
    }
}