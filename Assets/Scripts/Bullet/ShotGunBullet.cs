using System.Collections;
using SalvationOfSouls.DeadMoroze.Runtime.AI.Controllers;
using UnityEngine;

namespace Bullet
{
    public class ShotGunBullet : MonoBehaviour
    {
        public GameObject _hit;
        private ParticleSystem _particle;

        private float _timeDelay;

        private void Start()
        {
            _timeDelay = GetComponent<ParticleSystem>().main.duration;
        }

        void OnBecameVisible()
        {
            StartCoroutine(dontActive());
        }

        private IEnumerator dontActive()
        {
            yield return new WaitForSeconds(_timeDelay);
            gameObject.SetActive(false);
        }

        private void OnParticleCollision(GameObject other)
        {
            if (other.layer == 7)
            {
                other.GetComponent<MainHealthHandler>().HandleDamage(10, transform);
            }
        }
    }
}
