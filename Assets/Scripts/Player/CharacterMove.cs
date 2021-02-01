using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// [RequireComponent(typeof(Rigidbody))]

public enum AnimationName
{
    Walk,
    ShotShotgun,
    ShotAssaultRifle
}
public class CharacterMove : MonoBehaviour
{
    public InputActionAsset _input;
    [SerializeField] private float _moveSpeed;
    private Vector3 _moveDirection;
    private CharacterController _controller;
    private PlayerAnimationHandler _playerAnimation;


    private void Awake() 
    {
        _controller = GetComponent<CharacterController>();
        _playerAnimation = GetComponent<PlayerAnimationHandler>();

        var a = Gamepad.current;
        Debug.Log(a);
    }

    public void MoveCharacter(Vector3 direction)
    {
        var x = direction.x;
        var y = direction.y;

        if(x != 0 || y != 0)
        {
            _playerAnimation.PoseAnimation(AnimationName.Walk.ToString(), true);
        }
        else if(x == 0 && y == 0)
        {
            _playerAnimation.PoseAnimation(AnimationName.Walk.ToString(), false);
        }


        Vector3 move = transform.right * x + transform.forward * y;
        _controller.Move(move * _moveSpeed * Time.deltaTime);
    }

    public void OffsetCharacter()
    {
        //shatter effect
    }
}
