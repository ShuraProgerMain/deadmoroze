using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MainHealthHandler : MonoBehaviour, IDamaged
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;
    [SerializeField] private HealthBarHandler _barHandler;

    private void OnEnable()
    {
        _currentHealth = _maxHealth;
        if (_barHandler != null)
        {
            _barHandler.Initialize(_maxHealth);
        }
    }

    public void HandleDamage(int damage, Transform point)
    {
        _currentHealth -= damage;
        _barHandler.UpdateViewHealthBar(damage);

        if (_currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }

        ShowHitEffect(point);
    }

    private void ShowHitEffect(Transform point)
    {
        ObjectPooler.init.SpawnFromPool("HitDefault", point.position, Quaternion.identity);
    }
}
