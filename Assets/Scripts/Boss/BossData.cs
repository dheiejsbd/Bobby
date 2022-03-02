
using UnityEngine;


namespace Bobby
{
    [CreateAssetMenu(fileName = "Boss Data2", menuName = "Scriptable Object/Boss Data", order = int.MaxValue)]
    public class BossData : ScriptableObject
    {
        public float HP;
        public float Attack;
        public RandomPattern random;
    }
}