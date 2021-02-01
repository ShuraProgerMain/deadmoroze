using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowManAttack : AIAttack
{
    private Animator _animator;

    private Coroutine _isAttackCoroutine;

    private void OnEnable() 
    {
        GlobalInformation.init.SnowmanDamageChange += ChangeDamageValue;
    }

    private void OnDisable()
    {
        GlobalInformation.init.SnowmanDamageChange -= ChangeDamageValue;
    }

    private void Awake() 
    {
        _animator = GetComponent<Animator>();
    }

    public override void OnAttack()
    {
        if(_isAttackCoroutine != null)
        {
            return;
        }
        else
        {
            _isAttackCoroutine = StartCoroutine(AttackUnit(delayAttack));
        }
    }

    public override IEnumerator AttackUnit(int delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            _animator.SetTrigger("isAttack");
        }
    }
}
