/*
 *using UnityEditor;
using UnityEngine;
using System;

namespace Boby
{
    public class GameEvnt : MonoBehaviour
    {
        public static GameEvnt Instance;
        private void Awake()
        {
            Instance = this;
        }
        public event Action OnBossHit;
        public void BossHit()
        {
            if (OnBossHit != null)
                OnBossHit();
        }
    }
}
*/