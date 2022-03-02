using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bobby
{
    [System.Serializable]
    public class Skill
    {
        public int id;
        public string parameter;
        public float animationLength;
        public GameObject socket;
    }

    public class BossController : MonoBehaviour
    {
        public enum State
        {
            Idle,
            Attack,
            Skill,
            Die,
        }

        [SerializeField] private BossData bossData;
        [SerializeField] protected Skill flameSkill;

        public float MaxHP { get { return bossData.HP; } }
        private float mCurHP;
        public float CurHP
        {
            get { return mCurHP; }
            set
            {
                mCurHP = value;

                Debug.LogFormat("현재 보스 HP {0}", mCurHP);
                UpdateConditions();
            }
        }

        public float Attack { get { return bossData.Attack; } }

        private AttackCondition forAttack;

        private bool isAttacking;
        private float AttackTimer;
        private float AttackTime = 2f;

        private State state;
        protected Animator animator;
        protected AnimationEventListener animationEventListener;

        private void Awake()
        {
            animator = gameObject.GetComponentInChildren<Animator>();
            animationEventListener = gameObject.GetComponentInChildren<AnimationEventListener>();
            animationEventListener.startFlame += () => { flameSkill.socket.SetActive(true); };
            animationEventListener.endFlame += () => { flameSkill.socket.SetActive(false); };
        }
        public void SetData()
        {
            forAttack = new AttackCondition();
            CurHP = MaxHP;
        }
        public void SetFirstState()
        {
            state = State.Idle;
        }

        public void OnUpdate()
        {
            switch (state)
            {
                case State.Idle:
                    {
                        if (forAttack.NomalUpdate())
                        {
                            state = State.Attack;
                        }

                        if (forAttack.IsTrue())
                        {
                            state = State.Skill;

                        }

                        if (CurHP <= 0)
                        {
                            state = State.Die;
                        }
                    }
                    break;

                case State.Attack:
                    {
                        UpdateNomalAttackState();
                    }
                    break;

                case State.Skill:
                    {
                        UpdateSkillAttackState();
                    }
                    break;

                case State.Die:
                    {
                        Debug.Log("boss die");
                        PageManager.Change(PageID.Gameover);
                    }
                    break;
            }
        }
        public void OnLateUpdate()
        {
        }

        /// <summary>
        /// 조건들 업데이트
        /// </summary>
        private void UpdateConditions()
        {
            forAttack.Update(this);
        }
        private void UpdateNomalAttackState()
        {
            if (isAttacking)
            {
                AttackTimer += Time.deltaTime;
                if (AttackTimer > AttackTime)
                {
                    Debug.Log("기본 공격 종료.");
                    forAttack.isTimer = 0f;
                    isAttacking = false;
                    state = State.Idle;
                }
                else
                {
                    Debug.Log("기본 공격 중");
                }
            }
            else
            {
                Debug.Log("기본 공격 시작.");

                isAttacking = true;
                AttackTimer = 0f;
            }
        }
        private void UpdateSkillAttackState()
        {
            if (isAttacking)
            {
                AttackTimer += Time.deltaTime;
                if (AttackTimer > AttackTime)
                {
                    Debug.Log("스킬 공격 종료.");

                    isAttacking = false;
                    state = State.Idle;
                    forAttack.BossSkillHp = 0.3f;
                }
                else
                {
                    Debug.Log("스킬 공격 중");
                }
            }
            else
            {
                Debug.Log("스킬 공격 시작.");

                isAttacking = true;
                AttackTimer = 0f;
            }
        }

        private void OnGUI()
        {
            if (GUI.Button (new Rect(0,0,60,60), "브레스"))
            {
                StartCoroutine(UseSkill(flameSkill));
            }
        }

        protected IEnumerator UseSkill(Skill skill)
        {
            animator.SetBool(skill.parameter, true);
            

            yield return new WaitForSeconds(skill.animationLength);

            animator.SetBool(skill.parameter, false);
        }

        public void OnParticleCollision(GameObject other)
        {
                if (other.layer == 11)
                    CurHP -= 10;
        }
    }

}
