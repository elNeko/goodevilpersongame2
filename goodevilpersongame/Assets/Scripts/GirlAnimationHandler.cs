using Spine.Unity;
using UnityEngine;
using Animation = Spine.Animation;

namespace DefaultNamespace {

    public class GirlAnimationHandler : MonoBehaviour {
        public Transform deathBG;
        public DeathColliderHandler death;

        private bool _isDead;
        private Rigidbody _rb;
        private SkeletonAnimation _skeleton;
        private bool _wasAirborne;
        private Vector3 _initialScale;
        private Animation _animationFall;
        private Animation _animationRise;
        private Animation _animationRun;
        private Animation _animationIdle;
        private Animation _animationLand;
        private Animation _animationDie;

        private void Start() {
            death.dead.AddListener(Die);

            _initialScale = transform.localScale;
            _rb = GetComponentInParent<Rigidbody>();

            _skeleton = GetComponent<SkeletonAnimation>();

            _animationDie = _skeleton.skeleton.Data.FindAnimation("die");
            _animationLand = _skeleton.skeleton.Data.FindAnimation("land");
            _animationIdle = _skeleton.skeleton.Data.FindAnimation("idle_2");
            _animationFall = _skeleton.skeleton.Data.FindAnimation("fall");
            _animationRise = _skeleton.skeleton.Data.FindAnimation("rise");
            _animationRun = _skeleton.skeleton.Data.FindAnimation("run");

            SetAnimation(_animationIdle, true);
        }

        private void Update() {
            if (_isDead) return;
            
            var grounded = Physics.Raycast(_rb.position, -Vector3.up, 1f);

            var isMovingX = Mathf.Abs(_rb.velocity.x) > 0.1f;

            if (isMovingX) {
                var scale = new Vector3(Mathf.Sign(_rb.velocity.x) * 1f, 1f, 1f);
                transform.localScale = Vector3.Scale(_initialScale, scale);
            } else {
                if (Input.GetKey(KeyCode.D)) {
                    var scale = new Vector3(1f, 1f, 1f);
                    transform.localScale = Vector3.Scale(_initialScale, scale);
                }

                if (Input.GetKey(KeyCode.A)) {
                    var scale = new Vector3(-1f, 1f, 1f);
                    transform.localScale = Vector3.Scale(_initialScale, scale);
                }
            }

            if (grounded) {
                if (_wasAirborne) {
                    _wasAirborne = false;
                    SetAnimation(_animationLand, false);
                } else {

                    var current = _skeleton.AnimationState.GetCurrent(0);

                    if (current.Animation != _animationLand || current.IsComplete) {
                        if (isMovingX || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) {
                            SetAnimation(_animationRun, true);
                        } else {
                            SetAnimation(_animationIdle, true);
                        }
                    }

                }

            } else {
                _wasAirborne = true;

                if (_rb.velocity.y < 0) {
                    SetAnimation(_animationFall, true);
                } else {
                    SetAnimation(_animationRise, true);
                }
            }
        }

        private void Die() {
            _isDead = true;
            deathBG.gameObject.SetActive(true);
            SetAnimation(_animationDie, false);
        }

        private void SetAnimation(Animation anim, bool loop) {
            if (_skeleton.AnimationState.GetCurrent(0).Animation != anim) {
                _skeleton.AnimationState.SetAnimation(0, anim, loop);
            }
        }
    }

}