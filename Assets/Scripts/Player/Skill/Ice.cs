using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
namespace Bobby
{
    class Ice:ISkill
    {
        public int ID => 2;
        public float Stamina => 10;
        public bool single => true;
        public float CoolTime => 10;
        public string SkillAnim => "isHealing";

        float Range = 10;
        float Slow = 0.3f;
        float SlowTime = 5;

        GameObject owner;
        ParticleSystem Fx;

        public Ice(GameObject owner, ParticleSystem Fx)
        {
            this.owner = owner;
            this.Fx = Fx;
        }

        public void AttackTrigger()
        {
            Collider[] coll = Physics.OverlapSphere(owner.transform.position, Range, 1<<14);
            for (int i = 0; i < coll.Length; i++)
            {
                NavMeshAgent agent;
                if(coll[i].TryGetComponent<NavMeshAgent>(out agent))
                {
                    agent.enabled = false;
                    GameObject obj = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefabs/Ice"), coll[i].transform.position, coll[i].transform.rotation);
                    var monster = coll[i].GetComponent<MonsterController>();
                    Coroutine.instance.StartCoroutine(back(monster, agent, obj));
                }
            }
            Fx.Play();
        }

        IEnumerator back(MonsterController monster, NavMeshAgent agent, GameObject obj)
        {
            float speed = agent.speed;
            agent.speed = 0;
            yield return new WaitForSeconds(SlowTime);
            monster.enabled = true;
            agent.speed = speed;
            UnityEngine.Object.Destroy(obj);
        }

        public bool CanAttack()
        {
            return true;
        }

        public void ExitTrigger()
        {

        }
    }
}
