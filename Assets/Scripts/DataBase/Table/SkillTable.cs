
using UnityEngine;
using System.Collections.Generic;

namespace Boby
{
    //CSV에 있는 데이터들의 정보를 참조하는 변수들의 집합
    public class SkillTable : Table
    {
        public override int Id { get { return 0; } }

        public Dictionary<int, SkillRowData> SkillRowDatas;

        public override void Load()
        {
            
            TextAsset text = Resources.Load<TextAsset>("Tables/Skill");
            string[] t = text.text.Split(new char[] { '\n' });
            for (int i = 0; i < t.Length - 2; i++)
            {
                //텍스트 내부 정보를 ,단위로 나눈다.
                string[] d = t[i + 1].Split(new char[] { ',' });
                SkillRowData SkillRow = new SkillRowData();
                SkillRow.ID = d[0].ToEnum<SkillID>();
                SkillRow.Name = (d[1]);
                SkillRow.Damage = int.Parse(d[2]);
                SkillRow.CoolTime = int.Parse(d[3]);
                SkillRow.Type = (SkillType)int.Parse(d[4]);
                SkillRow.Range = int.Parse(d[5]);
                SkillRow.Duration = int.Parse(d[6]);
                SkillRow.Speed = int.Parse(d[7]);
                SkillRow.AttackCount = int.Parse(d[8]);


                Debug.Log(JsonUtility.ToJson(SkillRow));

            }   
        }
    }
}