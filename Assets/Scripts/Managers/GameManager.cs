using Factories;
using Managers.ResourcesManagers;
using Services;
using Services.ChessBoards;
using Services.Teams;
using Services.Units;
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

        public TimerService TimerService;
        public ChessBoardConfigurationService ChessBoardConfigurationService;
        public UnitsSpawningConfigurationService UnitsSpawningConfigurationService;
        public TeamsConfigurationService TeamsConfigurationService;
        public UnitConfigurationService UnitConfigurationService;
        public GameStateService GameStateService;
        public UnitsCountService UnitsCountService;
        public UnitsStateMonitoringService UnitsStateMonitoringService;
        public TeamsStateMonitoringService TeamsStateMonitoringService;
        public PathFindingService PathFindingService;
        public UnitsPathProvidingService UnitsPathProvidingService;
        public UnitsMovingService UnitsMovingService;
        public UnitsFightingService UnitsFightingService;

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
            TimerService = new TimerService();
            ChessBoardConfigurationService = new ChessBoardConfigurationService();
            UnitsSpawningConfigurationService = new UnitsSpawningConfigurationService();
            TeamsConfigurationService = new TeamsConfigurationService();
            UnitConfigurationService = new UnitConfigurationService();
            GameStateService = new GameStateService();
            // UnitsCountService need TeamsConfigurationService
            // UnitsCountService need UnitsSpawningConfigurationService
            UnitsCountService = new UnitsCountService();
            UnitsStateMonitoringService = new UnitsStateMonitoringService();
            TeamsStateMonitoringService = new TeamsStateMonitoringService();
            PathFindingService = new PathFindingService();
            UnitsPathProvidingService = new UnitsPathProvidingService();
            UnitsMovingService = new UnitsMovingService();
            UnitsFightingService = new UnitsFightingService();

            TeamsConfigurationService.Initialize();
            // UnitFactory need TeamsConfigurationService
            UnitFactory.Initialize();

            TimerService.Initialize();
            ChessBoardConfigurationService.Initialize();
            UnitsSpawningConfigurationService.Initialize();
            UnitConfigurationService.Initialize();
            GameStateService.Initialize();
            UnitsCountService.Initialize();
            // UnitsStateMonitoringService need TeamsConfigurationService
            UnitsStateMonitoringService.Initialize();
            // TeamsStateMonitoringService on Initialize need
            //     UnitsStateMonitoringService
            // TeamsStateMonitoringService need
            //     TeamsConfigurationService
            TeamsStateMonitoringService.Initialize();
            // UnitsPathProvidingService need SelectedGameStateChanged
            UnitsPathProvidingService.Initialize();
            // UnitsMovingService need TimerService
            // UnitsMovingService need UnitsPathProvidingService
            UnitsMovingService.Initialize();
            // UnitsFightingService on Initialize need
            //     TimerService
            // UnitsFightingService need
            //     GameStateService,
            //     TeamsConfigurationService,
            //     UnitsStateMonitoringService,
            //     UnitsPathProvidingService
            UnitsFightingService.Initialize();
            // GameObjectsManager need ChessBoardConfigurationService
            GameObjectsManager.Initialize();
            UserInterfaceManager.Initialize();
            TexturesResourcesManager.Initialize("Textures");
        }
    }
}
