using System;
using UnityEditor;
using UnityEngine;
using DG.Tweening;
namespace Boby
{

    public class BossStage : MonoBehaviour, IStage
    {
        [SerializeField] Transform Trigger;
        [SerializeField] MonsterController Boss;
        public StageID GetID()
        {
            return StageID.Boss;
        }
        public StageID GetNextID()
        {
            return StageID.Boss;
        }
        public Transform GetTrigger()
        {
            return Trigger;
        }

        public void Initialize(Action EndAction)
        {
            Boss.DieEvent = EndAction;
            Boss.OnStart();
            Boss.SetDamageCauser(GameObject.FindObjectOfType<PlayerController>().transform);
            Boss.enabled = false;
        }

        public void Exit()
        {
        }

        public void OnPrepare()
        {
        }

        public void OnStart()
        {
            Boss.enabled = true;
            BossHUDWindow HUD = GameObject.Find("Canvas").transform.Find("Boss").GetComponent<BossHUDWindow>();
            HUD.SetMonsterController(Boss);
            HUD.ShowSequence.Play();
        }

        public void OnUpdate()
        {

        }

        #region TriggerPosGizmos
        private void OnDrawGizmos()
        {
            if (!Trigger) return;
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(Trigger.position, 1);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(Trigger.position, Trigger.forward * 50);
            Gizmos.DrawRay(Trigger.position, Trigger.forward * -50);
        }
        #endregion
    }
}