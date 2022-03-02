using UnityEditor;
using UnityEngine;

namespace Bobby
{
    [System.Serializable]
    public class GolemData : IMonsterData
    {
        public float MaxHP;

        //===================================================//
        //  공격 관련 변수
        //===================================================//
        public float AttackRange;
        public float AttackSpeed;
        public float CloseAttackRange;
        public float CloseAttackSpeed;
        //===================================================//


        //===================================================//
        //  추적 관련 변수
        //===================================================//
        public float TraceRange;
        public float TraceSpeed;
        public string TraceAnimationName;
        //===================================================//


    }
}