using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Bobby
{
    public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform rectBack;
        [SerializeField] private RectTransform rectJoystick;
        Vector2 Output = Vector2.zero;
        private float Radius;
        bool trigger = false;

        void Start()
        {
            //뭐가 다른지 모르지만 일단 쓰기 편한걸로
            //rectBack = transform.Find("JoystickGround").GetComponent<RectTransform>();
            //rectJoystick = transform.Find("JoystickGround/Joystick").GetComponent<RectTransform>();
            // JoystickBackground의 반지름입니다.
            Radius = rectBack.rect.width * 0.5f;
        }
        public void OnDrag(PointerEventData eventData)
        {
            if (!trigger) return;
            Vector2 value = eventData.position - (Vector2)rectBack.position;
            value = Vector2.ClampMagnitude(value, Radius);
            rectJoystick.localPosition = value;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            trigger = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            rectJoystick.localPosition = Vector3.zero;
            trigger = false;
        }

        public Vector2 output()
        {
            return Output.normalized;
        }
    }
}