using UnityEngine;
using UnityEditor;

namespace Bobby
{
    public class Hit : IState
    {
        public Hit(GameObject owner, IMonsterBehaviourHandler handler)
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

        public int Id { get { return 3; } }

        public void Enter()
        {
            owner.transform.LookAt(blackboard.DamageCauser.transform);
            animationHandler.Play(blackboard.HitAnimationName);
        }
        public void Execute()
        {
            if (monsterBehaviourHandler.IsDie)
            {
                monsterBehaviourHandler.DoDie();
                return;
            }
            if (!animationHandler.IsHitAnimationPlay())
            {
                monsterBehaviourHandler.DoStop();
            }
        }
        public void Exit()
        {
            monsterBehaviourHandler.IsHited = false;
        }
    }
}