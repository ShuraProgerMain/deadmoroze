using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public enum AnimationName
    {
        Walk,
        ShotShotgun,
        ShotAssaultRifle
    }

    public sealed class CharacterMove
    {
        private float _moveSpeed = 5f;
        private Vector3 _moveDirection;
        private CharacterController _controller;
        private PlayerAnimationHandler _playerAnimation;
        
        private Transform _transform;

        public CharacterMove(PlayerEntityView playerEntityView, PlayerAnimationHandler playerAnimationHandler) 
        {
            _controller = playerEntityView.CharacterController;
            _transform = playerEntityView.Transform;
            _playerAnimation = playerAnimationHandler;
            
            Gamepad a = Gamepad.current;
            Debug.Log(a);
        }

        public void MoveCharacter(Vector3 direction)
        {
            float x = direction.x;
            float y = direction.y;

            if (x != 0 || y != 0)
            {
                _playerAnimation.PoseAnimation(AnimationName.Walk.ToString(), true);
            }
            else if (x == 0 && y == 0)
            {
                _playerAnimation.PoseAnimation(AnimationName.Walk.ToString(), false);
            }


            Vector3 move = _transform.right * x + _transform.forward * y;
            _controller.Move(move * (_moveSpeed * Time.deltaTime));
        }

        public void OffsetCharacter()
        {
            
        }
    }
}