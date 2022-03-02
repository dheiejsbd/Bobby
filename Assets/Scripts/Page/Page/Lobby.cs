using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
    
namespace Bobby
{
    public class Lobby : IPage
    {
        public Lobby(Blackboard blackboard)
        {
            this.blackboard = blackboard;
        }

        public int ID => (int)PageID.Lobby;
        public string BGM => "Lobby";

        protected Blackboard blackboard;
        protected bool IsDone;

        protected PlayerController PlayerController;
        protected PlayerInputWindow playerInputWindow;
        protected PlayerStatHUDWindow playerStatHUDWindow;
        protected ShopWindow ShopWindow;
        protected LoadingScreenImage loadingScreenImage;

        #region Initialize & Terminate

        public void Initialize()
        {
            Transform Client = GameObject.Find("Client").transform;
            Transform Canvas = Client.Find("Canvas");
            Transform Game = Canvas.Find("Game");
            Transform HUD = Game.Find("HUD");
            Transform Lobby = Canvas.Find("Lobby");
            Transform Rect = Canvas.Find("Rect");

            playerStatHUDWindow = HUD.GetComponentInChildren<PlayerStatHUDWindow>();
            playerInputWindow = Rect.GetComponentInChildren<PlayerInputWindow>();
            ShopWindow = Lobby.Find("ShopWindow").GetComponent<ShopWindow>();
            loadingScreenImage = Rect.Find("LoadingScreenImage").GetComponent<LoadingScreenImage>();
        }

        public void Terminate()
        {
        }
        #endregion

        #region Prepare
        public void Prepare()
        {
            ShopWindow.SetSelectButtonEvent(setPlayerID);
            ShopWindow.SetExitEvent(ExitShop);
        }
        
        void setPlayerID(int id)
        {
            GameObject.Destroy(PlayerController.gameObject);
            blackboard.SavePlayerID(id);
            PlayerSpawner PlayerSpawner = GameObject.FindObjectOfType<PlayerSpawner>();
            GameObject PlayerObject = PlayerSpawner.Spawn(id);

            PlayerObject.SetActive(false);
            PlayerObject.transform.position = PlayerController.transform.position;
            PlayerObject.transform.rotation = PlayerController.transform.rotation;
            GameObject.Destroy(PlayerController.gameObject);
            PlayerObject.SetActive(true);

            PlayerController = PlayerObject.GetComponent<PlayerController>();
            PlayerController.onEnterShop = EnterShop;
            PlayerController.onEnterPortal = EnterPortal;

            PlayerController.SetPlayerInput(playerInputWindow);
            PlayerController.SetPlayerHUD(playerStatHUDWindow);
        }
        #endregion


        public IEnumerator Enter()
        {
            yield return null;
            IsDone = false;


            loadingScreenImage.ShowSequence.Play();
            yield return Client.Instance.StartCoroutine(LoadingProcess());
            yield return new WaitForSeconds(1f);

            loadingScreenImage.HideSequence.Play();

            yield return null;
        }

        protected IEnumerator LoadingProcess()
        {
            /// 스테이지 씬 로딩
            AsyncOperation Op = SceneManager.LoadSceneAsync("LobbyOrigin", LoadSceneMode.Additive);
            while (true)
            {
                yield return null;

                /// 스테이지 씬이 로딩될때까지 대기
                if (Op.isDone)
                    break;
            }

            /// 스포너를 얻어온다.
            void SetupPlayer()
            {
                PlayerSpawner PlayerSpawner = GameObject.FindObjectOfType<PlayerSpawner>();
                GameObject PlayerObject = PlayerSpawner.Spawn(blackboard.PlayerID);

                PlayerController = PlayerObject.GetComponent<PlayerController>();
                PlayerController.onEnterShop = EnterShop;
                PlayerController.onEnterPortal = EnterPortal;

                PlayerController.SetPlayerInput(playerInputWindow);
                PlayerController.SetPlayerHUD(playerStatHUDWindow);

            }
            SetupPlayer();
            playerInputWindow.ShowSequence.Play();

            IsDone = true;
        }

        public void Update()
        {
            if (!IsDone) return;
            PlayerController?.OnUpdate();
        }

        public void LateUpdate()
        {
            if (!IsDone) return;
            PlayerController?.OnLateUpdate();
        }

        public IEnumerator Exit()
        {
            SoundManager.instance.StopAll();
            loadingScreenImage.ShowSequence.Play();
            yield return new WaitForSeconds(0.8f);
            AsyncOperation Op = SceneManager.UnloadSceneAsync("LobbyOrigin");
            while (true)
            {
                yield return null;

                /// 스테이지 씬이 로딩될때까지 대기
                if (Op.isDone)
                    break;
            }
            GameObject.Destroy(PlayerController.gameObject);
            PlayerController = null;
        }


        protected void EnterPortal(Portal portal)
        {
            blackboard.SaveLinkedSceneName(portal.GetLinkedSceneName());

            PageManager.Change(PageID.Game);
        }

        protected void EnterShop()
        {
            playerInputWindow.ResetInput();
            playerInputWindow.HideSequence.Play();
            playerInputWindow.ResetInput();

            playerStatHUDWindow.HideSequence.Play();

            ShopWindow.ShowSequence.Play();

        }

        protected void ExitShop()
        {
            playerInputWindow.ShowSequence.Play();
            ShopWindow.HideSequence.Play();
        }
    }
}