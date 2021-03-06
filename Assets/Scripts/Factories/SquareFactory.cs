﻿using System.Collections.Generic;
using Managers;
using Services;
using UnityEngine;
using Views;

namespace Factories
{
    public class SquareFactory : MonoBehaviour
    {
        [SerializeField]
        private SquareView _squarePrefab;

        public Vector3 SquareSize
        {
            get { return _squarePrefab.gameObject.GetComponent<SpriteRenderer>().bounds.size; }
        }

        public List<List<SquareView>> SquareInstances = new List<List<SquareView>>();

        public void Initialize()
        {
            SelectedGameStateChanged(GameManager.Instance.GameStateService.SelectedGameState);
            GameManager.Instance.GameStateService.SelectedGameStateChanged += SelectedGameStateChanged;
        }

        public void InstantiateSquares(Transform parent)
        {
            for (var indexX = 0; indexX < GameManager.Instance.ChessBoardConfigurationService.Width; indexX++)
            {
                SquareInstances.Add(new List<SquareView>());

                for (var indexY = 0; indexY < GameManager.Instance.ChessBoardConfigurationService.Height; indexY++)
                {
                    var instance = InstantiateSquare(parent, indexX, indexY);

                    SquareInstances[indexX].Add(instance);
                }
            }
        }

        private SquareView InstantiateSquare(Transform parent, int indexX, int indexY)
        {
            var instance = Instantiate(_squarePrefab, parent);
            var position = new Vector3(indexX + 1, indexY + 1); // from indexes to coordinates
            instance.gameObject.transform.localPosition = position;
            return instance;
        }

        private void SelectedGameStateChanged(GameStateService.GameState gameState)
        {
            if (gameState != GameStateService.GameState.NotStarted)
            {
                return;
            }

            foreach (var squareViews in SquareInstances)
            {
                foreach (var squareView in squareViews)
                {
                    Destroy(squareView.gameObject);
                }
            }

            SquareInstances.Clear();
        }
    }
}
