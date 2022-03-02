


using UnityEngine;

namespace Bobby
{
    public class Client : MonoBehaviour
    {
        public static Client Instance { get; private set; }

        DataBaseManager DataBaseManager;
        UIManager UIManager;
        PageManager PageManager;
        SoundManager SoundManager;

        private void Awake()
        {
            Instance = this;

            DataBaseManager = new DataBaseManager();
            DataBaseManager.Initialize();

            UIManager = new UIManager();
            UIManager.Initialize();

            PageManager = new PageManager();
            PageManager.Initialize();

            SoundManager = new SoundManager();
        }

        private void Start()
        {
            PageManager.Prepare();
            PageManager.TryChange(PageID.Intro);
        }

        private void Update()
        {
            PageManager.OnUpdate();
        }

        private void LateUpdate()
        {
            PageManager.OnLateUpdate();
        }

        private void OnDestroy()
        {
            DataBaseManager.Terminate();
            DataBaseManager = null;

            UIManager.Terminate();
            UIManager = null;

            PageManager.Terminate();
            PageManager = null;
        }
    }
}
