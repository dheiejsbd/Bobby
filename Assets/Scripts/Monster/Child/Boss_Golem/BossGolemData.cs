using UnityEditor;
using UnityEngine;

namespace Boby
{
    [System.Serializable]
    public class BossGolemData : IMonsterData
    {
        public float MaxHP;

        //===================================================//
        //  공격 관련 변수
        //===================================================//
        public float AttackSpeed;
        public float CloseAttackRange;
        public float CloseAttackSpeed;
        //===================================================//



        //===================================================//
        //  Summon 관련 변수
        //===================================================//
        public GameObject SummonMonster;
        public Transform[] SummonPos;
        public float SummonTime;
        //===================================================//
    }
}