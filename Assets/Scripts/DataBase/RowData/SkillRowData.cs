using UnityEditor;
using UnityEngine;

namespace Boby
{

    //SkillTable에 있는 정보를 가르키는 열거형
    public class SkillRowData
    {
        public SkillID ID;
        public string Name;
        public float Damage;
        public float CoolTime;
        public SkillType Type;
        public float Range;
        public float Duration;
        public float Speed;
        public int AttackCount;
    }
    public enum SkillID
    {
        skill1,

    }
    public enum SkillType
    { 
        None,
    }

}