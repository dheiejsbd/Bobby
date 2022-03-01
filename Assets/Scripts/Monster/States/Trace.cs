using UnityEngine;
using UnityEditor;

namespace Boby
{
    public class Trace : IState
    {
        public Trace(GameObject owner, IMonsterBehaviourHandler handler)
        {
            this.owner = owner;
            blackboard = owner.GetComponent<IMonsterBlackboard>();
            movementHandler = owner.GetComponent<IMovementHandler>();
            animationHandler = owner.GetComponent<IAnimationHandler>();
            monsterBehaviourHandler = handler;
        }

        protected GameObject owner;
        protected IMonsterBlackboard blackboard;
        protected IMovementHandler movementHandler;
        protected IAnimationHandler animationHandler;
        protected IMonsterBehaviourHandler monsterBehaviourHandler;

        protected Transform My { get { return owner.transform; } }
        protected Transform Causer { get { return blackboard.DamageCauser; } }
        protected float AttackRange { get { return blackboard.AttackRange; } }
        protected string TraceAnimationName { get { return blackboard.TraceAnimationName; } }
        protected float TraceSpeed { get { return blackboard.TraceSpeed; } }

        public int Id { get { return 1; } }

        public void Enter()
        {
            movementHandler.SetSpeed(TraceSpeed);
            animationHandler.Play(TraceAnimationName);
        }
        public void Execute()
        {
            if (Causer)
            {
                if (monsterBehaviourHandler.IsHited)
                {
                    monsterBehaviourHandler.DoHited();
                    return;
                }

                Vector3 a = Causer.position;
                Vector3 b = My.position;

                float distance = Vector3.Distance(a, b);
                if (distance > AttackRange)
                {
                    movementHandler.SetDestination(Causer.position);
                }
                else
                {
                    monsterBehaviourHandler.DoStop();
                }
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
            movementHandler.Stop();
        }
    }
}