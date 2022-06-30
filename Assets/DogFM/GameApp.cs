using System;
using DogFM.Backpack;

namespace DogFM
{
    /// <summary>
    /// 游戏主流程
    /// 作为中介者，拥有各个子系统的调用接口
    /// </summary>
    public class GameApp : MonoSingleton<GameApp>
    {
        public static SceneStateController sceneStateController = new SceneStateController();
        public static InventorySystem inventorySystem = new InventorySystem();

        private void Awake()
        {
            DontDestroyOnLoad(this);
            UIMgr.Instance.Init();
            NetworkMgr.Instance.Init();
        }

        private void Start()
        {
            sceneStateController.SetState(new ChatScene(sceneStateController), "Chat");
        }

        private void Update()
        {
            if (sceneStateController != null)
            {
                sceneStateController.StateUpdate();
            }
        }

        public void LoadScene(string sceneName)
        {
            ISceneState sceneState = null;
            switch (sceneName)
            {
                case "Login":
                    sceneState = new LoginScene(sceneStateController);
                    break;
            }

            sceneStateController.SetState(sceneState, sceneName);
        }
    }
}