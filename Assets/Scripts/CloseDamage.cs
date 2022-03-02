using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Bobby
{
    public class CloseDamage : MonoBehaviour
    {
        public float Damage;
        private void OnTriggerEnter(Collider other)
        {
            PlayerController player;
            if (other.TryGetComponent<PlayerController>(out player))
            {
                player.TakeDamage(gameObject, Damage, DamageType.NormalDamage);
                return;
            }
            if (other.transform.parent.TryGetComponent<PlayerController>(out player))
            {
                player.TakeDamage(gameObject, Damage, DamageType.NormalDamage);
            }
        }
    }

}