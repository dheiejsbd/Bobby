using UnityEditor;
using UnityEngine;

namespace Bobby
{
    public class GolemController : MonsterController
    {
        [SerializeField] public GolemData data;

        protected GolembehaviourHandler BehaviourHandler => behaviourHandler as GolembehaviourHandler;

        public override void OnStart()
        {
            behaviourHandler = new GolembehaviourHandler(this);

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
            if(damageCauser == null)
            {
                damageCauser = GameObject.Find("DragonSD_32(Clone)");
            }
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
            stateMachine.Add(new Idle(gameObject, behaviourHandler));
            stateMachine.Add(new Trace(gameObject, behaviourHandler));
            stateMachine.Add(new Attack(gameObject, behaviourHandler));
            stateMachine.Add(new Hit(gameObject, behaviourHandler));
            stateMachine.Add(new Die(gameObject, behaviourHandler, DieEvent));
            stateMachine.Add(new CloseAttack(gameObject, behaviourHandler));
            stateMachine.Switch(0);
        }
    }
}