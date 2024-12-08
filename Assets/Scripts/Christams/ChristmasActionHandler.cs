using Interfaces;
using UnityEngine;

namespace Christams
{
    public class ChristmasActionHandler : MonoBehaviour, IDamaged
    {
        public int healthTree;
        [SerializeField] private int _currentHealth;

        #region Events

        public delegate void UpdateSlider(int volume);

        public UpdateSlider CurrentChristmasHealth;

        #endregion

        private void Start()
        {
            _currentHealth = healthTree;
            CurrentChristmasHealth?.Invoke(_currentHealth);
        }

        public void HandleDamage(int damage, Transform point = null)
        {
            Debug.Log(damage);
            _currentHealth -= damage;
            CurrentChristmasHealth?.Invoke(_currentHealth);
        }
    }
}
