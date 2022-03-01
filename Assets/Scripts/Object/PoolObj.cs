using System.Collections;
using UnityEngine;

namespace Boby
{
    public abstract class PoolObj : MonoBehaviour
    {
        protected virtual void OnEnable()
        {
            StartCoroutine(ReturnObj(gameObject));
        }
        protected virtual IEnumerator ReturnObj(GameObject obj)
        {
            yield return new WaitForSeconds(5f);
        }
    }
}