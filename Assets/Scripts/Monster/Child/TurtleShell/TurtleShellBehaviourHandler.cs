using UnityEngine;
using UnityEditor;

namespace Boby
{
    public class TurtleShellBehaviourHandler : IMonsterBehaviourHandler
    {
        public TurtleShellBehaviourHandler(TurtleShellController turtleShell)
        {
            this.turtleShell = turtleShell;
        }
        protected TurtleShellController turtleShell;
        protected Vector3 OwnerPosition { get { return turtleShell.transform.position; } }

        public bool IsDie { get { return turtleShell.CurHP <= 0; } }
        public bool IsHited { get; set; }

        protected int HitCount;

        public bool CanAttack()
        {
            if (turtleShell.DamageCauser == null) return false;

            Vector3 a = turtleShell.DamageCauser.position;
            Vector3 b = OwnerPosition;

            float distance = Vector3.Distance(a, b);
            if (distance > turtleShell.AttackRange)
            {
                return false;
            }

            //if (stateMachine.ActivatedStateId == 3)
            //    return false;

            return true;
        }

        public bool CanTrace()
        {
            if (turtleShell.DamageCauser == null)
                return false;

            return true;
        }

        public bool CanDefend()
        {
            if (2 > HitCount) return false;
            if (turtleShell.GetActivatedStateId() == 10)
                return false;
            
            return true;
        }

        public void DoDie()
        {
            turtleShell.Transfer(4);
        }

        public void DoHited()
        {
            turtleShell.Transfer(3);
        }

        public void DoAttack()
        {
            turtleShell.Transfer(2);
        }

        public void DoTrace()
        {
            turtleShell.Transfer(1);
        }

        public void DoStop()
        {
            turtleShell.Transfer(0);
        }

        public void DoDefend()
        {
            turtleShell.Transfer(10);
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
            //else if (CanDefend())
            //{
            //    DoDefend();
            //}
            else if (CanAttack())
            {
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
            ++HitCount;
            IsHited = true;
        }
    }
}