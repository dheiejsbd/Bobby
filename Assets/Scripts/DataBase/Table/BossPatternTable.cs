
using UnityEngine;
using System.Collections.Generic;

namespace Bobby
{
    public class BossPatternTable : Table
    {
        public override int Id { get { return 2; } }

        public Dictionary<int, BossPatternRowData> BossPatternRowDatas;

        public override void Load()
        {
            TextAsset text = Resources.Load<TextAsset>("Tables/BossPattern");
            string[] t = text.text.Split(new char[] { '\n' });
            for (int i = 0; i < t.Length - 2; i++)
            {
                string[] d = t[i + 1].Split(new char[] { ',' });
                BossPatternRowData PatternRow = new BossPatternRowData();
                PatternRow.ID = int.Parse(d[0]);
                PatternRow.AnimID = d[3];
                PatternRow.Delay = int.Parse(d[4]);
                PatternRow.Range = int.Parse(d[5]);
            }
        }
    }
}