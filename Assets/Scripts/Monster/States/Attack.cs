using UnityEngine;
using UnityEditor;

namespace Bobby
{
    public class Attack : IState
    {
        public Attack(GameObject owner, IMonsterBehaviourHandler handler)
        {
            this.owner = owner;
            blackboard = owner.GetComponent<IMonsterBlackboard>();
            animationHandler = owner.GetComponent<IAnimationHandler>();
            monsterBehaviourHandler = handler;
        }

        protected GameObject owner;
        protected IMonsterBlackboard blackboard;
        protected IAnimationHandler animationHandler;
        protected IMonsterBehaviourHandler monsterBehaviourHandler;

        public int Id { get { return 2; } }

        public void Enter()
        {
            owner.transform.LookAt(blackboard.DamageCauser.transform);
            animationHandler.Play("attack");
        }
        public void Execute()
        {
            if (!animationHandler.IsAttackAnimationPlay())
            {
                monsterBehaviourHandler.DoStop();
            }
            else
            {
                if (monsterBehaviourHandler.IsDie)
                {
                    monsterBehaviourHandler.DoDie();
                    return;
                }
                if (monsterBehaviourHandler.IsHited)
                {
                    monsterBehaviourHandler.DoHited();
                    return;
                }
            }
        }
        public void Exit()
        {
        }
    }
}