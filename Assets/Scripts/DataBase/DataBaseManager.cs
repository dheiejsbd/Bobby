using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Boby
{

    public class DataBaseManager 
    {
        public Dictionary<int, Table> Tables;
        public static DataBaseManager instance;

        public void Initialize()
        {
            instance = this;
            Tables = new Dictionary<int, Table>();
            Tables.Add(ID.SkillTableID, new SkillTable());
            Tables.Add(ID.BossTableID, new BossTable());
            Tables.Add(ID.BossPatternTableID, new BossPatternTable());
            Tables.Add(ID.SoundTableID, SoundTable.instance);
            Tables[ID.SoundTableID].Load();
        }

        public void Terminate()
        {
            Tables.Clear();
            Tables = null;
        }
    }
}
