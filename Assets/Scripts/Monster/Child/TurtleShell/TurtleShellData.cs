using UnityEngine;
using UnityEditor;


namespace Boby
{
    [System.Serializable]
    public class TurtleShellData : IMonsterData
    {
        public float MaxHP;

        //===================================================//
        //  공격 관련 변수
        //===================================================//
        public float AttackRange;
        public float AttackSpeed;
        //===================================================//


        //===================================================//
        //  추적 관련 변수
        //===================================================//
        public float TraceRange;
        public float TraceSpeed;
        public string TraceAnimationName;
        //===================================================//
        public Vector2 IdleTime;
    }
}