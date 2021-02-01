using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COVIDAttack : AIAttack
{
    [SerializeField] private LayerMask _layers;
    private Coroutine _isAttackCoroutine;

    private void OnEnable() 
    {
        GlobalInformation.init.COVIDMainDamageChange += ChangeDamageValue;
    }

    private void OnDisable()
    {
        GlobalInformation.init.COVIDMainDamageChange -= ChangeDamageValue;
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

            RaycastHit hit;

            if(Physics.Raycast(transform.position, transform.forward, out hit, 4, _layers))
            {
                if(hit.collider.gameObject.layer == 6)
                {
                    var dmg = hit.collider.GetComponent<IDamaged>();


                    if(dmg != null)
                    {
                        dmg.HandleDamage(damage);
                    }

                }
            }
        }
    }
}
