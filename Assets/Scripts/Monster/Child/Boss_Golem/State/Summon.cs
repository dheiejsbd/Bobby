using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bobby
{
    public class Summon : IState
    {
        public Summon(GameObject owner, IMonsterBehaviourHandler handler)
        {
            this.owner = owner;
            blackboard = owner.GetComponent<IMonsterBlackboard>();
            animationHandler = owner.GetComponent<IAnimationHandler>();
            monsterBehaviourHandler = handler;
            data = owner.GetComponent<BossGolemController>().data;
            HpBar = Resources.Load<GameObject>("MonsterHPbar");
            HPbarSpawnTr = GameObject.Find("MonsterHpBarGrop").transform;
        }

        protected GameObject HpBar;
        protected Transform HPbarSpawnTr;
        protected GameObject owner;
        protected IMonsterBlackboard blackboard;
        protected IAnimationHandler animationHandler;
        protected IMonsterBehaviourHandler monsterBehaviourHandler;
        BossGolemData data;

        public int Id => 11;

        public void Enter()
        {
            animationHandler.Play("CloseAttack");
            for (int i = 0; i < data.SummonPos.Length; i++)
            {
                GameObject monster = GameObject.Instantiate(data.SummonMonster, data.SummonPos[i].position, data.SummonPos[i].rotation);
                monster.GetComponent<MonsterController>().OnStart();
                EnemyHpBar bar = Object.Instantiate(HpBar, HPbarSpawnTr).GetComponent<EnemyHpBar>();
                bar.TargetTr = monster.transform;
                bar.monsterController = monster.GetComponent<MonsterController>();
            }
        }
        public void Execute()
        {
            monsterBehaviourHandler.Update();
        }
        public void Exit()
        {
        }
    }
}