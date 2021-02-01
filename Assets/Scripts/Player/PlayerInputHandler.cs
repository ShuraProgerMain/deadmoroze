using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerInputHandler : MonoBehaviour
{
    private CharacterShot _characterShot;
    private CharacterMove _characterMove;

    private PlayerInput _playerInput;
    
    private void OnEnable() 
    {
        _playerInput.Enable();
    }

    private void OnDisable() 
    {
        _playerInput.Disable();
    }

    private void Awake() 
    {
        _playerInput = new PlayerInput();
        _characterShot = GetComponent<CharacterShot>();
        _characterMove = GetComponent<CharacterMove>();
    }

    void Update()
    {
        if(_playerInput.PlayerActions.Shoot.ReadValue<float>() > 0)
        {
            _characterShot.OnShot();
        }

        _characterMove.MoveCharacter(_playerInput.PlayerActions.Move.ReadValue<Vector2>());
        
    }
}
