using UnityEngine;
using UnityEditor;

namespace Boby
{
    public class Die : IState
    {
        public Die(GameObject owner, IMonsterBehaviourHandler handler, System.Action DieEvent)
        {
            this.owner = owner;
            blackboard = owner.GetComponent<IMonsterBlackboard>();
            animationHandler = owner.GetComponent<IAnimationHandler>();
            monsterBehaviourHandler = handler;
            this.DieEvent = DieEvent;
        }

        protected GameObject owner;
        protected IMonsterBlackboard blackboard;
        protected IAnimationHandler animationHandler;
        protected IMonsterBehaviourHandler monsterBehaviourHandler;
        protected System.Action DieEvent;
        public int Id { get { return 4; } }

        public void Enter()
        {
            owner.transform.LookAt(blackboard.DamageCauser.transform);
            owner.GetComponent<CapsuleCollider>().enabled = false;
            owner.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            DieEvent?.Invoke();
            animationHandler.Play("die");
        }

        void DropItem()
        {

        }

        public void Execute()
        {
        }
        public void Exit()
        {
        }
    }
}