using UnityEditor;
using UnityEngine;

namespace Boby
{
    public class SlimeController : MonsterController
    {
        [SerializeField] public SlimeData data;

        protected SlimebehaviourHandler BehaviourHandler => behaviourHandler as SlimebehaviourHandler;
        protected float Damage { get { return GetComponentInChildren<CloseDamage>().Damage; } set { GetComponentInChildren<CloseDamage>().Damage = value; } }
        public override void OnStart()
        {
            behaviourHandler = new SlimebehaviourHandler(this);

            CurHP = MaxHP = data.MaxHP;
            HitAnimationName = "hit";

            base.OnStart();
        }

        protected override void Update()
        {
            Damage = data.Damage;
            if(damageCauser == null)
            {
                damageCauser = GameObject.Find("DragonSD_32(Clone)");
            }
            base.Update();
        }

        public override void TakeDamage(GameObject causer, float damage, DamageType damageType)
        {
            base.TakeDamage(causer, damage, damageType);
            SoundManager.instance.PlayEffect("SlimeHit");
            BehaviourHandler.TakeDamage();
        }

        protected void SetUpAnimationEvent()
        {
            
        }


        protected override void SetupStateMachine()
        {
            stateMachine = new StateMachine();
            stateMachine.Add(new SlimeIdle(gameObject, behaviourHandler));
            stateMachine.Add(new Attack(gameObject, behaviourHandler));
            stateMachine.Add(new Hit(gameObject, behaviourHandler));
            stateMachine.Add(new Dash(gameObject, behaviourHandler));
            stateMachine.Add(new Die(gameObject, behaviourHandler, DieEvent));
            stateMachine.Switch(0);
        }
    }
}