using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Boby
{
    interface ISkill
    {
        int ID { get; }
        float Stamina { get; }
        bool single { get; }
        float CoolTime { get; }
        string SkillAnim { get; }
        bool CanAttack();
        void AttackTrigger();
        void ExitTrigger();
    }
}
