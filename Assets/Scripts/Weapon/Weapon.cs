using ObjectPool;
using UnityEngine;

namespace Weapon
{
    public abstract class Weapon : MonoBehaviour
    {
        public ParticleSystem _muzzleFlash;

        [HideInInspector] public int damage;
        [HideInInspector] public string bullet;
        [HideInInspector] public Transform point;


        public abstract void Initialize(WeaponSample sample);

        public virtual void OnShot()
        {
            if(_muzzleFlash != null)
            {
                _muzzleFlash.Play();
            }
        
            ObjectPooler.init.SpawnFromPool(bullet, point.position, point.rotation);
        }

        public virtual void OnStopShot()
        {
        
        }
    }
}
