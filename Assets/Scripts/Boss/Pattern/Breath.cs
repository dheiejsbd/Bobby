using UnityEngine;

namespace Bobby
{
    class AttackPattern2 : IPattern
    {
        public PatternID ID => PatternID.Breath;

        public BossPatternRowData Data => throw new System.NotImplementedException();

        Transform PlayerTR;

        public void Prepare()
        {
            PlayerTR = GameObject.Find("Player(Clone)").transform;
        }
        public float BeforeAttack()
        {
            ///애니메이션 넣기
            return Data.Delay;
        }

        public void AttackSuccess()
        {
            if (PlayerTR.rotation.y == 0)
            {
                Debug.Log("GameOver");
            }
        }


        public float Attack()
        {
            /// 애니메이션 넣기
            return 0;
        }

    }
}

