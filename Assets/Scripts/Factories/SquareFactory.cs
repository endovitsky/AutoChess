using System;
using System.Collections.Generic;
using Managers;
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

        //TODO: move this method to more correct place
        public SquareView GetSquareViewByPosition(Vector2 position)
        {
            SquareView result = null;

            foreach (var squareViews in SquareInstances)
            {
                foreach (var squareView in squareViews)
                {
                    var floatComparisonTolerance = 0.001f;
                    if (Math.Abs(squareView.gameObject.transform.localPosition.x - position.x) < floatComparisonTolerance &&
                        Math.Abs(squareView.gameObject.transform.localPosition.y - position.y) < floatComparisonTolerance)
                    {
                        return squareView;
                    }
                }
            }

            return result;
        }
    }
}
