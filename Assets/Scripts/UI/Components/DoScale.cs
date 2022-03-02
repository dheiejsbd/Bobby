

using UnityEngine;
using DG.Tweening;

namespace Bobby
{
    public class DoScale : UIWidget
    {
        [SerializeField] protected RectTransform iconImageRectTransformComponent;
        [SerializeField] protected float scaleSize = 0.2f;
        [SerializeField] protected float duration = 0.35f;

        protected override void Awake() 
        {
            //DOTween.Sequence()
            //    .Append();

            iconImageRectTransformComponent.DOScale(scaleSize, duration).SetEase(Ease.InBounce).SetLoops(-1, LoopType.Yoyo);
        }
    }
}