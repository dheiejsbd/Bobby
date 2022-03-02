using UnityEditor;
using UnityEngine;

namespace Bobby
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Object/Player Data", order = int.MaxValue)]
    public class PlayerData : ScriptableObject
    {
        public float HP;
        public float WalkSpeed;
        public float DashSpeed;
        public float DashTime;
        public float Attack;
        public float MaxStamina;
        public SkillID skillID;
        public float RotateSpeed;
        public float KnockBackSpeed;

        public Stamina Stamina;
        public Sound Sound;
    }


    [System.Serializable]
    public struct Stamina
    {
        public float Idle;
        public float Walk;
        public float Dash;
        public float Attack;
        public float Jump;
    }
    [System.Serializable]
    public struct Sound
    {
        public string Walk;
        public string Dash;
        public string Attack;
        public string skill;
        public string hit;
    }

}