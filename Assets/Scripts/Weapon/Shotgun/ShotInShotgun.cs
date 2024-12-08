using Effects;
using UnityEngine;

namespace Weapon.Shotgun
{
    public class ShotInShotgun : Weapon
    {
        [SerializeField] private Transform _shotPoint;
        [SerializeField] private ShatterCam _shake;

        public override void Initialize(WeaponSample sample)
        {
            damage = sample.damage;
            bullet = sample.bullet;
            point = _shotPoint;
        }

        public override void OnShot()
        {
            base.OnShot();
            _shake.OnShake();
        }
    }
}
