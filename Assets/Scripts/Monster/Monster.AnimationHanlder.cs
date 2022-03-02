using UnityEngine;
using UnityEditor;

namespace Bobby
{
    public partial class MonsterController : IAnimationHandler
    {
        bool isAttackerAnimationPlay;
        bool isHitAnimationPlay;

        void SetupAnimationEvents()
        {
            monsterAnimationController.OnAttackAnimationWasStart = () => 
            {
                isAttackerAnimationPlay = true; 
            };
            monsterAnimationController.OnAttackAnimationWasFinished = () => 
            {
                isAttackerAnimationPlay = false; 
            };
            monsterAnimationController.OnHitAnimationWasStart = () =>
            {
                isHitAnimationPlay = true;
            };
            monsterAnimationController.OnHitAnimationWasFinished = () =>
            {
                isHitAnimationPlay = false;
            };
            monsterAnimationController.OnFired = () =>
            {
                projectileController.Fire(DamageCauser);
            };
        }

        public bool IsAttackAnimationPlay()
        {
            return isAttackerAnimationPlay;
        }

        public bool IsHitAnimationPlay()
        {
            return isHitAnimationPlay;
        }
        public void Play(string animationName)
        {
            myAnimator.Play(animationName);
        }
    }
}