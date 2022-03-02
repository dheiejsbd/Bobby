using UnityEngine;
using UnityEditor;
using DG.Tweening;

namespace Bobby
{
    [RequireComponent(typeof(CanvasGroup))]
    public class LoadingScreenImage : UIWidget
    {
        public override Sequence ShowSequence
        {
            get
            {
                Sequence seq = base.ShowSequence
                        .Append(DOTween.To(() => Alpha, x => Alpha = x, 1f, 0.8f));
                return seq;
            }
        }

        public override Sequence HideSequence
        {
            get
            {
                Sequence seq = base.HideSequence
                        .Append(DOTween.To(() => Alpha, x => Alpha = x, 0f, 0.8f));
                return seq;
            }
        }

        [SerializeField] UnityEngine.UI.Image iconImageComponent;

        CanvasGroup canvasGroup;
        protected float Alpha 
        { 
            get { return canvasGroup.alpha; }
            set { canvasGroup.alpha = value; }
        } 
       
        protected override void Awake()
        {
            canvasGroup = gameObject.GetComponent<CanvasGroup>();
            HideSequence.Play(); 
        }
    }
}