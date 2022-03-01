using UnityEngine;
using UnityEditor;

namespace Boby
{
    public class Defense : IState
    {
        public Defense(GameObject owner, IMonsterBehaviourHandler handler)
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

        public int Id { get { return 10; } }

        public void Enter()
        {
            animationHandler.Play("defend");
            blackboard.HitAnimationName = "defend_hit";
        }
        public void Execute()
        {
            if (monsterBehaviourHandler.IsHited)
            {
                monsterBehaviourHandler.DoHited();
            }
        }
        public void Exit()
        {
        }
    }
}