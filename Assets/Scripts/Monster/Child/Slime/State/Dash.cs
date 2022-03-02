using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
namespace Bobby
{
    public class Dash : IState
    {
        public Dash(GameObject owner, IMonsterBehaviourHandler handler)
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

        protected SlimeController Controller => owner.transform.GetComponent<SlimeController>();
        protected NavMeshAgent nav => owner.transform.GetComponent<NavMeshAgent>();

        float DashTimer;
        public int Id => 10;

        public void Enter()
        {
            Vector3 LookAtVector = Controller.DamageCauser.position;
            LookAtVector.y = Controller.transform.position.y;
            Controller.transform.LookAt(LookAtVector);
            animationHandler.Play("dash");
            SoundManager.instance.PlayEffect("SlimeDash");
            DashTimer = 0;
            //SoundManager.instance.PlayEffect(Controller.data.DashSound);
        }

        public void Execute()
        {
            if(monsterBehaviourHandler.IsDie)
            {
                monsterBehaviourHandler.DoDie();
                return;
            }
            else if(Controller.data.DashTime > DashTimer)
            {
                UpdateDashState();
                return;
            }
            monsterBehaviourHandler.DoStop();
        }

        public void Exit()
        {
        }

        public void UpdateDashState()
        {
            Debug.Log("Dash");
            DashTimer += Time.deltaTime;
            owner.transform.Translate(Vector3.forward * Time.deltaTime * Controller.data.DashSpeed);
        }
    }
}