using UnityEngine;
using UnityEngine.UI;

namespace UI.ActiveGame
{
    public class SliderHandler : MonoBehaviour
    {
        [SerializeField] private Image newHealthBar;
        
        [SerializeField] private ChristmasActionHandler _christmasAction;

        private int _maxHealth;
        private void OnEnable() 
        {
            _christmasAction.CurrentChristmasHealth += UpdateChristmasHealthSlider;
            _maxHealth = _christmasAction.healthTree;
        }

        private void OnDisable() 
        {
            _christmasAction.CurrentChristmasHealth -= UpdateChristmasHealthSlider;
        }

        private void UpdateChristmasHealthSlider(int value)
        {
            newHealthBar.fillAmount = Mathf.InverseLerp(0, _maxHealth, value);
        }
    }
}
