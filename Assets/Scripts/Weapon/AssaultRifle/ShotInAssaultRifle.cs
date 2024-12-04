using UnityEngine;

public sealed class ShotInAssaultRifle : Weapon
{
    [SerializeField] private Transform _shotPoint;

    public override void Initialize(WeaponSample sample)
    {
        damage = sample.damage;
        bullet = sample.bullet;
        point = _shotPoint;
    }

    public override void OnShot()
    {
        if (_muzzleFlash != null)
        {
            _muzzleFlash.Play();
        }
    }
}
