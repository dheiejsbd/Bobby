using System;
using UnityEditor;
using UnityEngine;

namespace Boby
{
    public interface IStage
    {
        StageID GetID();
        StageID GetNextID();
        Transform GetTrigger();
        /// <summary>
        /// 초기화
        /// </summary>
        void Initialize(Action EndAction);
        /// <summary>
        /// 스테이지 입장
        /// </summary>
        void OnPrepare();
        /// <summary>
        /// 스테이지 시작
        /// </summary>
        void OnStart();
        /// <summary>
        /// 스테이지 진행
        /// </summary>
        void OnUpdate();
        /// <summary>
        /// 스테이지 종료
        /// </summary>
        void Exit();
    }
}