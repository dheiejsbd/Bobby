using UnityEngine;
using UnityEditor;

namespace Boby
{
    public partial class MonsterController : IMonsterBlackboard
    {
        protected static Transform causer;
        public Transform DamageCauser 
        {
            get { return causer; }
            protected set { causer = value; }
        }

        public void SetDamageCauser(Transform value)
        {
            DamageCauser = value;
        }
        //===================================================//
        //  ~ 관련 변수
        //===================================================//
        public float MaxHP { get; set; }
        public float CurHP { get; set; }
        //===================================================//


        //===================================================//
        //  공격 관련 변수
        //===================================================//
        public float AttackRange { get; set; }
        public float AttackSpeed { get; set; }
        //===================================================//


        //===================================================//
        //  추적 관련 변수
        //===================================================//
        public float TraceRange { get; set; }
        public float TraceSpeed { get; set; }
        public string TraceAnimationName { get; set; }
        //===================================================//


        //===================================================//
        //  피격 관련 변수
        //===================================================//
        public string HitAnimationName { get; set; }
        //===================================================//

    }
}