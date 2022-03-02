using UnityEngine;
using UnityEditor;

namespace Bobby
{
    public class Idle : IState
    {
        public Idle(GameObject owner, IMonsterBehaviourHandler handler)
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

        public int Id { get { return 0; } }

        public void Enter()
        {
            animationHandler.Play("idle");
        }
        public void Execute()
        {
            monsterBehaviourHandler.Update();
        }
        public void Exit()
        {
        }
    }
}