using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
namespace Bobby
{
    public class TurtleIdle : IState
    {
        public TurtleIdle(GameObject owner, IMonsterBehaviourHandler handler)
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

        protected TurtleShellController Controller => owner.transform.GetComponent<TurtleShellController>();
        protected NavMeshAgent nav => owner.transform.GetComponent<NavMeshAgent>();

        float IdleTimer = 0;
        public int Id => 0;

        public void Enter()
        {
            animationHandler.Play("idle");
            IdleTimer = Random.Range(Controller.data.IdleTime.x, Controller.data.IdleTime.y);
        }

        public void Execute()
        {
            if (monsterBehaviourHandler.IsDie)
            {
                monsterBehaviourHandler.DoDie();
                return;
            }
            else if (0 >= IdleTimer)
            {
                monsterBehaviourHandler.Update();
                return;
            }
            IdleTimer -= Time.deltaTime;
        }

        public void Exit()
        {
        }
    }
}