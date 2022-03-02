using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bobby
{
    class Ice:ISkill
    {
        public int ID => 2;
        public float Stamina => 10;
        public bool single => true;
        public float CoolTime => 5;
        public string SkillAnim => "isHealing";

        float HealingAmount = 50;


        GameObject owner;
        ParticleSystem Fx;

        public Ice(GameObject owner, ParticleSystem Fx)
        {
            this.owner = owner;
            this.Fx = Fx;
        }

        public void AttackTrigger()
        {
            owner.GetComponent<PlayerController>().HpControl(HealingAmount);
            Fx.Play();
        }

        public bool CanAttack()
        {
            return true;
        }

        public void ExitTrigger()
        {

        }
    }
}
