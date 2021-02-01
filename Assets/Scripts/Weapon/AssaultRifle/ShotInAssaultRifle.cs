using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotInAssaultRifle : Weapon
{
    [SerializeField] private Transform _shotPoint;

    public override void Initialize(WeaponSample sample)
    {
        damage = sample.damage;
        bullet = sample.bullet;
        point = _shotPoint;
    }
}
