using UnityEditor;
using UnityEngine;

namespace Bobby
{
    public class BossGolemController : MonsterController
    {
        [SerializeField] public BossGolemData data;

        protected BossGolembehaviourHandler BehaviourHandler => behaviourHandler as BossGolembehaviourHandler;

        public override void OnStart()
        {
            behaviourHandler = new BossGolembehaviourHandler(this);

            CurHP = MaxHP = data.MaxHP;
            HitAnimationName = "hit";

            base.OnStart();
        }

        protected override void Update()
        {
            if (damageCauser == null)
            {
                damageCauser = GameObject.FindObjectOfType<PlayerController>()?.gameObject;
            }
            base.Update();
        }

        public override void TakeDamage(GameObject causer, float damage, DamageType damageType)
        {
            if(HitParticle)
            {
                HitParticle.transform.position = causer.transform.position;
                HitParticle.Play();
            }

            IDamageCauser temp = causer.GetComponent<IDamageCauser>();
            damageCauser = temp.GetCauser();

            CurHP -= damage;

            DamageCauser = damageCauser.transform;
        }


        protected override void SetupStateMachine()
        {
            stateMachine = new StateMachine();
            stateMachine.Add(new Idle(gameObject, behaviourHandler));
            stateMachine.Add(new Attack(gameObject, behaviourHandler));
            stateMachine.Add(new Die(gameObject, behaviourHandler, DieEvent));
            stateMachine.Add(new CloseAttack(gameObject, behaviourHandler));
            stateMachine.Add(new Summon(gameObject, behaviourHandler));
            stateMachine.Switch(0);
        }

        public override void Prepare()
        {

        }
    }
}