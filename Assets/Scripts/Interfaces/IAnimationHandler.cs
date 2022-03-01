using UnityEngine;
using UnityEditor;

namespace Boby
{
    public interface IAnimationHandler
    {
        bool IsAttackAnimationPlay();
        bool IsHitAnimationPlay();
        void Play(string animationName);

    }
}