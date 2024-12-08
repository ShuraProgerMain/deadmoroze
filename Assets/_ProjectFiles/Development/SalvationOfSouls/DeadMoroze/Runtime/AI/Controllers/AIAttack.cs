using System.Collections;
using UnityEngine;

namespace SalvationOfSouls.DeadMoroze.Runtime.AI.Controllers
{
	public abstract class AIAttack : MonoBehaviour
    {
        public int damage;
        public int delayAttack;
        private Coroutine _isAttackCoroutine;

        public abstract void OnAttack();

        public abstract IEnumerator AttackUnit(int delay);

        public void ChangeDamageValue(int value)
        {
            damage = value;
        }

        private void OnParticleCollision(GameObject other)
        {
            Debug.Log(other.name);
        }
    }
}