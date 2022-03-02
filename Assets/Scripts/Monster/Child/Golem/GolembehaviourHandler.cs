using UnityEditor;
using UnityEngine;

namespace Bobby
{
    public class GolembehaviourHandler : IMonsterBehaviourHandler
    {
        public GolembehaviourHandler(GolemController GolemController)
        {
            this.GolemController = GolemController;
        }
        protected GolemController GolemController;
        protected Vector3 OwnerPosition => GolemController.transform.position;

        public bool IsDie { get { return GolemController.CurHP <= 0; } }
        public bool IsHited { get; set; }

        float AttackDelay = 0;


        public bool CanAttack()
        {
            if (GolemController.DamageCauser == null) return false;

            if (AttackDelay > Time.time) return false;



            Vector3 a = GolemController.DamageCauser.position;
            Vector3 b = OwnerPosition;

            float distance = Vector3.Distance(a, b);
            if (distance > GolemController.AttackRange)
            {
                return false;
            }

            return true;
        }

        public bool CanTrace()
        {
            if (GolemController.DamageCauser == null) return false;
            Vector3 a = GolemController.DamageCauser.position;
            Vector3 b = OwnerPosition;
            float distance = Vector3.Distance(a, b);
            if (distance < GolemController.AttackRange) return false;
            return true;
        }

        public void DoDie()
        {
            GolemController.Transfer(4);
        }

        public void DoHited()
        {
            GolemController.Transfer(3);
        }

        public void DoAttack()
        {
            AttackDelay = Time.time + GolemController.data.AttackSpeed;
            GolemController.Transfer(2);
        }

        public void DoCloseAttack()
        {
            GolemController.Transfer(10);
        }

        public void DoTrace()
        {
            GolemController.Transfer(1);
        }

        public void DoStop()
        {
            GolemController.Transfer(0);
        }

        public void Update()
        {
            if (IsDie)
            {
                DoDie();
                return;
            }
            else if (IsHited)
            {
                DoHited();
                return;
            }
            else if (CanAttack())
            {
                Vector3 a = GolemController.DamageCauser.position;
                Vector3 b = OwnerPosition;
                Debug.Log(GolemController.DamageCauser);
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
            else if (CanTrace())
            {
                DoTrace();
            }
            
        }

        public void TakeDamage()
        {
            IsHited = true;
        }
    }
}