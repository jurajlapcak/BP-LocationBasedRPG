using UnityEngine;

namespace LocationRPG
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private string _currentAnimation;

        public string CurrentAnimation
        {
            get => _currentAnimation;
            set => _currentAnimation = value;
        }

        private void ChangeAnimation(string animation)
        {
            //do not overloop
            if (_currentAnimation == animation) return;

           //play new animation
            _animator.Play(animation);

            //save currently playing animation state
            _currentAnimation = animation;
        }
        
        public void ToggleWalking()
        {
            ChangeAnimation(AnimationConstants.WALKING);
        }

        public void ToggleIdle()
        {
            ChangeAnimation(AnimationConstants.IDLE);
        }
        
        public void PlayAttack()
        {
            ChangeAnimation(AnimationConstants.ATTACK);
        }

        public void PlayHit()
        {
            ChangeAnimation(AnimationConstants.HIT);
        }
        public void PlayDying()
        {
            ChangeAnimation(AnimationConstants.DYING);
        }
    }
}