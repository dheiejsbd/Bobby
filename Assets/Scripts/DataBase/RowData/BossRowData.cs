using UnityEditor;
using UnityEngine;

namespace Boby
{
    public class BossRowData
    {
        public int ID;
        public BossType Type;
        public string Name;
        public int Hp;

    }

    public enum BossType
    {
        None,
    }

}