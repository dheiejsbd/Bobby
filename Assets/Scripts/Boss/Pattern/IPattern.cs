using UnityEditor;
using UnityEngine;

namespace Bobby
{
    public interface IPattern 
    {
        PatternID ID { get; }
        BossPatternRowData Data { get; }
        void Prepare();
        float Attack();
        float BeforeAttack();
        void AttackSuccess();
        
    }
}