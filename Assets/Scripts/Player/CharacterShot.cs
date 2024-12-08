using System.Collections;
using System.Threading.Tasks;
using SalvationOfSouls.Core.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class CharacterShot
    {
        public float timeDelay;
        public Weapon activeWeapon;
        public AnimationName animationName;
        
        private readonly PlayerAnimationHandler _playerAnimation;
        
        private bool _weaponIsLoaded = true;

        public CharacterShot(PlayerAnimationHandler playerAnimation)
        {
            _playerAnimation = playerAnimation;
        }

        public void OnShot()
        {
            if(!_weaponIsLoaded) return;

            _weaponIsLoaded = false;
            activeWeapon.OnShot();
            _playerAnimation.WeaponAnimation(animationName.ToString());
            // StartCoroutine(ReloadWeapon(timeDelay));
            ReloadWeapon(timeDelay).ContinueWith(_ => Debug.Log("Reload completed"));
        }

        public void OnStopShoot()
        {
            activeWeapon.OnStopShot();
        }

        private async Task ReloadWeapon(float time)
        {
            await Awaitable.WaitForSecondsAsync(time);

            _weaponIsLoaded = true;
        }
    }
}
