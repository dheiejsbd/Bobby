using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using DG.Tweening;
using TMPro;



namespace Bobby
{
    public class PlayerStatHUDWindow : UIWidget
    {
        [SerializeField] Image HPImage;
        [SerializeField] Image StaminaImage;
        [SerializeField] Image SkillCoolTimeImage;
        [SerializeField] TextMeshProUGUI HPText;
        [SerializeField] TextMeshProUGUI StaminaText;

        float MaxStamina;
        float MaxHP;
        float LastHP;
        protected override void Awake()
        {
            HideSequence.Play();
        }

        public void SetMaxHp(float HP)
        {
            this.MaxHP = HP;
        }

        public void UpdateHp(float Now)
        {
            HPText.text = Now-(Now%1) +"/"+ MaxHP;

            if (LastHP == Now) return;

            if(LastHP > Now)
            {
                HPImage.color = Color.white;
            }
            //else
            //{
            //    HPImage.color = Color.green;
            //}


            LastHP = Now;
            HPImage.DOFillAmount(Now / MaxHP, 0.5f).SetEase(Ease.Linear);
            HPImage.DOColor(Color.red, 0.5f).SetEase(Ease.Flash);
            if(gameObject.active)
            {
                //StartCoroutine(Delay(0.25f));
            }
            //HPImage.fillAmount = Now / MaxHP;
        }

        IEnumerator Delay(float time)
        {
            yield return new WaitForSeconds(time);
            HPImage.DOColor(Color.red, 0.25f).SetEase(Ease.Flash);
        }

        public void SetMaxStamina(float Max)
        {
            this.MaxStamina = Max;
        }

        public void UpdateStamina(float Now)
        {
            StaminaImage.fillAmount = Now / MaxStamina;
            Now -= Now % 1;
            StaminaText.text = Now + "/" + MaxStamina;
        }

        public void UpdateSkillCoolTime(float fillAmount)
        {
            if(fillAmount == 1)
            {
                SkillCoolTimeImage.color = Color.white;
            }
            else
            {
                SkillCoolTimeImage.color = Color.gray;
            }
            SkillCoolTimeImage.fillAmount = fillAmount;
        }    
    }
}