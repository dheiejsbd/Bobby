using UnityEditor;
using UnityEngine;

namespace Bobby
{
    public class BossGolembehaviourHandler : IMonsterBehaviourHandler
    {
        public BossGolembehaviourHandler(BossGolemController BossGolemController)
        {
            this.BossGolemController = BossGolemController;
        }
        protected BossGolemController BossGolemController;
        protected Vector3 OwnerPosition => BossGolemController.transform.position;

        public bool IsDie { get { return BossGolemController.CurHP <= 0; } }
        public bool IsHited { get; set; }

        protected float SummonTime => BossGolemController.data.SummonTime;
        protected float SummonTimer = 20;
        protected float AttackTime => BossGolemController.data.AttackSpeed;
        protected float AttackTimer = 10;

        #region Can
        public bool CanAttack()
        {
            if (BossGolemController.DamageCauser == null) return false;

            /*
            Vector3 a = BossGolemController.DamageCauser.position;
            Vector3 b = OwnerPosition;

            float distance = Vector3.Distance(a, b);
            if (distance > BossGolemController.AttackRange)
            {
                return false;
            }
            */
            return true;
        }

        public bool CanTrace()
        {
            if (BossGolemController.DamageCauser == null)
                return false;

            return true;
        }
        public bool CanSummon()
        {
            if(SummonTimer <= Time.time)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Do
        public void DoDie()
        {
            BossGolemController.Transfer(4);
        }

        public void DoHited()
        {
            BossGolemController.Transfer(3);
        }

        public void DoAttack()
        {
            BossGolemController.Transfer(2);
        }

        public void DoCloseAttack()
        {
            BossGolemController.Transfer(10);
        }

        public void DoTrace()
        {
            BossGolemController.Transfer(1);
        }

        public void DoStop()
        {
            BossGolemController.Transfer(0);
        }

        public void DoSummon()
        {
            SummonTimer = SummonTime + Time.time;
            BossGolemController.Transfer(11);
        }

        public void DoIdle()
        {
            BossGolemController.Transfer(0);
        }
        #endregion

        public void Update()
        {
            Debug.Log(BossGolemController.DamageCauser);
            if (IsDie)
            {
                DoDie();
                return;
            }
            if(!CanAttack())
            {
                return;
            }
            else if(CanSummon())
            {
                DoSummon();
                return;
            }
            else
            {
                Vector3 a = BossGolemController.DamageCauser.position;
                Vector3 b = OwnerPosition;
                Debug.Log(BossGolemController.DamageCauser);
                float distance = Vector3.Distance(a, b);


                //DoCloseAttack();
                //return;
                //if (distance > GolemController.data)
                //{
                //    return;
                //}

                DoAttack();

                return;
            }
        }
    }
}