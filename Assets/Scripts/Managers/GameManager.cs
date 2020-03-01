using Services;
using UnityEngine;

namespace Managers
{
    [RequireComponent(typeof(GameObjectsManager))]
    [RequireComponent(typeof(UserInterfaceManager))]
    public class GameManager : MonoBehaviour
    {
        // static instance of GameManager which allows it to be accessed by any other script 
        public static GameManager Instance;

        public GameObjectsManager GameObjectsManager
        {
            get { return this.gameObject.GetComponent<GameObjectsManager>(); }
        }
        public UserInterfaceManager UserInterfaceManager
        {
            get { return this.gameObject.GetComponent<UserInterfaceManager>(); }
        }

        public ChessBoardConfigurationService ChessBoardConfigurationService;
        public UnitsSpawningConfigurationService UnitsSpawningConfigurationService;
        public GameStateService GameStateService;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;

                DontDestroyOnLoad(gameObject); // sets this to not be destroyed when reloading scene 
            }
            else
            {
                if (Instance != this)
                {
                    // this enforces our singleton pattern, meaning there can only ever be one instance of a GameManager 
                    Destroy(gameObject);
                }
            }

            Initialize();
        }

        private void Initialize()
        {
            ChessBoardConfigurationService = new ChessBoardConfigurationService();
            UnitsSpawningConfigurationService = new UnitsSpawningConfigurationService();
            GameStateService = new GameStateService();

            ChessBoardConfigurationService.Initialize();
            UnitsSpawningConfigurationService.Initialize();
            GameStateService.Initialize();
            // GameObjectsManager need ChessBoardConfigurationService
            GameObjectsManager.Initialize();
            UserInterfaceManager.Initialize();
        }
    }
}
