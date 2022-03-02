using UnityEditor;
using UnityEngine;

namespace Bobby
{
    public class Trigger : MonoBehaviour
    {
        public enum Type
        {
            shop,
        }
        [SerializeField] Type Key;
        public Type GetKey()
        {
            return Key;
        }
    }
}