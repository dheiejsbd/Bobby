using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using DG.Tweening;
namespace Bobby
{
    public class Outtro : IPage
    {
        public Outtro(Blackboard blackboard)
        {
            this.blackboard = blackboard;
        }

        public int ID => (int)PageID.Outtro;
        public string BGM => "Outtro";

        protected Blackboard blackboard;
        protected UIButton GameStartButton;
        protected VideoPlayer Video;
        protected LoadingScreenImage loadingScreenImage;
        protected GameObject Text;
        #region
        public void Initialize()
        {
            Transform Client = GameObject.Find("Client").transform;
            Transform Canvas = Client.Find("Canvas");
            Video = GameObject.Find("VideoPlayer").GetComponent<VideoPlayer>();
            Transform Rect = Canvas.Find("Rect");
            loadingScreenImage = Rect.Find("LoadingScreenImage").GetComponent<LoadingScreenImage>();


            GameStartButton = Canvas.Find("GameStratButton").GetComponent<UIButton>();
            GameStartButton.onButtonWasReleased = () => 
            {
                Debug.Log("게임 시작 버튼 클릭");

                GameStartButton.Deactivate();

                PageManager.Change(PageID.Lobby);
            };

            Text = GameStartButton.transform.Find("Text").gameObject;
        }

        public void Terminate()
        {
        }
        #endregion

        #region
        public void Prepare()
        {
            GameStartButton.Deactivate();
            Video.clip = Resources.Load<VideoClip>("Video/Outtro");
            Video.playOnAwake = false;
            Video.isLooping = true;
        }
        #endregion

        #region
        public IEnumerator  Enter()
        {
            yield return null;
            Text.SetActive(false);
            loadingScreenImage.HideSequence.Play();
            GameStartButton.Activate();
            Video.Play();
        }

        public void Update()
        {
        }

        public void LateUpdate()
        {
        }

        public IEnumerator Exit()
        {
            loadingScreenImage.ShowSequence.Play();
            yield return new WaitForSeconds(0.8f);
            GameStartButton.Deactivate();
            Video.Stop();
        }
        #endregion
    }

}

