﻿using System.Collections.Generic;
using Managers;
using Models;
using Services;
using UnityEngine;
using Views;

namespace Factories
{
    public class UnitFactory : MonoBehaviour
    {
        [SerializeField]
        private UnitView _unitViewPrefab;

        private Dictionary<string, List<UnitView>> _teamUnitViewInstances = new Dictionary<string, List<UnitView>>();

        public void Initialize()
        {
            // by default there is no instances of units of any team
            foreach (var teamName in GameManager.Instance.TeamsConfigurationService.TeamNames)
            {
                _teamUnitViewInstances.Add(teamName, new List<UnitView>());
            }

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

            _teamUnitViewInstances[teamName].Add(instance);
            GameManager.Instance.UnitsCountService.IncreaseUnitsCountForTeam(teamName);
            GameManager.Instance.UnitsStateMonitoringService.RegisterUnitForStateMonitoring(instance.UnitModel);

            instance.gameObject.GetComponent<SpriteRenderer>().sprite =
                GameManager.Instance.TexturesResourcesManager.Get("Units", teamName);
        }

        public void DestroyUnit(UnitView unitView)
        {
            foreach (var keyValuePair in _teamUnitViewInstances)
            {
                foreach (var unitViewInstance in keyValuePair.Value)
                {
                    if (unitViewInstance == unitView)
                    {
                        Destroy(unitViewInstance.gameObject);

                        keyValuePair.Value.Remove(unitViewInstance);
                    }
                }

                if (keyValuePair.Value.Count == 0)
                {
                    _teamUnitViewInstances.Remove(keyValuePair.Key);
                }
            }
        }

        private void SelectedGameStateChanged(GameStateService.GameState gameState)
        {
            if (gameState != GameStateService.GameState.NotStarted)
            {
                return;
            }

            foreach (var keyValuePair in _teamUnitViewInstances)
            {
                foreach (var unitView in keyValuePair.Value)
                {
                    Destroy(unitView.gameObject);
                }
            }

            _teamUnitViewInstances.Clear();
        }
    }
}
