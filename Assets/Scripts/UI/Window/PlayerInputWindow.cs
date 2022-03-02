using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Bobby
{
    public class PlayerInputWindow : UIWidget
    {
        [SerializeField] Joystick joystick;
        [SerializeField] RectTransform Joystick1;
        [SerializeField] UIButton RunButton;
        [SerializeField] UIButton AttackButton;
        [SerializeField] UIButton SkillButton;


        public System.Action onAttackButtonWasPressed;
        public System.Action onAttackButtonisPressing;
        public System.Action onAttackButtonWasReleased;

        public System.Action onSkillButtonWasPressed;
        public System.Action onSkillButtonisPressing;
        public System.Action onSkillButtonWasReleased;

        public System.Action onRunButtonWasPressed;
        public System.Action onRunButtonisPressing;
        public System.Action onRunButtonWasReleased;

        protected override void Awake()
        {
            AttackButton.onButtonWasPressed = () => { onAttackButtonWasPressed?.Invoke(); }; 
            AttackButton.onButtonisPressing = () => { onAttackButtonisPressing?.Invoke(); };
            AttackButton.onButtonWasReleased = () => { onAttackButtonWasReleased?.Invoke(); };

            SkillButton.onButtonWasPressed = () => { onSkillButtonWasPressed?.Invoke(); }; 
            SkillButton.onButtonisPressing = () => { onSkillButtonisPressing?.Invoke(); };
            SkillButton.onButtonWasReleased = () => { onSkillButtonWasReleased?.Invoke(); };

            RunButton.onButtonWasPressed = () => { onRunButtonWasPressed?.Invoke(); }; 
            RunButton.onButtonisPressing = () => { onRunButtonisPressing?.Invoke(); };
            RunButton.onButtonWasReleased = () => { onRunButtonWasReleased?.Invoke(); };
            HideSequence.Play();
        }

        public void ResetInput()
        {
            joystick.OnPointerUp(null);
            AttackButton.OnPointerUp(null);
            SkillButton.OnPointerUp(null);
            RunButton.OnPointerUp(null);
        }

        public Vector3 GetMoveDIr()
        {
            Vector3 MovePos = new Vector3(Joystick1.localPosition.x, 0, Joystick1.localPosition.y).normalized;
            return MovePos;
        }
    }
}
