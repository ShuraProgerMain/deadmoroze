using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarHandler : MonoBehaviour
{
    // [SerializeField] public float _maxHealth;
    private Renderer _healthBarRenderer;
    private float _currentHealth;
    private float _nextHealthValue;
    private float _lastCorValue;
    // private float _lastDamage;

    private Coroutine _animationHealtBar;

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Awake()
    {
        _healthBarRenderer = GetComponent<Renderer>();
    }

    public void Initialize(float maxHealth)
    {
        _currentHealth = maxHealth;
        _lastCorValue = _currentHealth;
        if (_healthBarRenderer != null)
        {
            _healthBarRenderer.material.SetFloat("_MaxCountBar", maxHealth);
            _healthBarRenderer.material.SetFloat("_CurrentValue", _currentHealth);
        }
        _nextHealthValue = _currentHealth;
    }

    public void UpdateViewHealthBar(float damageValue)
    {
        if (_animationHealtBar != null)
        {
            _currentHealth = _nextHealthValue;
            _healthBarRenderer.material.SetFloat("_CurrentValue", _currentHealth);
            StopCoroutine(_animationHealtBar);
        }

        _nextHealthValue -= damageValue;
        _animationHealtBar = StartCoroutine(AnimationHealthBar(damageValue));

        Debug.Log("Last Value: " + _lastCorValue + " \n " + " and Current: " + _currentHealth);
    }

    private IEnumerator AnimationHealthBar(float damage)
    {
        _currentHealth -= damage;

        var i = 1;
        while (_lastCorValue > _currentHealth)
        {
            i++;
            _lastCorValue = Mathf.MoveTowards(_lastCorValue, _currentHealth, i / 2);
            _healthBarRenderer.material.SetFloat("_CurrentValue", _lastCorValue);
            yield return new WaitForSeconds(0.05f);
        }
    }
}