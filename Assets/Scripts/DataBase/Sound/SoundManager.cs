using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Bobby
{
    public class SoundManager
    {
        public SoundManager ()
        {
            instance = this;
            Initialize();
        }

        static public SoundManager instance { private set; get; }
        public Dictionary<string, SoundRowData> Sounds => SoundTable.instance.Sounds;
        public Dictionary<int, AudioSource> Loop = new Dictionary<int, AudioSource>();
        int id = 0;
        AudioSource AudioBgm;
        AudioSource AudioEffect;
        GameObject SoundObj;

        #region singleton
        private void Initialize()
        {
            SoundObj = new GameObject();

            AudioBgm = SoundObj.AddComponent<AudioSource>();
            AudioBgm.loop = true;
            AudioBgm.playOnAwake = false;
            AudioEffect = SoundObj.AddComponent<AudioSource>();
            AudioEffect.playOnAwake = false;
            AudioEffect.loop = false;
        }
        #endregion singleton

        #region Sound


        public void PlayEffect(string _name)
        {
            Debug.Log("TryPlayEffect" + _name);
            if (Sounds[_name].Type != AudioType.Effect) { Debug.LogError("Do Not Match Type - " + _name + " Type = " + Sounds[_name].Type); return; }

            AudioEffect.PlayOneShot(Sounds[_name].clip, Sounds[_name].Volume);
        }

        public int PlayLoopEffect(string _name)
        {
            id++;
            Loop.Add(id, SoundObj.AddComponent<AudioSource>());
            Loop[id].clip = Sounds[_name].clip;
            Loop[id].loop = true;
            Loop[id].Play();
            return id;
        }
        public void StopLoopEffect(int ID)
        {
            if (!Loop.ContainsKey(ID)) return;
            GameObject.Destroy(Loop[ID]);
        }

        public void StopEffect()
        {
            AudioEffect.Stop();
        }

        #endregion Sound

        #region BGM
        public void PlayBgm(string _name)
        {
            Debug.Log("TryPlayBGM" + _name);

            if (Sounds[_name].Type != AudioType.Bgm) { Debug.LogError("Do Not Match Type - " + _name + " Type = " + Sounds[_name].Type); return; }
            Debug.Log(Sounds[_name].clip);
            AudioBgm.Stop();
            AudioBgm.clip = Sounds[_name].clip;
            AudioBgm.volume = Sounds[_name].Volume;
            AudioBgm.Play();
        }

        public void StopBgm(string _name)
        {
            AudioBgm.Stop();
        }
        #endregion BGM

        public void StopAll()
        {
            AudioEffect.Stop();
            AudioBgm.Stop();
            foreach (var key in Loop.Keys)
            {
                GameObject.Destroy(Loop[key]);
            }
        }
    }
}