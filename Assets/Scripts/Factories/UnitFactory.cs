using System;
using System.Collections.Generic;
using Managers;
using Models;
using Services;
using UnityEngine;
using Views;

namespace Factories
{
    public class UnitFactory : MonoBehaviour
    {
        public Action<UnitModel> UnitInstantiated = delegate { };
        public Action<UnitModel> UnitDestroyed = delegate { };

        [SerializeField]
        private UnitView _unitViewPrefab;

        private List<UnitView> _unitViewInstances = new List<UnitView>();

        public void Initialize()
        {
            SelectedGameStateChanged(GameManager.Instance.GameStateService.SelectedGameState);
            GameManager.Instance.GameStateService.SelectedGameStateChanged += SelectedGameStateChanged;
        }

        public void TryInstantiateUnit(SquareView squareView)
        {
            var teamName = "";

            var rightBorderPositionX = GameManager.Instance.ChessBoardConfigurationService.Width;
            var leftBorderPositionX = 0;

            if ((squareView.gameObject.transform.localPosition.x - leftBorderPositionX > GameManager.Instance.UnitsSpawningConfigurationService.StepFromLeft) &&
                (rightBorderPositionX - squareView.gameObject.transform.localPosition.x >= GameManager.Instance.UnitsSpawningConfigurationService.StepFromRight))
            {
                return;
            }

            if (squareView.gameObject.transform.localPosition.x - leftBorderPositionX <=
                GameManager.Instance.UnitsSpawningConfigurationService.StepFromLeft) // near the left border
            {
                teamName = "Blue";
            }
            if (rightBorderPositionX - squareView.gameObject.transform.localPosition.x <
                GameManager.Instance.UnitsSpawningConfigurationService.StepFromRight) // near the right border
            {
                teamName = "Red";
            }

            if (!GameManager.Instance.UnitsCountService.IsCanSpawnUnitForTeam(teamName))
            {
                return;
            }

            InstantiateUnit(squareView, teamName);
        }
        public void InstantiateUnit(SquareView squareView, string teamName)
        {
            UnitView instance = null;

            instance = Instantiate(_unitViewPrefab, squareView.gameObject.transform);

            instance.Initialize(new UnitModel(
                GameManager.Instance.UnitConfigurationService.InitialHealth,
                GameManager.Instance.UnitConfigurationService.AttackDamage,
                teamName,
                squareView,
                instance));

            squareView.UnitView = instance;

            _unitViewInstances.Add(instance);
            UnitInstantiated.Invoke(instance.UnitModel);

            instance.gameObject.GetComponent<SpriteRenderer>().sprite =
                GameManager.Instance.TexturesResourcesManager.Get("Units", teamName);
        }
        public void DestroyUnit(UnitView unitView)
        {
            var unitViewInstancesForDestroying = new List<UnitView>();

            foreach (var unitViewInstance in _unitViewInstances)
            {
                if (unitViewInstance == unitView)
                {
                    unitViewInstancesForDestroying.Add(unitViewInstance);
                }
            }

            foreach (var unitViewInstanceForDestroying in unitViewInstancesForDestroying)
            {
                _unitViewInstances.Remove(unitViewInstanceForDestroying);
                UnitDestroyed.Invoke(unitViewInstanceForDestroying.UnitModel);

                Destroy(unitViewInstanceForDestroying.gameObject);
            }
        }

        private void SelectedGameStateChanged(GameStateService.GameState gameState)
        {
            if (gameState != GameStateService.GameState.NotStarted)
            {
                return;
            }

            foreach (var unitView in _unitViewInstances)
            {
                Destroy(unitView.gameObject);
            }

            _unitViewInstances.Clear();
        }
    }
}
