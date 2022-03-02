using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace Bobby
{
    public class BossHUDWindow : UIWidget
    {
        [SerializeField] Image BossHPbar;
        MonsterController MonsterController;
        protected override void Awake()
        {
            HideSequence.Play();
        }
        public void SetMonsterController(MonsterController link)
        {
            MonsterController = link;
            BossHPbar.fillAmount = 1;
        }
        public void LateUpdate()
        {
            if (MonsterController == null) return;
            if (MonsterController.CurHP <= 0)
            {
                HideSequence.Play();
                MonsterController = null;
            }

            BossHPbar.fillAmount = MonsterController.CurHP / MonsterController.MaxHP;
        }
    }
}