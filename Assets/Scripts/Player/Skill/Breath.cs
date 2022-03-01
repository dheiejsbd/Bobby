using UnityEditor;
using UnityEngine;

namespace Boby
{
    public class Breath : ISkill
    {
        ParticleSystem FxBreath;
        public Breath(ParticleSystem Fx)
        {
            FxBreath = Fx;
        }

        public int ID => 0;
        public float Stamina { get;} = 0.05f;
        public bool single => false;
        public float CoolTime=>0;
        public string SkillAnim => "isFireSkill";

        public void Prepare()
        {

        }

        public bool CanAttack()
        {
            return true;
        }

        public void AttackTrigger()
        {
            var emission = FxBreath.emission;
            emission.enabled = true;
        }

        public void ExitTrigger()
        {
            var emission = FxBreath.emission;
            emission.enabled = false;
        }
    }
}