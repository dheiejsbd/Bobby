using UnityEngine;
using UnityEditor;

namespace Boby
{
    public interface IDamageCauser 
    {
        GameObject GetCauser();
    }
}