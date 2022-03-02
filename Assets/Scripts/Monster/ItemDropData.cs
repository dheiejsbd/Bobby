using UnityEditor;
using UnityEngine;

namespace Bobby
{
    public class ItemDropData : MonoBehaviour
    {
        [System.Serializable]
        public struct DataSet
        {
            [Range(0,100)]
            public float percentage;
            public GameObject Resources;
        }
        public DataSet[] data;
    }
}