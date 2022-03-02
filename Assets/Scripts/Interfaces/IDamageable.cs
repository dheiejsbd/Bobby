
using UnityEngine;

namespace Bobby
{
    public interface IDamageable 
    {
        void TakeDamage(GameObject causer, float Damage, DamageType damageType);
    }
}