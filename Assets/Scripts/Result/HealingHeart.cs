using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Boby
{
    public class HealingHeart : MonoBehaviour
    {
        [SerializeField] protected float HealingAmount;
        [SerializeField] protected float SpinSpeed;
        [SerializeField] protected float scaleSize = 0.2f;
        [SerializeField] protected float duration = 0.35f;
        private void Start()
        {
            transform.DOScale(scaleSize, duration).SetEase(Ease.InBounce).SetLoops(-1, LoopType.Yoyo);
        }
        private void Update()
        {
            transform.Rotate(transform.up, Time.deltaTime * SpinSpeed);
        }
        private void OnTriggerEnter(Collider other)
        {
            PlayerController playerController;
            if (other.TryGetComponent(out playerController))
            {
                SoundManager.instance.PlayEffect("PlayerHeal");
                playerController.HpControl(HealingAmount);
                Destroy(gameObject);
            }
        }
    }
}