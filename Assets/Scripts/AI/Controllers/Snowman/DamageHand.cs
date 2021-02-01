using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHand : MonoBehaviour
{
    [SerializeField] private AIAttack _aiAttack;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.layer == 6)
        {
            var dmg = other.GetComponent<IDamaged>();

            if(dmg != null)
            {
                dmg.HandleDamage(_aiAttack.damage);
            }
        }
    }
}
