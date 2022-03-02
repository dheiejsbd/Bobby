using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace Bobby
{
    public class PlayerController : MonoBehaviour, IDamageable
    {
        public delegate void EnterShop();
        public EnterShop onEnterShop;

        public delegate void EnterPortal(Portal portal);
        public EnterPortal onEnterPortal;

        public float time = 0;
        public float Delaytime = 0.2f;
        [SerializeField] PlayerData Data;
        [SerializeField] int SkillID;
        [SerializeField] ParticleSystem AttackParticle;
        [SerializeField] ParticleSystem HitParticle;
        public System.Action PlayerDie;

        #region Component
        protected PlayerInputWindow PlayerInputWindow;
        protected PlayerStatHUDWindow PlayerStatHUDWindow;

        protected ProjectileController ProjectileController;
        protected PlayerMovementControlelr PlayerMovementControlelr;

        protected Animator Animator;
        #endregion

        #region Skill
        Dictionary<int, ISkill> Skills = new Dictionary<int, ISkill>();
        ISkill activeSkill;
        ParticleSystem FxBreath;
        #endregion

        #region status
        protected float CurHP;
        protected float Stamina;
        protected float moveSpeed;
        #endregion

        #region animatorParameters
        protected bool IsAttack
        {
            get { return Animator.GetBool("isFire"); }
            set { Animator.SetBool("isFire", value); }
        }
        protected bool IsSkill
        {
            get { return Animator.GetBool(activeSkill.SkillAnim); }
            set { Animator.SetBool(activeSkill.SkillAnim, value); }
        }
        protected bool IsMove
        {
            get { return Animator.GetBool("isMove"); }
            set { Animator.SetBool("isMove", value); }
        }
        protected bool IsRun
        {
            get { return Animator.GetBool("isRun"); }
            set { Animator.SetBool("isRun", value); }
        }
        #endregion

        #region Timer
        protected float attackTimer;
        protected float SkillTimer;
        protected bool isKnockBack;
        protected float knockBackTimer;
        #endregion

        #region State
        bool isDashing { get { return PlayerMovementControlelr.isDashing; } set { PlayerMovementControlelr.isDashing = value; } }
        public bool immortality = false;
        #endregion

        #region Auto
        [SerializeField]LayerMask TargetlayerMask;
        [SerializeField]float viewRange;
        [SerializeField] float viewAngle;
        #endregion
        int BreathSoundID = 0;
        bool SkillChack = false;
//==========여기부터 함수===========

        #region setting

        private void Awake()
        {
            CurHP = Data.HP;
            Stamina = Data.MaxStamina;
            Animator = GetComponent<Animator>();

            ProjectileController = gameObject.GetComponentInChildren<ProjectileController>();
            PlayerMovementControlelr = new PlayerMovementControlelr(gameObject, Data);
            PlayerMovementControlelr.PrePare();

            ResetTimer();
            AddSkill();

            Transform[] AllData = gameObject.GetComponentsInChildren<Transform>();

            foreach (Transform Obj in AllData)
            {
                if (Obj.name == "FX_Boss_Bress")
                {
                    FxBreath = Obj.GetComponent<ParticleSystem>();
                }
            }

            activeSkill = Skills[SkillID];
        }
        private void Start()
        {
            GameObject.Find("Main Camera").GetComponent<FollowCam>().SetTarget(transform);
        }

        void ResetTimer()
        {
            time = 0;
            SkillTimer = 0;
            knockBackTimer = 0;
        }
        void AddSkill()
        {
            TryAddSkill(new Breath(AttackParticle));
            TryAddSkill(new Heal(gameObject, AttackParticle));
            TryAddSkill(new Ice(gameObject, AttackParticle));
        }

        void TryAddSkill(ISkill skill)
        {
            if (Skills.ContainsKey(skill.ID)) return;

            Skills.Add(skill.ID, skill);
        }

        public void SetPlayerInput(PlayerInputWindow window)
        {
            PlayerInputWindow = window;
            PlayerInputWindow.onAttackButtonWasPressed = AttackButtonWasPressed;
            PlayerInputWindow.onAttackButtonisPressing  = AttackButtonButtonisPressing;
            PlayerInputWindow.onAttackButtonWasReleased = AttackButtonButtonWasReleased;


            PlayerInputWindow.onSkillButtonWasPressed = SkillButtonWasPressed;
            PlayerInputWindow.onSkillButtonisPressing  = SkillButtonButtonisPressing;
            PlayerInputWindow.onSkillButtonWasReleased = SkillButtonButtonWasReleased;
            
            PlayerInputWindow.onRunButtonWasPressed = RunButtonWasPressed;
        }
        public void SetPlayerHUD(PlayerStatHUDWindow window)
        {
            PlayerStatHUDWindow = window;
            PlayerStatHUDWindow.SetMaxHp(Data.HP);
            PlayerStatHUDWindow.SetMaxStamina(Data.MaxStamina);
        }
        #endregion

        #region 공격 버튼 이벤트
        protected void AttackButtonWasPressed()
        {
            TryFire();
        }
        protected void AttackButtonButtonisPressing()
        {
        }
        protected void AttackButtonButtonWasReleased()
        {
        }
        #endregion

        #region 스킬 버튼 이벤트
        protected void SkillButtonWasPressed()
        {
            TrySkill();
        }
        protected void SkillButtonButtonisPressing()
        {
        }
        protected void SkillButtonButtonWasReleased()
        {
            EndSkill();
        }
        #endregion

        #region RunButtonEvent
        protected void RunButtonWasPressed()
        {
            TryDash();
        }
        #endregion

        #region 기본 공격 구현부

        private void TryFire()
        {
            //  1. 스태미너 검사
            if (IsAttack) return;
            if (IsSkill) return;
            if (Stamina < Data.Stamina.Attack) { SoundManager.instance.PlayEffect("NoneStemina"); return; } 

            Stamina -= Data.Stamina.Attack;
            SoundManager.instance.PlayEffect(Data.Sound.Attack);
            StartCoroutine(FireProcess());
        }
        private IEnumerator FireProcess()
        {
            IsAttack = true;
            yield return new WaitForSeconds(0.25f);
            IsAttack = false;
        }
        #endregion

        #region 스킬 구현부
        void TrySkill()
        {
            activeSkill = Skills[SkillID];
            if (IsAttack) return;
            if (IsSkill) return;
            if (activeSkill.Stamina > Stamina) return;
            if (!activeSkill.CanAttack()) return;
            if (SkillTimer > Time.time) return;
            if (SkillChack) return;

            SkillTimer = Time.time + activeSkill.CoolTime;
            IsSkill = true;
            SkillChack = true;
            if(activeSkill.single)
            {
                Stamina -= activeSkill.Stamina;
                SoundManager.instance.PlayEffect(Data.Sound.skill);
            }    
        }

        void EndSkill()
        {
            IsSkill = false;
            if (!activeSkill.single)
            {
                LoopSkillSound(false);
            }
            activeSkill.ExitTrigger();
        }
        #endregion

        void TryDash()
        {
            if (isDashing) return;
            if (Data.Stamina.Dash > Stamina) { SoundManager.instance.PlayEffect("NoneStemina"); return; }
            isDashing = true;
            Stamina -= Data.Stamina.Dash;
            SkillButtonButtonWasReleased();
        }

        #region 애니매이션 키프레임 이벤트
        public void FireEvent()
        {
            if(IsSkill || SkillChack)
            {
                SkillChack = false;
                activeSkill.AttackTrigger();
                if (activeSkill.single)
                {
                    SoundManager.instance.PlayEffect(Data.Sound.skill);
                    EndSkill();
                }
                else
                {
                    LoopSkillSound(true);
                    if (IsSkill) return;
                    EndSkill();
                }
            }
            else
            {
                Vector3 Target = FOV.GetTarget(transform, TargetlayerMask, viewRange, viewAngle);
                if(Target != Vector3.zero)
                {
                    Target.y = transform.position.y;
                    transform.LookAt(Target);
                }
                ProjectileController.Fire(transform.forward);
            }
        }
        public void WalkEvent()
        {
            SoundManager.instance.PlayEffect(Data.Sound.Walk);
        }
        public void DashEvent()
        {
            SoundManager.instance.PlayEffect(Data.Sound.Dash);
        } 

        public void LoopSkillSound(bool Play)
        {
            if(Play)
            {
                BreathSoundID = SoundManager.instance.PlayLoopEffect(Data.Sound.skill);
            }
            else
            {
                SoundManager.instance.StopLoopEffect(BreathSoundID);
            }
        }
        #endregion

        #region Update
        public void OnUpdate()
        {
            IsRun = isDashing;
            time += Time.deltaTime;

            if (isKnockBack)
            {
                UpdateKnockBack();
                isDashing = false;
                return;
            }
            else if(isDashing)
            {
                UpdateDashState();
            }
            else if(IsAttack || IsSkill)
            {
                if (IsAttack) return;

                if (Stamina < activeSkill.Stamina)
                {
                    EndSkill();
                }
                transform.rotation = PlayerMovementControlelr.LookVector(PlayerInputWindow.GetMoveDIr());

                return;
            }
            else
            {
                UpdateLocomotion();
                PlayerMovementControlelr.Update();
            }
        }
        protected void UpdateKnockBack()
        {
            Vector3 direction = new Vector3(transform.forward.x, transform.forward.y, transform.forward.z * -1).normalized;
            transform.position = transform.position + (direction * Time.deltaTime * Data.KnockBackSpeed);

            if (knockBackTimer > 0.2f)
            {
                knockBackTimer = 0f;
                isKnockBack = false;
            }
            else
            {
                knockBackTimer += Time.deltaTime;
            }
        }
        protected void UpdateDashState()
        {
            PlayerMovementControlelr.UpdateDashState();
        }
        protected void UpdateLocomotion()
        {
            Vector3 dir = PlayerInputWindow.GetMoveDIr();
            if (dir != Vector3.zero)
            {
                IsMove = true;

                /*
#if UNITY_EDITOR
                bool clickedRunButton = Input.GetMouseButton(1);
#else
                bool clickedRunButton = this.clickedRunButton;
#endif

                #region <CanRun?>
                if (clickedRunButton)
                {
                    if (Stamina < Data.Stamina.Run * Time.deltaTime)
                        canRun = false;
                }
                else
                {
                    canRun = true;
                }
                #endregion


                if (clickedRunButton && canRun)
                {
                    Stamina -= Data.Stamina.Run * Time.deltaTime;
                    moveSpeed = Data.RunSpeed;

                    IsRun = true;
                }
                else*/
                //{
                    moveSpeed = Data.WalkSpeed;
                    Stamina -= Data.Stamina.Walk * Time.deltaTime;
                    IsRun = false;
                //}

                PlayerMovementControlelr.Move(dir, moveSpeed);
            }
            else
            {
                Stamina -= Data.Stamina.Idle * Time.deltaTime;
                IsMove = false;
            }
        }
        public void OnLateUpdate()
        {
            PlayerMovementControlelr.LateUpdate();
            Stamina = Mathf.Clamp(Stamina, 0, Data.MaxStamina);

            UpdateStamina();
            UpdateHUD();
        }
        #endregion

        #region Hp
        public void TakeDamage(GameObject causer, float Damage, DamageType damageType)
        {
            if (immortality) return;
            HitParticle.transform.position = causer.transform.position;
            HitParticle.Play();
            SoundManager.instance.PlayEffect(Data.Sound.hit);
            switch (damageType)
            {
                case DamageType.KnockBackDamage:
                    {
                        isKnockBack = true;
                    }
                    break;
                case DamageType.NormalDamage:
                    {

                    }
                    break;
            }
            CurHP -= Damage;
            if (CurHP <= 0)
            {
                Debug.Log("플레이어 사망");
                PlayerDie();
                Destroy(GetComponent<Rigidbody>());
            }
        }
        public void HpControl(float variation)
        {
            CurHP += variation;

            if (CurHP > Data.HP)
            {
                CurHP = Data.HP;
            }
            UpdateHUD();
        }
        #endregion

        #region Stamina
        void UpdateStamina()
        {
            if (activeSkill.single) return;
            if (!IsSkill) return;
            Stamina -= activeSkill.Stamina;
        }

        #endregion

        #region HUD
        public void UpdateHUD()
        {
            PlayerStatHUDWindow?.UpdateHp(CurHP);
            PlayerStatHUDWindow?.UpdateStamina(Stamina);

            float StartTime = SkillTimer - activeSkill.CoolTime;
            float fill = (Time.time - StartTime) / activeSkill.CoolTime;
            fill = Mathf.Clamp(fill, 0, 1);
            PlayerStatHUDWindow?.UpdateSkillCoolTime(fill);
        }
        #endregion

        private void OnTriggerEnter(Collider other)
        {
            AttackCollision attackCollision = other.gameObject.GetComponent<AttackCollision>();
            Trigger Trigger = other.gameObject.GetComponent<Trigger>();

            if(Trigger)
            {
                if(Trigger.GetKey() == Trigger.Type.shop)
                {
                    onEnterShop?.Invoke();
                }
            }

            Portal potal = other.gameObject.GetComponent<Portal>();
            if (potal)
            {
                onEnterPortal?.Invoke(potal);
                //potal.GetEnterSceneName();
            }
        }
        void OnDestroy()
        {
            PlayerInputWindow.ResetInput();
        }
    }
}