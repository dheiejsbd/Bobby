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
        //�θ� RectTransform ������Ʈ
        private RectTransform rectParent;
        //�ڽ� RectTransform ������Ʈ
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

            //���� ��ǥ�� ��ũ���� ��ǥ�� ��ȯ
            var screenPos = Camera.main.WorldToScreenPoint(TargetTr.position + offset); 
            //ī�޶��� ���� ����(180�� ȸ��)�� �� ��ǩ�� ����
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
