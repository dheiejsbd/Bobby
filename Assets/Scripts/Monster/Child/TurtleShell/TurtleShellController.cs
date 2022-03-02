using UnityEngine;
using UnityEditor;


namespace Bobby
{
    public partial class TurtleShellController : MonsterController
    {
        [SerializeField] public TurtleShellData data;

        protected TurtleShellBehaviourHandler BehaviourHandler {  get { return base.behaviourHandler as TurtleShellBehaviourHandler; } }

        public override void OnStart()
        {
            behaviourHandler = new TurtleShellBehaviourHandler(this);

            CurHP = MaxHP = data.MaxHP;
            AttackRange = data.AttackRange;
            TraceRange = data.TraceRange;
            TraceSpeed = data.TraceSpeed;
            TraceAnimationName = data.TraceAnimationName;
            HitAnimationName = "hit";

            base.OnStart();
        }

        protected override void Update()
        {
            base.Update();
        }

        public override void TakeDamage(GameObject causer, float damage, DamageType damageType)
        {
            HitParticle.transform.position = causer.transform.position;
            HitParticle.Play();

            IDamageCauser temp = causer.GetComponent<IDamageCauser>();
            damageCauser = temp.GetCauser();

            CurHP -= damage;


            DamageCauser = damageCauser.transform;
            BehaviourHandler.TakeDamage();
        }


        protected override void SetupStateMachine()
        {
            stateMachine = new StateMachine();
            stateMachine.Add(new TurtleIdle(gameObject, behaviourHandler));
            stateMachine.Add(new Trace(gameObject, behaviourHandler));
            stateMachine.Add(new Attack(gameObject, behaviourHandler));
            stateMachine.Add(new Hit(gameObject, behaviourHandler));
            stateMachine.Add(new Die(gameObject, behaviourHandler, DieEvent));
            stateMachine.Add(new Defense(gameObject, behaviourHandler));
            stateMachine.Switch(0);
        }
    }
}