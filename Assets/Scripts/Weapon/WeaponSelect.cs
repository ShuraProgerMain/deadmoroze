using System.Collections.Generic;
using Player;
using SalvationOfSouls.IO;
using UnityEngine;

public class WeaponSelect
{
    private CharacterShot _characterShot;
    
    private readonly IReadOnlyList<WeaponSample> _sampleWeapons; 
    private readonly IReadOnlyList<GameObject> _weaponObjects;
    
    private readonly List<Weapon> _weapon = new();

    private int _selectWeapon = 0;

    public WeaponSelect(InputComponent inputComponent,
        CharacterShot characterShot, 
        IReadOnlyList<WeaponSample> sampleWeapons, 
        IReadOnlyList<GameObject> weaponObjects)
    {
        _characterShot = characterShot;
        _sampleWeapons = sampleWeapons;
        _weaponObjects = weaponObjects;

        inputComponent.PlayerInput.PlayerActions.OneWeapon.performed += _ => ChangeWeaponKeyboard(0);
        inputComponent.PlayerInput.PlayerActions.TwoWeapon.performed += _ => ChangeWeaponKeyboard(1);
        inputComponent.PlayerInput.PlayerActions.ChangeWeapon.performed += _ => ChangeWeaponGamepad();
        
        Start();
    }
    
    private void Start() 
    {
        for (var i = 0; i < _sampleWeapons.Count; i++)
        {
            _weapon.Add(_weaponObjects[i].GetComponent<Weapon>());
            _weapon[i].Initialize(_sampleWeapons[i]);
        }

        SelectActiveWealon(1);
    }

    private void ChangeWeaponKeyboard(int i)
    {
        SelectActiveWealon(i);
    }

    private void ChangeWeaponGamepad()
    {
        _selectWeapon++;

        if(_selectWeapon == 2)
        {
            _selectWeapon = 0;
        }

        SelectActiveWealon(_selectWeapon);
    }

    private void SelectActiveWealon(int value)
    {
        for(int i = 0; i < _weaponObjects.Count; i++)
        {
            _weaponObjects[i].SetActive(false);
        }

        _selectWeapon = value;

        _weaponObjects[value].SetActive(true);
        _characterShot.activeWeapon = _weapon[value];
        _characterShot.animationName = _sampleWeapons[value].animationName;
        _characterShot.timeDelay = _sampleWeapons[value].timeDelay;
    }
}

