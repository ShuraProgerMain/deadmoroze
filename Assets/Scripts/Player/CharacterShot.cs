using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterShot : MonoBehaviour
{
    public float timeDelay;
    
    public Weapon activeWeapon;

    public AnimationName animationName;

    private PlayerAnimationHandler _playerAnimation;

    private bool _weaponIsLoaded = true;


    private void Awake()
    {
        _playerAnimation = GetComponent<PlayerAnimationHandler>();
    }

    public void asd(InputAction.CallbackContext context)
    {
        if(context.action.ReadValue<float>() > 0)
        {
            OnShot();
        }
    }

    public void OnShot()
    {
        if(!_weaponIsLoaded) return;

        _weaponIsLoaded = false;
        activeWeapon.OnShot();
        _playerAnimation.WeaponAnimation(animationName.ToString());
        StartCoroutine(ReloadWeapon(timeDelay));
    }

    private IEnumerator ReloadWeapon(float time)
    {
        yield return new WaitForSeconds(time);

        _weaponIsLoaded = true;
    }

}
