using Christams;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ActiveGame
{
    public class SliderHandler : MonoBehaviour
    {
        [SerializeField] private Image newHealthBar;
        [SerializeField] private Image contrastHealthBar;
        
        [SerializeField] private ChristmasActionHandler _christmasAction;

        private int _maxHealth;
        private float _maxAwaitTime = 1f;
        private float _currentAwaitTime;
        private bool _contrastIsBusy;
        
        private int _currentHealth = 2000;
        
        private void OnEnable() 
        {
            _christmasAction.CurrentChristmasHealth += UpdateChristmasHealthSlider;
            _maxHealth = _christmasAction.healthTree;
            _currentHealth = _maxHealth;
        }

        private void OnDisable() 
        {
            _christmasAction.CurrentChristmasHealth -= UpdateChristmasHealthSlider;
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _currentHealth -= 100;
                UpdateChristmasHealthSlider(_currentHealth);
            }
        }
        
        private void UpdateChristmasHealthSlider(int value)
        {
            newHealthBar.fillAmount = Mathf.InverseLerp(0, _maxHealth, value);
            
            if (_contrastIsBusy)
            {
                _currentAwaitTime = _maxAwaitTime;
                return;
            }
            
            ShowContrast();
        }

        private async Awaitable ShowContrast()
        {
            if (_contrastIsBusy)
            {
                _currentAwaitTime = _maxAwaitTime;
                await Awaitable.NextFrameAsync();
            }
            
            _contrastIsBusy = true;
            _currentAwaitTime = _maxAwaitTime;

            Debug.Log($"currentAwaitTime {_currentAwaitTime}");
            while (_currentAwaitTime >= 0)
            {
                _currentAwaitTime -= Time.deltaTime;
                await Awaitable.NextFrameAsync();
            }
            
            contrastHealthBar.fillAmount = newHealthBar.fillAmount;
            _contrastIsBusy = false;
        }
    }
}
