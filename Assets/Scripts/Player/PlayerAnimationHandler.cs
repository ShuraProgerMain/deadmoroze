using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
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
