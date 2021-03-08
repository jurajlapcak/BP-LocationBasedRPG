using UnityEngine;

namespace LocationRPG
{
    public abstract class AnimationController
    {
        protected Animator _animator;
        protected string CurrentAnimation;
        public AnimationController(Animator _animator, string _currentAnimation)
        {
            this._animator = _animator;
            CurrentAnimation = _currentAnimation;
        }
        
        public void ChangeAnimation(string animation)
        {
            //do not overloop
            if (CurrentAnimation == animation) return;
            
            //play new animation
            _animator.Play(animation);
            
            //save currently playing animation state
            CurrentAnimation = animation;
        }
        
        
    }
}