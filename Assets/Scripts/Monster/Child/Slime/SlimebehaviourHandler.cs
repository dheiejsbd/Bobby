using UnityEditor;
using UnityEngine;

namespace Bobby
{
    public class SlimebehaviourHandler : IMonsterBehaviourHandler
    {
        public SlimebehaviourHandler(SlimeController SlimeController)
        {
            this.SlimeController = SlimeController;
        }
        protected SlimeController SlimeController;
        protected Vector3 OwnerPosition =>  SlimeController.transform.position;

        public bool IsDie { get { return SlimeController.CurHP <= 0; } }
        public bool IsHited { get; set; }

        float AttackDelay = 0;

        #region 사용X
        public bool CanAttack()
        {
            return true;
        }
        public void DoAttack()
        {

        }
        #endregion

        public bool CanTrace()
        {
            return SlimeController.DamageCauser != null;
        }

        public void DoDie()
        {
            SlimeController.Transfer(4);
            Collider[] col = SlimeController.gameObject.GetComponentsInChildren<Collider>();
            for (int i = 0; i < col.Length; i++)
            {
                col[i].enabled = false;
            }
        }

        public void DoHited()
        {
            SlimeController.Transfer(3);
        }

        public void DoTrace()
        {
            SlimeController.Transfer(10);
        }

        public void DoStop()
        {
            SlimeController.Transfer(0);
        }

        public void Update()
        {
            if (IsDie)
            {
                DoDie();
                return;
            }
            else if (CanTrace())
            {
                DoTrace();
            }
            else if(IsHited)
            {
                DoHited();
            }
            else
            {
                DoStop();
            }
        }

        public void TakeDamage()
        {
            IsHited = true;
        }
    }
}