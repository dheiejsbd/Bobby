using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
namespace Bobby
{
    public enum AudioType
    {
        Bgm,
        Effect
    }
    [System.Serializable]
    public class SoundRowData
    {
        public string name;
        public AudioClip clip;
        public AudioType Type;
        public float Volume;
        public AudioMixerGroup Mixer;
    }
}