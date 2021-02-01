using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamaged
{
    public void HandleDamage(int damage, Transform point = null);
}
