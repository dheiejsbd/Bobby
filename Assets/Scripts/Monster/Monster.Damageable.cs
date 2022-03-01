using UnityEngine;
using UnityEditor;

namespace Boby
{
    public partial class MonsterController : IDamageable
    {
        [SerializeField] protected ParticleSystem HitParticle;

        public virtual void TakeDamage(GameObject causer, float damage, DamageType damageType)
        {
            if (!GetComponent<MonsterController>().enabled) return;
            HitParticle.transform.position = causer.transform.position;
            HitParticle.Play();

            IDamageCauser temp = causer.GetComponent<IDamageCauser>();
            damageCauser = temp.GetCauser();

            CurHP -= damage;

            if (0 > CurHP)
            {
                //IsDie = true;
            }
            else
            {
                DamageCauser = damageCauser.transform;
                //IsHited = true;
            }
        }

        public void OnParticleCollision(GameObject other)
        {
            if (!GetComponent<MonsterController>().enabled) return;
            FXDamage FX = other.GetComponent<FXDamage>();
            CurHP -= FX.Damage;
            DamageCauser = other.transform;
        }
    }
}