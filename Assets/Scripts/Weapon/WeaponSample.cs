using Player;
using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(fileName = " New weapon ", menuName = " Weapon Sample ")]
    public class WeaponSample : ScriptableObject
    {
        public int damage;
        public float timeDelay;
        public Sprite weaponSprite;
        public string bullet;
        public GameObject weapon;

        public AnimationName animationName;

    }
}
