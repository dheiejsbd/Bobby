using UnityEditor;
using UnityEngine;

namespace Bobby
{
    [System.Serializable]
    public class SlimeData : IMonsterData
    {
        public float MaxHP;
        public float Damage;
        public DamageType DamageType;

        //===================================================//
        //  추적 관련 변수
        //===================================================//
        public float DashSpeed;
        public float DashTime;
        public string DashAnimationName;
        public string DashSound;
        //===================================================//

        public Vector2 IdleTime;
    }
}