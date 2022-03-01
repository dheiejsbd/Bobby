using UnityEngine;
using UnityEditor;

namespace Boby
{
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof(CapsuleCollider))]
    public partial class MonsterController : MonoBehaviour, IMonsterBlackboard
    {

        protected Animator myAnimator;
        protected UnityEngine.AI.NavMeshAgent navMeshAgent;
        protected ProjectileController projectileController;
        protected MonsterAnimationController monsterAnimationController;
        protected GameObject damageCauser;
        protected StateMachine stateMachine;
        protected IMonsterBehaviourHandler behaviourHandler;
        public System.Action DieEvent;

        protected bool IsDie { get { return behaviourHandler.IsDie; } }

        public virtual void Prepare()
        {

        }

        public virtual void OnStart()
        {
            void SetupComponents()
            {
                myAnimator = gameObject.GetComponentInChildren<Animator>();
                monsterAnimationController = gameObject.GetComponentInChildren<MonsterAnimationController>();
                navMeshAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
                projectileController = gameObject.GetComponentInChildren<ProjectileController>();
            }
            SetupComponents();

            DieEvent += RandomItem;
            SetupStateMachine();
            SetupAnimationEvents();
        }

        public string stateNameForDebug;

        protected virtual void Update()
        {

            //navMeshAgent.speed = moveSpeed;

            stateMachine?.Update();

            stateNameForDebug = stateMachine?.ActivatedStateName;
        }

        void Hit()
        {
            
        }

        void Die()
        {
            gameObject.SetActive(false);
        }

        public void Transfer(int id)
        {
            stateMachine.Switch(id);
        }

        public int GetActivatedStateId() { return stateMachine.ActivatedStateId; }

        protected virtual void SetupStateMachine()
        {
            stateMachine = new StateMachine();
            stateMachine.Add(new Idle(gameObject, behaviourHandler));
            stateMachine.Add(new Trace(gameObject, behaviourHandler));
            stateMachine.Add(new Attack(gameObject, behaviourHandler));
            stateMachine.Add(new Hit(gameObject, behaviourHandler));
            stateMachine.Add(new Die(gameObject, behaviourHandler, DieEvent));
            stateMachine.Switch(0);
        }


        void RandomItem()
        {
            ItemDropData Items = gameObject.GetComponent<ItemDropData>();
            float Max = 0;
            for (int i = 0; i < Items.data.Length; i++)
            {
                Max += Items.data[i].percentage;
            }

            float r = Random.Range(0.0f, Max);

            for (int i = 0; i < Items.data.Length; i++)
            {
                r -= Items.data[i].percentage;
                if(r <= 0)
                {
                    if (Items.data[i].Resources == null) return;
                    Instantiate(Items.data[i].Resources, transform.position + Vector3.up, Quaternion.Euler(Vector3.zero));
                    return;
                }
            }
        }
    }
}