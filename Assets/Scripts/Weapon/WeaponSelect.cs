using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelect : MonoBehaviour
{
    public List<Weapon> _weapon;

    [SerializeField] private CharacterShot _characterShot;
    [SerializeField] private List<WeaponSample> _sampleWeapons;
    [SerializeField] private List<GameObject> _weaponObjects;

    private int _selectWeapon = 0;

    
    
    private void Start() 
    {
        for (var i = 0; i < _sampleWeapons.Count; i++)
        {
            _weapon.Add(_weaponObjects[i].GetComponent<Weapon>());
            _weapon[i].Initialize(_sampleWeapons[i]);
        }

        SelectActiveWealon(0);
    }

    public void ChangeWeaponKeyboard(int i)
    {
        SelectActiveWealon(i);
    }

    public void ChangeWeaponGamepad()
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

