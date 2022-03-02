using UnityEditor;
using UnityEngine;

namespace Bobby
{
    public class Medusa : IPattern
    {
        public PatternID ID => PatternID.Medusa;
        
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
        public float Attack()
        {
            return Data.Maintain;
        }
        public void AttackSuccess()
        {
            if (PlayerTR.rotation.y==0)
            {
                Debug.Log("GameOver");
            }
        }

    }
}