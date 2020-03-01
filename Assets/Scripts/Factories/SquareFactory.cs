using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Factories
{
    public class SquareFactory : MonoBehaviour
    {
        [SerializeField]
        private Transform _squarePrefab;

        public Vector3 SquareSize
        {
            get { return _squarePrefab.gameObject.GetComponent<SpriteRenderer>().bounds.size; }
        }

        private List<List<Transform>> _squareInstances = new List<List<Transform>>();

        public void InstantiateSquares(Transform parent)
        {
            for (var indexX = 0; indexX < GameManager.Instance.ChessBoardConfigurationService.Width; indexX++)
            {
                _squareInstances.Add(new List<Transform>());

                for (var indexY = 0; indexY < GameManager.Instance.ChessBoardConfigurationService.Height; indexY++)
                {
                    var instance = InstantiateSquare(parent, indexX, indexY);

                    _squareInstances[indexX].Add(instance);
                }
            }
        }

        private Transform InstantiateSquare(Transform parent, int indexX, int indexY)
        {
            var instance = Instantiate(_squarePrefab, parent);
            var position = new Vector3(indexX + 1, indexY + 1); // from indexes to coordinates
            instance.gameObject.transform.localPosition = position;
            return instance;
        }
    }
}
