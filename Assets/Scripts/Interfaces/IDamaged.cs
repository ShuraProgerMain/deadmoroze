using UnityEngine;

namespace Interfaces
{
    public interface IDamaged
    {
        public void HandleDamage(int damage, Transform point = null);
    }
}
