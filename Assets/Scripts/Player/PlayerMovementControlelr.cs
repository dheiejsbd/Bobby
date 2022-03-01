using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Boby
{
    public class PlayerMovementControlelr
    {
        private GameObject playerGameObject;
        CharacterController controller;

        Vector3 Force = Vector3.zero;
        float Gravity = 9.81f;

        PlayerData Data;
        public bool isDashing = false;
        float DashSpeed => Data.DashSpeed;
        float DashDurationTime => Data.DashTime;
        float DashDurationTimer = 0;

        public PlayerMovementControlelr(GameObject playerGameObject, PlayerData Data)
        {
            this.playerGameObject = playerGameObject;
            this.Data = Data;
        }

        public void PrePare()
        {
            controller = playerGameObject.GetComponent<CharacterController>();
        }

        public void Move(Vector3 MoveDir, float MoveSpeed)
        {
            if (MoveDir == Vector3.zero) return;

            controller.transform.rotation = LookVector(MoveDir);
            controller.Move(MoveDir * MoveSpeed * Time.deltaTime);
        }

        public Quaternion LookVector(Vector3 MoveDir)
        {
            if (MoveDir == Vector3.zero)
            {
                return playerGameObject.transform.rotation;
            }
            return Quaternion.Lerp(controller.transform.rotation, Quaternion.LookRotation(MoveDir.normalized), Data.RotateSpeed * Time.deltaTime);
        }



        public void Jump(float JumpForce)
        {
            if (!Physics.Raycast(playerGameObject.transform.position + Vector3.up * 0.1f, Vector3.down, 0.3f, 1 << 9)) return;

            Force = Vector3.up * JumpForce;
            controller.Move(Force * Time.deltaTime);
        }

        public void Update()
        {

        }
        public void LateUpdate()
        {
            if (controller.isGrounded)
                Force = Vector3.zero;
            else
                Force += Vector3.down * 9.18f * Time.deltaTime;

            controller.Move(Force * Time.deltaTime);
        }
        public void UpdateDashState()
        {
            if (isDashing)
            {
                DashDurationTimer += Time.deltaTime;
                if (DashDurationTimer > DashDurationTime)
                {
                    DashDurationTimer = 0;
                    isDashing = false;
                }
                else
                {
                    controller.Move(playerGameObject.transform.forward * Time.deltaTime * DashSpeed);
                }
            }
        }

        /*
        enum DirectionButton
        {
            Left,
            Right,
        }





        Transform PlayerTransform { get { return playerGameObject.GetComponent<Transform>(); } }
        Rigidbody PlayerRigidbody { get { return playerGameObject.GetComponent<Rigidbody>(); } }



        float JumpFos = 10;
        float MoveSpeed = 8;
        float DashSpeed = 40f;

        bool JumpTrigger = false;


        float MoveAxis = 0;
        float TargetMoveAxis = 0;

        bool isMoving;
        bool isJumping;
        bool isDashing;


        /// <summary>
        /// DashTime
        /// </summary>
        float DashDurationTime = 0.05f;
        float DashDurationTimer = 0;

        DirectionButton PreviousClickedButton;
        DirectionButton LastClickedButton;
        bool isDoubleClickedButton;

        public void PrePare()
        {
            Transform Client = GameObject.Find("Client").transform;
            Transform Canvas = Client.Find("Canvas");
            Transform Game = Canvas.Find("Game");


            UIButton A_temp = Game.Find("A").GetComponent<UIButton>();
            A_temp.OnPressButton += (GameObject button) =>
            {
                Debug.Log("Press A");
                PreviousClickedButton = LastClickedButton;
                LastClickedButton = DirectionButton.Left;


                if (TargetMoveAxis != 0)
                {
                    TargetMoveAxis = 0;
                    return;
                }


                TargetMoveAxis = -1;
            };
            A_temp.OnDoubleClickButton += (GameObject button) => { isDoubleClickedButton = true; };
            A_temp.OnReleaseButton += (GameObject button) => { TargetMoveAxis = 0; };


            UIButton D_temp = Game.Find("D").GetComponent<UIButton>();
            D_temp.OnPressButton += (GameObject button) =>
            {

                Debug.Log("Press D");
                PreviousClickedButton = LastClickedButton;
                LastClickedButton = DirectionButton.Right;

                if (TargetMoveAxis != 0)
                {
                    TargetMoveAxis = 0;
                    return;
                }
                TargetMoveAxis = 1;
            };
            D_temp.OnDoubleClickButton += (GameObject button) => { isDoubleClickedButton = true; };
            D_temp.OnReleaseButton += (GameObject button) => { TargetMoveAxis = 0; };

            UIButton Jump_temp = Game.Find("Jump").GetComponent<UIButton>();
            Jump_temp.OnClickButton += (GameObject button) =>
            {
                JumpTrigger = true;
            };
        }

        public void Update()
        {
            //Rotate();
            //Move();


            UpdateDashState();

            UpdateJumpState();

            UpdateMoveState();

        }

        public void LateUpdate()
        {
            MoveAxis = Mathf.MoveTowards(MoveAxis, TargetMoveAxis, 10 * Time.deltaTime);

            void RefreshButtonState()
            {
                isDoubleClickedButton = false;
            }
            RefreshButtonState();

            TargetMoveAxis = 0;
            JumpTrigger = false;
        }

        public void UpdateJumpState()
        {
            if (JumpTrigger)
            {
                if (!IsGround()) return;

                Jump();
            }
            isJumping = IsGround(); 
        }

        bool IsGround()
        {
            if (Physics.Raycast(PlayerTransform.position, Vector3.down, 1.1f, 1 << 9)) return true;

            return false;
        }

        void Jump()
        {
            PlayerRigidbody.velocity = Vector3.up * JumpFos;
        }

        public void UpdateMoveState()
        {
            if (isMoving)
            {
                Debug.Log("Update Player Move");

                Rotate();
                Move();
            }
            else
            {
                if (isDashing) return;
                //  이동 할수 있는가 조건 검사
                isMoving = true;
            }
        }

        void Rotate()
        {
            if (MoveAxis > 0)
                PlayerTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            else if (MoveAxis < 0)
                PlayerTransform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }

        void Move()
        {
            PlayerTransform.Translate(PlayerTransform.right * Time.deltaTime * MoveAxis * MoveSpeed);
        }

        public void UpdateDashState()
        {
            if (isDashing)
            {
                DashDurationTimer += Time.deltaTime;
                if (DashDurationTimer > DashDurationTime)
                {
                    DashDurationTimer = 0;
                    isDashing = false;
                }
                else
                {
                    Transform pt = PlayerTransform;
                    pt.Translate(Vector3.right * Time.deltaTime * DashSpeed);
                    isMoving = false;
                }
            }
            else
            {
                // 1.방향 버튼이 더블 클릭이 되었는가?
                if (!IsDoubleClicked()) return;
                // 2.두번째 클릭 버튼 방향과 이전 방향 버튼이 동일한가? 
                if (!IsSameDirection()) return;
                // 3.대쉬 중인가?          
                if (IsDashing()) return;

                isDashing = true;
                Debug.Log("Dash");
            }
        }

        private bool IsDoubleClicked() 
        {
            if (isDoubleClickedButton) return true;

            return false; 
        }

        private bool IsSameDirection() 
        {
            if (LastClickedButton == PreviousClickedButton) return true;
            return false; 
        }

        private bool IsDashing() { return isDashing; }*/
    }
}