using UnityEngine;
using UnityEditor;

namespace Boby
{
    public interface IMonsterBehaviourHandler
    {
        void Update();

        /// <summary>
        /// 사망하였는가?
        /// </summary>
        bool IsDie { get;}

        /// <summary>
        /// 
        /// </summary>
        bool IsHited { get; set; }

        /// <summary>
        /// 공격 할수 있는가?
        /// </summary>
        /// <returns></returns>
        bool CanAttack();

        /// <summary>
        /// 추적할수 있는가?
        /// </summary>
        /// <returns></returns>
        bool CanTrace();

        /// <summary>
        /// 사망하다
        /// </summary>
        void DoDie();

        /// <summary>
        /// 피격 당하다
        /// </summary>
        void DoHited();

        /// <summary>
        /// 공격하다
        /// </summary>
        void DoAttack();

        /// <summary>
        /// 추적하다
        /// </summary>
        void DoTrace();

        /// <summary>
        /// 추적을 멈추다
        /// </summary>
        void DoStop();

    }
}