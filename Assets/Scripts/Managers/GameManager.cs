using Factories;
using Managers.ResourcesManagers;
using Services;
using UnityEngine;

namespace Managers
{
    [RequireComponent(typeof(GameObjectsManager))]
    [RequireComponent(typeof(UserInterfaceManager))]
    [RequireComponent(typeof(TexturesResourcesManager))]
    [RequireComponent(typeof(ChessBoardFactory))]
    [RequireComponent(typeof(SquareFactory))]
    [RequireComponent(typeof(UnitFactory))]
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
        public TexturesResourcesManager TexturesResourcesManager
        {
            get { return this.gameObject.GetComponent<TexturesResourcesManager>(); }
        }

        public ChessBoardFactory ChessBoardFactory
        {
            get { return this.gameObject.GetComponent<ChessBoardFactory>(); }
        }
        public SquareFactory SquareFactory
        {
            get { return this.gameObject.GetComponent<SquareFactory>(); }
        }
        public UnitFactory UnitFactory
        {
            get { return this.gameObject.GetComponent<UnitFactory>(); }
        }

        public ChessBoardConfigurationService ChessBoardConfigurationService;
        public UnitsSpawningConfigurationService UnitsSpawningConfigurationService;
        public TeamsConfigurationService TeamsConfigurationService;
        public UnitConfigurationService UnitConfigurationService;
        public GameStateService GameStateService;
        public UnitsCountService UnitsCountService;
        public UnitsStateMonitoringService UnitsStateMonitoringService;
        public PathFindingService PathFindingService;
        public UnitsPathProvidingService UnitsPathProvidingService;
        public UnitsMovingService UnitsMovingService;

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
            TeamsConfigurationService = new TeamsConfigurationService();
            UnitConfigurationService = new UnitConfigurationService();
            GameStateService = new GameStateService();
            // UnitsCountService need TeamsConfigurationService
            // UnitsCountService need UnitsSpawningConfigurationService
            UnitsCountService = new UnitsCountService();
            UnitsStateMonitoringService = new UnitsStateMonitoringService();
            PathFindingService = new PathFindingService();
            UnitsPathProvidingService = new UnitsPathProvidingService();
            UnitsMovingService = new UnitsMovingService();

            TeamsConfigurationService.Initialize();
            // UnitFactory need TeamsConfigurationService
            UnitFactory.Initialize();

            ChessBoardConfigurationService.Initialize();
            UnitsSpawningConfigurationService.Initialize();
            UnitConfigurationService.Initialize();
            GameStateService.Initialize();
            UnitsCountService.Initialize();
            // UnitsStateMonitoringService need TeamsConfigurationService
            UnitsStateMonitoringService.Initialize();
            // UnitsPathProvidingService need SelectedGameStateChanged
            UnitsPathProvidingService.Initialize();
            // UnitsMovingService need UnitsPathProvidingService
            UnitsMovingService.Initialize();
            // GameObjectsManager need ChessBoardConfigurationService
            GameObjectsManager.Initialize();
            UserInterfaceManager.Initialize();
            TexturesResourcesManager.Initialize("Textures");
        }
    }
}
