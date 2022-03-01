using UnityEngine;
using UnityEditor;
using System.Collections;

public class Coroutine : MonoBehaviour
{
    public static Coroutine instance;

    private void Awake()
    {
        instance = this;
    }
    public static IEnumerator Begin(IEnumerator p)
    {
        yield return instance.StartCoroutine(p);
    }
}