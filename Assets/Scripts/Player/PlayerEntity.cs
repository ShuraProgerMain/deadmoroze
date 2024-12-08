using System.Collections.Generic;
using SalvationOfSouls.Core.Interfaces;
using SalvationOfSouls.IO;
using UnityEngine;
using PlayerInput = SalvationOfSouls.IO.PlayerInput;

namespace Player
{
    public class PlayerEntity : IUpdatable
    {
        private readonly WeaponSelect _weaponSelect;
        private readonly CharacterShot _characterShot;
        private readonly CharacterMove _characterMove;
        private readonly PlayerAnimationHandler _playerAnimationHandler;
        private readonly EntityView _entityView;

        private readonly InputComponent _inputComponent;
        private readonly PlayerInput _playerInput;

        public PlayerEntity(InputComponent inputComponent, EntityView entityView,
            IReadOnlyList<WeaponSample> sampleWeapons, 
            IReadOnlyList<GameObject> weaponObjects)
        {
            _inputComponent = inputComponent;
            _entityView = entityView;
            _playerAnimationHandler = new PlayerAnimationHandler(entityView.Animator);
            
            _playerInput = inputComponent.PlayerInput;
            
            _characterShot = new CharacterShot(_playerAnimationHandler);
            _weaponSelect = new WeaponSelect(_inputComponent, _characterShot, sampleWeapons, weaponObjects);

            _characterMove = new CharacterMove(_entityView, _playerAnimationHandler);

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
