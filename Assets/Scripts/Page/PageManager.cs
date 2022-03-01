using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Boby
{
    public class PageManager
    {
        static PageManager Instance;

        public static void Change(PageID NextID)
        {
            Debug.Log(NextID + "Nextid");
            Instance.TryChange(NextID);
        }

        protected Dictionary<PageID,IPage> Pages;
        protected IPage ActivePage;
        protected Blackboard blackboard;

        public void Initialize()
        {
            Instance = this;
            
            Pages = new Dictionary<PageID,IPage>();
            blackboard = new Blackboard();

            Intro Intro = new Intro(blackboard);
            Intro.Initialize();

            Lobby Lobby = new Lobby(blackboard);
            Lobby.Initialize();

            InGame InGame = new InGame(blackboard);
            InGame.Initialize();

            Outtro Outtro = new Outtro(blackboard);
            Outtro.Initialize();

            //Gameover gameover = new Gameover(blackboard);
            //gameover.Initialize();

            TryAdd(Intro);
            TryAdd(Lobby);
            TryAdd(InGame);
            TryAdd(Outtro);
            //TryAdd(gameover);
        }

        public void Terminate()
        {           
            foreach (var item in Pages)
            {
                item.Value.Terminate();
            }

            Pages.Clear(); 
            Pages = null;
        }

        public void Prepare()
        {
            foreach (var item in Pages)
            {
                item.Value.Prepare();
            }
        }

        public void OnUpdate()
        {
            Debug.Log(ActivePage + " activate");
           ActivePage?.Update();
        }
        public void OnLateUpdate()
        {
            ActivePage?.LateUpdate();
        }

        public void TryChange(PageID PageId)
        {
            Debug.Log("Change " + PageId);
            Coroutine.instance.StartCoroutine(ChangeEnumerator(PageId));
        }

        private IEnumerator ChangeEnumerator(PageID PageId)
        {
            if (!Pages.ContainsKey(PageId))
            {
                Debug.LogError(PageId + "Haven't key");
                yield break;
            }

            if (ActivePage != null)
            {
                yield return Coroutine.instance.StartCoroutine(ActivePage.Exit());
            }
            
            ActivePage = Pages[PageId];
            ActivePage.Prepare();
            yield return Coroutine.instance.StartCoroutine(ActivePage.Enter());
            SoundManager.instance.PlayBgm(ActivePage.BGM);
            yield return null;
        }

        public void TryAdd(IPage Page)
        {
            if (Pages.ContainsKey((PageID)Page.ID))
                return;

            Add(Page);
        }

        void Add(IPage AddPage)
        {
            Pages.Add((PageID)AddPage.ID, AddPage);
        }

        public void TryRemove(PageID Page)
        {
            Pages.Remove(Page);
        }
    }
}
