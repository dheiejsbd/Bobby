using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Boby
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ResultWindow : UIWidget
    {
        [SerializeField] protected UIButton LobbyButton;
        [SerializeField] protected GameObject WinImage;
        [SerializeField] protected GameObject LoseImage;

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
            SoundManager.instance.StopAll();
            if(isWin)
            {
                SoundManager.instance.PlayEffect("Win");
            }
            else
            {
                SoundManager.instance.PlayEffect("Lose");
            }

            WinImage.SetActive(isWin);
            LoseImage.SetActive(!isWin);

            ShowSequence.Play();
        }

        public void SetLobbyButtonEvent(System.Action action)
        {
            LobbyButton.onButtonWasPressed = action;
        }
    }
}
