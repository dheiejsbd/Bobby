using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Boby
{
    public enum StageID
    {
        Lobby,
        stage1,
        stage2,
        stage3,
        stage4,
        stage5,
        stage6,
        Boss
    }
    public class StageManager : MonoBehaviour
    {
        Dictionary<StageID, IStage> Stages = new Dictionary<StageID, IStage>();
        StageID ActiveStageID;
        Action EndStage;
        public Action EndGame;
        bool Clear = true;
        PlayerController PlayerController;

        #region Events
        public void Initialize()
        {
            PlayerController = GameObject.FindObjectOfType<PlayerController>();
            EndStage = ClearEvent;
            StageInitialize();
            ActiveStageID = StageID.Lobby;
            Stages[ActiveStageID].OnPrepare();
        }
        void StageInitialize()
        {
            IStage[] IStages = GetComponentsInChildren<IStage>();

            for (int i = 0; i < IStages.Length; i++)
            {
                if (!Stages.ContainsKey(IStages[i].GetID()))
                {
                    Debug.Log("Adds : "+IStages[i].GetID());
                    IStages[i].Initialize(EndStage);
                    Stages.Add(IStages[i].GetID(), IStages[i]);
                }
            }
        }

        public void Prepare()
        {

        }
        public void OnUpdate()
        {
            if(Clear)
            {
                if(EnterStage())
                {
                    StartCoroutine(ChangeStage());
                    return;
                }
            }

            Stages[ActiveStageID].OnUpdate();
        }
        bool EnterStage()
        {
            return PlayerController.transform.position.x > Stages[Stages[ActiveStageID].GetNextID()].GetTrigger().position.x;
        }
        IEnumerator ChangeStage()
        {
            Clear = false;
            PlayerController.enabled = false;
            for (int i = 0; i < PlayerController.GetComponent<Animator>().parameterCount; i++)
            {
                PlayerController.GetComponent<Animator>().SetBool(PlayerController.GetComponent<Animator>().parameters[i].name, false);
            }
            Stages[ActiveStageID].Exit();
            ActiveStageID = Stages[ActiveStageID].GetNextID();
            yield return new WaitForSeconds(4);
            PlayerController.enabled = true;
            yield return new WaitForSeconds(1);
            Stages[ActiveStageID].OnStart();
        }


        public void OnLateUpdate()
        {

        }
        #endregion

        #region StageEvent
        void ClearEvent()
        {
            Clear = true;
            if (Stages[ActiveStageID].GetID() == StageID.Boss)
            {
                EndGame();
                return;
            }
            Stages[ActiveStageID].OnPrepare();
        }

        #endregion
    }
}
