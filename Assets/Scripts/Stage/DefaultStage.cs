using System;
using UnityEditor;
using UnityEngine;

namespace Bobby
{

    public class DefaultStage : MonoBehaviour, IStage
    {
        [SerializeField] StageID ID;
        [SerializeField] StageID NextID;
        [SerializeField] Transform Trigger;
        [SerializeField] MonsterSpawner MonsterSpawner;
        [SerializeField] Animator DoorAnimatorOpen;
        [SerializeField] Animator DoorAnimatorClose;
        [SerializeField] string OpenAnimName = "Open";
        [SerializeField] string CloseAnimName = "Close";

        public StageID GetID()
        {
            return ID;
        }
        public StageID GetNextID()
        {
            return NextID;
        }
        public Transform GetTrigger()
        {
            return Trigger;
        }

        public void Initialize(Action EndAction)
        {
            if (DoorAnimatorOpen == null) DoorAnimatorOpen = DoorAnimatorClose;
            MonsterSpawner.Initialize(GameObject.FindObjectOfType<PlayerController>().transform);
            MonsterSpawner.EndStage = EndAction;
        }

        public void Exit()
        {
            Debug.Log("CloseAnimator");
            DoorAnimatorClose.Play(CloseAnimName);
        }

        public void OnPrepare()
        {
            Debug.Log("Open");
            DoorAnimatorOpen?.Play(OpenAnimName);
        }

        public void OnStart()
        {
            MonsterSpawner.ActiveMob();
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