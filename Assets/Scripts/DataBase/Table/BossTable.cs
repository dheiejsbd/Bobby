
using UnityEngine;
using System.Collections.Generic;

namespace Bobby
{
    //CSV에 있는 데이터들의 정보를 참조하는 변수들의 집합
    public class BossTable : Table
    {
        public override int Id { get { return 1; } }

        public Dictionary<int, BossRowData> BossRowDatas;

        public override void Load()
        {
            TextAsset text = Resources.Load<TextAsset>("Tables/Boss");
            string[] t = text.text.Split(new char[] { '\n' });
            for (int i = 0; i < t.Length - 2; i++)
            {
                
                //텍스트 내부 정보를 ,단위로 나눈다.
                string[] d = t[i + 1].Split(new char[] { ',' });
                BossRowData BossRow = new BossRowData();
                BossRow.ID = int.Parse(d[0]);
                BossRow.Type = (BossType)int.Parse(d[1]);
                BossRow.Name = (d[2]);
                BossRow.Hp = int.Parse(d[3]);

                Debug.Log(JsonUtility.ToJson(BossRow));
                
            }
        }
    }
}