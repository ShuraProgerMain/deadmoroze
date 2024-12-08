using System.Collections.Generic;
using SalvationOfSouls.DeadMoroze.Runtime.Core.Interfaces;
using SalvationOfSouls.DeadMoroze.Runtime.IO;
using SalvationOfSouls.IO;
using UnityEngine;
using Weapon;
using PlayerInput = SalvationOfSouls.IO.PlayerInput;

namespace Player
{
    public class PlayerEntity : IUpdatable
    {
        private readonly WeaponSelect _weaponSelect;
        private readonly CharacterShot _characterShot;
        private readonly CharacterMove _characterMove;
        private readonly PlayerAnimationHandler _playerAnimationHandler;
        private readonly PlayerEntityView _playerEntityView;

        private readonly InputComponent _inputComponent;
        private readonly PlayerInput _playerInput;

        public PlayerEntity(InputComponent inputComponent, PlayerEntityView playerEntityView,
            IReadOnlyList<WeaponSample> sampleWeapons, 
            IReadOnlyList<GameObject> weaponObjects)
        {
            _inputComponent = inputComponent;
            _playerEntityView = playerEntityView;
            _playerAnimationHandler = new PlayerAnimationHandler(playerEntityView.Animator);
            
            _playerInput = inputComponent.PlayerInput;
            
            _characterShot = new CharacterShot(_playerAnimationHandler);
            _weaponSelect = new WeaponSelect(_inputComponent, _characterShot, sampleWeapons, weaponObjects);

            _characterMove = new CharacterMove(_playerEntityView, _playerAnimationHandler);

            _playerInput.PlayerActions.Shoot.performed += _ => _characterShot.OnShot();
            _playerInput.PlayerActions.Shoot.canceled += _ => _characterShot.OnStopShoot();
        }
        
        public void Update(float deltaTime)
        {
            // if(_playerInput.PlayerActions.Shoot.ReadValue<float>() > 0)
            // {
            //     _characterShot.OnShot();
            // }

            _characterMove.MoveCharacter(_playerInput.PlayerActions.Move.ReadValue<Vector2>());
        }
    }
}
