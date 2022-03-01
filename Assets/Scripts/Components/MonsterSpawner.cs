using UnityEngine;

namespace Boby
{
    public class MonsterSpawner : MonoBehaviour
    {
        GameObject HpBar;
        Transform HPbarSpawnTr;
        [SerializeField] bool BossStage = false;
        public System.Action EndStage;
        public int MonsterCount {get; private set;}

        MonsterController[] monsters;
        public void Initialize(Transform Target)
        {
            HpBar = Resources.Load<GameObject>("MonsterHPbar");
            HPbarSpawnTr = GameObject.Find("MonsterHpBarGrop").transform;
            monsters = GetComponentsInChildren<MonsterController>();
            for (int i = 0; i < monsters.Length; i++)
            {
                monsters[i].SetDamageCauser(Target);
                monsters[i].enabled = false;
                monsters[i].DieEvent = MonsterDie;
                monsters[i].OnStart();
            }
            MonsterCount = monsters.Length;
        }
        /*
        public void Spawn()
        {
            for (int i = 1; i < monsters.Length; i++)
            {
                GameObject Monsterobject = Instantiate(MonsterPrefab);
                Monsterobject.transform.position = monsters[i].transform.position;
                Monsterobject.transform.rotation = monsters[i].transform.rotation;
                MonsterCount++;
                if(!BossStage)
                {
                    Monsterobject.GetComponent<MonsterController>().DieEvent = MonsterDie;
                }
            }
        }*/
        public void ActiveMob()
        {
            for (int i = 0; i < monsters.Length; i++)
            {
                monsters[i].GetComponent<MonsterController>().enabled = true;
                EnemyHpBar bar = Instantiate(HpBar, HPbarSpawnTr).GetComponent<EnemyHpBar>();
                bar.TargetTr = monsters[i].transform;
                bar.monsterController = monsters[i].GetComponent<MonsterController>();
            }
        }
        void MonsterDie()
        {
            MonsterCount--;
            if (MonsterCount > 0) return;
            Debug.Log("Clear");
            EndStage();
        }
    }
}