using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Bobby
{
    public class Pool 
    {
        List<GameObject> pool = new List<GameObject>();
        GameObject PoolObj;
        public static Transform PoolParent;
        public Pool(GameObject obj)
        {
            PoolObj = obj;
        }

        public GameObject Pop()
        {
            if (pool.Count > 0)
            {
                GameObject gameObject = pool[0];
                pool.RemoveAt(0);
                return gameObject;
            }
            else
            {
                if (PoolParent == null) PoolParent = new GameObject("ProjectilePool").transform;
                GameObject obj = GameObject.Instantiate(PoolObj, PoolParent);
                obj.SetActive(false);
                return obj;
            }
        }

        public void  Push(GameObject obj)
        {
            obj.SetActive(false);
            pool.Add(obj);
        }
    }
}