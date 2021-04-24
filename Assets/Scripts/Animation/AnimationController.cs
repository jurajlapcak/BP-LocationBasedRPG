using UnityEngine;

namespace LocationRPG
{
    public class AnimationController
    {
        private Animator _animator;
        private string CurrentAnimation;

        public AnimationController(Animator _animator, string _currentAnimation)
        {
            this._animator = _animator;
            CurrentAnimation = _currentAnimation;
        }

        private void ChangeAnimation(string animation)
        {
            //do not overloop
            if (CurrentAnimation == animation) return;

           //play new animation
            _animator.Play(animation);

            //save currently playing animation state
            CurrentAnimation = animation;
            Debug.Log(CurrentAnimation);
        }
        
        public void ToggleWalking()
        {
            ChangeAnimation(AnimationConstants.WALKING);
        }

        public void ToggleIdle()
        {
            ChangeAnimation(AnimationConstants.IDLE);
        }

        public void ToggleRunning()
        {
            ChangeAnimation(AnimationConstants.RUNNING);
        }
        
        public void PlayAttack()
        {
            ChangeAnimation(AnimationConstants.ATTACK);
        }

        public void PlayDying()
        {
            ChangeAnimation(AnimationConstants.DYING);
        }
    }
}