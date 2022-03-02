using UnityEngine;
using UnityEditor;

namespace Bobby
{
    public interface IAnimationHandler
    {
        bool IsAttackAnimationPlay();
        bool IsHitAnimationPlay();
        void Play(string animationName);

    }
}