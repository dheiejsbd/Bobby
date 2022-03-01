using UnityEditor;
using UnityEngine;

namespace Boby
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