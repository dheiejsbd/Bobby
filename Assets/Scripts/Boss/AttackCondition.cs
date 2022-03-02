using UnityEngine;
using UnityEditor;

namespace Bobby
{
    /// <summary>
    /// 
    /// </summary>
    public class AttackCondition 
    {
        private bool isTrue;
        private float isTime = 3f;
        public float isTimer;
        public float BossSkillHp = 0.5f;

        public bool NomalUpdate()
        {
            isTimer += Time.deltaTime;
            if (isTimer > isTime)
            {
                Debug.Log("기본 공격");
                return true;
            }
            return false;
        }

        public void Update(BossController boss)
        {
            float val = boss.MaxHP * BossSkillHp;
            if (val >= boss.CurHP)
            {
                Debug.Log("스킬 공격");
                isTrue = true;
            }
        }

        public bool IsTrue()
        {
            bool a = isTrue;
            isTrue = false;
            return a;
        }
    }
}