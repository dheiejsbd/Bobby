using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Boby
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ShopWindow : UIWidget
    {
        [SerializeField] protected intButton[] Select;
        [SerializeField] protected UIButton Exit;
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

        CanvasGroup canvasGroup;
        protected float Alpha
        {
            get { return canvasGroup.alpha; }
            set { canvasGroup.alpha = value; }
        }

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            HideSequence.Play();
        }

        public void Resualt(bool isWin)
        {
            ShowSequence.Play();
        }

        public void SetSelectButtonEvent(intButton.Deligate deligate)
        {
            for (int i = 0; i < Select.Length; i++)
            {
                Select[i].AddPointerDown(deligate);
            }
        }
        public void SetExitEvent(System.Action action)
        {
            Exit.onButtonWasPressed = action;
        }
    }
}
