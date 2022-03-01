using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Boby
{
    public class Gameover : IPage
    {
        public Gameover(Blackboard blackboard)
        {
            this.blackboard = blackboard;
        }

        public int ID => (int)PageID.Gameover;

        public string BGM => "Gameover";

        protected Blackboard blackboard;
        protected GameObject ResultTab;

        protected UIButton LobbyButton;

        public void Initialize()
        {
            Transform Client = GameObject.Find("Client").transform;
            Transform Canvas = Client.Find("Canvas");
            //Transform Result = Canvas.Find("Result");
            Transform Game = Canvas.Find("Game");
            Transform Result = Game.Find("ResultWindow");

            ResultTab = Result.gameObject;
            LobbyButton = Result.Find("LobbyButton").GetComponent<UIButton>();
            LobbyButton.onButtonWasReleased = () =>
            {
                PageManager.Change(PageID.Lobby);
            };
            ResultTab.SetActive(false);

        }

        public void Terminate()
        {

        }

        public void Prepare()
        {
            ResultTab.SetActive(false);
        }

        public IEnumerator Enter()
        {
            yield return null;
            ResultTab.SetActive(true);
        }

        public void Update()
        {
            Debug.Log("GaemoverPageUpdate");
        }

        public void LateUpdate()
        {

        }

        public IEnumerator Exit()
        {
            SoundManager.instance.StopAll();
            yield return null;
            ResultTab.SetActive(false);
            GameObject.Destroy(GameObject.Find("Player(Clone)"));
            GameObject.Destroy(GameObject.Find("Boss(Clone)"));
            SceneManager.UnloadScene("Stage@1001");
        }
    }
}

