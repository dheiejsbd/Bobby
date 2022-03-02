using UnityEngine;
using UnityEditor;

namespace Bobby
{
    public interface IDamageCauser 
    {
        GameObject GetCauser();
    }
}