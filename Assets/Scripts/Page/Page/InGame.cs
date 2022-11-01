using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace Bobby
{
    public class InGame : IPage
    {
        public InGame(Blackboard blackboard)
        {
            this.blackboard = blackboard;
        }

        public int ID => (int)PageID.Game;
        public string BGM => "InGame";
        protected string SceneName;

        protected Blackboard blackboard;
        protected bool IsDone;

        protected PlayerController PlayerController;
        protected PlayerInputWindow playerInputWindow;
        protected PlayerStatHUDWindow playerStatHUDWindow;

        protected StageManager StageManager;

        protected ResultWindow resultWindow;

        protected UIButton attack;
        protected UIButton jump;
        protected Joystick joystick;
        protected LoadingScreenImage loadingScreenImage;


        #region alpha omega
        public void Initialize()
        {
            Transform Client = GameObject.Find("Client").transform;
            Transform Canvas = Client.Find("Canvas");
            Transform Game = Canvas.Find("Game");
            Transform Rect = Canvas.Find("Rect");
            Transform PlayerInputWindow = Game.Find("PlayerInputWindow");
            resultWindow = Game.GetComponentInChildren<ResultWindow>();
            resultWindow.SetLobbyButtonEvent(() => { PageManager.Change(PageID.Lobby); });

            playerInputWindow = Canvas.GetComponentInChildren<PlayerInputWindow>();
            playerStatHUDWindow = Canvas.GetComponentInChildren<PlayerStatHUDWindow>();
            loadingScreenImage = Rect.Find("LoadingScreenImage").GetComponent<LoadingScreenImage>();
        }

        public void Terminate()
        {
        }
        #endregion

        public void Prepare()
        {

        }

        #region
        public  IEnumerator  Enter()
        {   
            IsDone = false;
            loadingScreenImage.ShowSequence.Play();
            yield return Client.Instance.StartCoroutine(LoadingProcess());
            yield return new WaitForSeconds(1f);

            loadingScreenImage.HideSequence.Play();
        }

        protected IEnumerator LoadingProcess()
        {
            void SetupUI()
            {
                playerStatHUDWindow.ShowSequence.Play();
                playerInputWindow.ShowSequence.Play();
            }
            SetupUI();

            SceneName = blackboard.LinkedSceneName;
            /// 스테이지 씬 로딩
            AsyncOperation Op = SceneManager.LoadSceneAsync(blackboard.LinkedSceneName, LoadSceneMode.Additive);
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
                PlayerController.SetPlayerInput(playerInputWindow);
                PlayerController.SetPlayerHUD(playerStatHUDWindow);
                PlayerController.PlayerDie = () => EndGame(false);
            }
            SetupPlayer();
            yield return null;
            void SetupStageManager()
            {
                StageManager = GameObject.FindObjectOfType<StageManager>();
                StageManager.Initialize();
                StageManager.EndGame = () => EndGame(true);
            }
            SetupStageManager();


                IsDone = true;
        }

        public void Update()
        {
            if (!IsDone) return;
            if(PlayerController.enabled) PlayerController?.OnUpdate();
            StageManager?.OnUpdate();
        }

        public void LateUpdate()
        {
            if (!IsDone) return;
            PlayerController?.OnLateUpdate();
            StageManager?.OnLateUpdate();
        }

        public IEnumerator Exit()
        {
            SoundManager.instance.StopAll();

            loadingScreenImage.ShowSequence.Play();
            yield return new WaitForSeconds(0.8f);
            Debug.Log("Unload");
            AsyncOperation Op = SceneManager.UnloadSceneAsync(SceneName);
            while (true)
            {
                yield return null;

                /// 스테이지 씬이 로딩될때까지 대기
                if (Op.isDone)
                    break;
            }
            resultWindow.HideSequence.Play();
            Object.FindObjectOfType<BossHUDWindow>()?.HideSequence.Play();
            MonsterController[] monsters = GameObject.FindObjectsOfType<MonsterController>();
            for (int i = 0; i < monsters.Length; i++)
            {
                GameObject.Destroy(monsters[i].gameObject);
            }
            PoolObj[] Projectiles = GameObject.FindObjectsOfType<PoolObj>();
            for (int i = 0; i < Projectiles.Length; i++)
            {
                GameObject.Destroy(Projectiles[i].gameObject);
            }
            GameObject.Destroy(PlayerController.gameObject);
            Debug.Log("Game Page Exit");
        }
        #endregion

        public void EndGame(bool iswin)
        {
            playerInputWindow.ResetInput();
            playerInputWindow.HideSequence.Play();
            playerStatHUDWindow.HideSequence.Play();


            GameObject.FindObjectOfType<MonsterController>().SetDamageCauser(null);
            resultWindow.SetLobbyButtonEvent(() => { PageManager.Change(PageID.Lobby); });

            if (iswin)
            {
                ClearGame();
                return;
            }
            resultWindow.Resualt(iswin);
        }
        void ClearGame()
        {
            Coroutine.instance.StartCoroutine(AfterClear());
        }
        IEnumerator AfterClear()
        {
            ParticleSystem ThunderParticle = GameObject.Instantiate(Resources.Load<GameObject>("FX/Thunder")).GetComponent<ParticleSystem>();
            FollowCam cam = GameObject.Find("Main Camera").GetComponent<FollowCam>();
            PlayerController.immortality = true;
            MonsterController[] monsters = GameObject.FindObjectsOfType<MonsterController>();
            for (int i = 0; i < 10; i++)
            {
                ThunderParticle.transform.position = monsters[i].transform.position;
                GameObject.Destroy(monsters[i].gameObject);
                cam.Shake(1f,1);
                ThunderParticle.Play();
                SoundManager.instance.PlayEffect("Thunder");
                yield return new WaitForSeconds(Random.Range(0.37f, 0.45f));
            }
            var lastmonster = Object.FindObjectsOfType<MonsterController>();
            foreach (var item in lastmonster)
            {
                Object.Destroy(item.gameObject);
            }
            var heal = GameObject.FindObjectsOfType<HealingHeart>();
            foreach (var item in heal)
            {
                Object.Destroy(item.gameObject);
            }
            GameObject.Destroy(ThunderParticle);
            yield return new WaitForSeconds(2);
            playerInputWindow.ResetInput();
            playerInputWindow.HideSequence.Play();
            playerStatHUDWindow.HideSequence.Play();
            resultWindow.SetLobbyButtonEvent(() => { PageManager.Change(PageID.Outtro); });
            resultWindow.Resualt(true);
        }
    }
}
