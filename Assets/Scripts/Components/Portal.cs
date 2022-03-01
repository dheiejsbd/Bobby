using UnityEngine;
using UnityEditor;

public class Portal : MonoBehaviour
{
    [SerializeField] string linkedSceneName;

    public string GetLinkedSceneName()
    {
        return linkedSceneName;
    }
}