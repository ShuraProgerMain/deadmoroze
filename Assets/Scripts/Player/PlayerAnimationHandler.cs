using UnityEngine;

namespace Player
{
    public sealed class PlayerAnimationHandler
    {
        private readonly Animator _animator;

        public PlayerAnimationHandler(Animator animator)
        {
            _animator = animator;
        }

        public void WeaponAnimation(string name)
        {
            _animator.SetTrigger(name);
        }

        public void PoseAnimation(string name, bool state)
        {
            _animator.SetBool(name, state);
        }
    }
}
