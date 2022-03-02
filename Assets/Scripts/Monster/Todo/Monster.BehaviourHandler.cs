using UnityEngine;
using UnityEditor;

namespace Bobby
{
    public partial class MonsterController //: IMonsterBehaviourHandler
    {
        /*
        /// <summary>
        /// 
        /// </summary>
        public bool IsDie { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsHited { get; set; }

        /// <summary>
        /// 공격 할수 있는가?
        /// </summary>
        /// <returns></returns>
        public bool CanAttack()
        {
            if (DamageCauser == null) return false;

            Vector3 a = DamageCauser.position;
            Vector3 b = transform.position;

            float distance = Vector3.Distance(a, b);
            if (distance > AttackRange)
            {
                return false;
            }

            if (stateMachine.ActivatedStateId == 3)
                return false;

            return true;
        }

        /// <summary>
        /// 추적할수 있는가?
        /// </summary>
        /// <returns></returns>
        public bool CanTrace()
        {
            if (DamageCauser == null)
                return false;

            return true;
        }

        /// <summary>
        /// 사망하다
        /// </summary>
        public void DoDie()
        {
            stateMachine.Switch(4);
        }

        /// <summary>
        /// 피격 당하다
        /// </summary>
        public void DoHited()
        {
            stateMachine.Switch(3);
        }

        /// <summary>
        /// 공격하다
        /// </summary>
        public void DoAttack()
        {
            stateMachine.Switch(2);            
        }

        /// <summary>
        /// 추적하다
        /// </summary>
        public void DoTrace()
        {
            stateMachine.Switch(1);
        }

        /// <summary>
        /// 추적을 멈추다
        /// </summary>
        public void DoStop()
        {
            stateMachine.Switch(0);
        }
        */
    }
}