using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bobby
{
    public class EnemyHpBar : MonoBehaviour
    {
        private Camera uiCamera;
        private Canvas canvas;
        //부모 RectTransform 컴포넌트
        private RectTransform rectParent;
        //자신 RectTransform 컴포넌트
        private RectTransform rectHp;
        private Image image;
        public Vector3 offset = Vector3.zero;
        [HideInInspector] public Transform TargetTr;
        public MonsterController monsterController;
        void Awake()
        {
            canvas = GetComponentInParent<Canvas>();
            uiCamera = canvas.worldCamera;
            rectParent = canvas.GetComponent<RectTransform>();  
            rectHp = GetComponent<RectTransform>();
            image = transform.GetChild(0).GetComponent<Image>();
        }

        void LateUpdate()
        {
            if (TargetTr == null) Destroy(gameObject);
            if (monsterController.CurHP <= 0) Destroy(gameObject);
            image.fillAmount = monsterController.CurHP / monsterController.MaxHP;

            //월드 좌표를 스크린의 좌표로 변환
            var screenPos = Camera.main.WorldToScreenPoint(TargetTr.position + offset); 
            //카메라의 뒷쪽 영역(180도 회전)일 때 좌푯값 보정
            if (screenPos.z < 0.0f)
            {
                screenPos *= -1.0f;
            }
            var localPos = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent
                                                                    , screenPos
                                                                    , uiCamera
                                                                    , out localPos);
            rectHp.localPosition = localPos;
        }
    }
}
