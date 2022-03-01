using UnityEngine;
using UnityEditor;

namespace Boby
{
    public class Blackboard 
    {
        public string LinkedSceneName { get; private set;  }
        public int PlayerID { get; private set; }
        public void SaveLinkedSceneName(string sceneName)
        {
            LinkedSceneName = sceneName;
        }
        public void SavePlayerID(int ID)
        {
            PlayerID = ID;
        }
    }
}