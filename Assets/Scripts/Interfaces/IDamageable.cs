
using UnityEngine;

namespace Boby
{
    public interface IDamageable 
    {
        void TakeDamage(GameObject causer, float Damage, DamageType damageType);
    }
}