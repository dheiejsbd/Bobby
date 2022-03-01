using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Boby
{
    public class UIWidget : MonoBehaviour
    {
        public virtual Sequence ShowSequence
        {
            get 
            {
                Sequence seq = DOTween.Sequence()
                .OnStart(() => 
                {
                    Activate();
                    //  
                    Debug.Log("�����ֱ� ������ ����");
                })
                .OnComplete(() => 
                {
                    //  
                    Debug.Log("�����ֱ� ������ ����");
                });

                return seq;
            }
        }

        public virtual Sequence HideSequence
        {
            get
            {
                Sequence seq = DOTween.Sequence()
                .OnStart(() =>
                {
                    //  
                    Debug.Log("����� ������ ����");
                })
                .OnComplete(() =>
                {

                   Deactivate();
                    //  
                    Debug.Log("����� ������ ����");
                });

                return seq;
            }
        }

        protected virtual void Awake() { }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
