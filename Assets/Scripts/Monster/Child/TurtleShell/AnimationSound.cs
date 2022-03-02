using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bobby
{


    public class AnimationSound : MonoBehaviour
    {
        [SerializeField] string hit;
        [SerializeField] string Attack;
        public void HitEvent()
        {
            SoundManager.instance.PlayEffect(hit);
        }
        public void AttackEvent()
        {
            SoundManager.instance.PlayEffect(Attack);
        }
    }
}