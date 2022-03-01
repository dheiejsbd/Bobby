using UnityEngine;
using UnityEditor;

namespace Boby
{
    public class MonsterAnimationController : MonoBehaviour
    {
        public System.Action OnAttackAnimationWasStart;
        public System.Action OnAttackAnimationWasFinished;
        public System.Action OnHitAnimationWasStart;
        public System.Action OnHitAnimationWasFinished;
        public System.Action OnFired;

        public void AttackAnimationWasStart() { OnAttackAnimationWasStart?.Invoke(); }
        public void AttackAnimationWasFinished() { OnAttackAnimationWasFinished?.Invoke(); }
        public void HitAnimationWasStart() { OnHitAnimationWasStart?.Invoke(); }
        public void HitAnimationWasFinished() { OnHitAnimationWasFinished?.Invoke(); }
        public void Fire() { OnFired?.Invoke(); }

    }
}