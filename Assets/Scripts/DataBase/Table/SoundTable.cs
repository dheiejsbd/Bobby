using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
namespace Boby
{
    public class SoundTable : Table
    {
        private static SoundTable minstance;
        public static SoundTable instance
        {
            get
            {
                if (minstance == null)
                {
                    minstance = new SoundTable();
                }
                return minstance;
            }
        }

        private SoundTable()
        {

        }
        public override int Id => ID.SoundTableID;
        public Dictionary<string, SoundRowData> Sounds = new Dictionary<string, SoundRowData>();
       

        public override void Load()
        {
            TextAsset text = Resources.Load<TextAsset>("Tables/SoundDB");
            string[] t = text.text.Split(new char[] { '\n' });
            for (int i = 0; i < t.Length - 2; i++)
            {
                //텍스트 내부 정보를 ,단위로 나눈다.
                string[] d = t[i + 1].Split(new char[] { ',' });
                SoundRowData SoundRowData = new SoundRowData();
                SoundRowData.name = d[0];
                SoundRowData.clip = Resources.Load<AudioClip>("Sound/" + d[1]);
                SoundRowData.Type = d[2].ToEnum<AudioType>();
                Sounds.Add(SoundRowData.name, SoundRowData);
                SoundRowData.Volume = float.Parse(d[3]);
            }
        }
    }
}