using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class GameObjectsManager : MonoBehaviour
    {
        [SerializeField]
        private int _height = 6;
        [SerializeField]
        private int _width = 8;
        [SerializeField]
        private Transform _cellPrefab;

        private List<List<Transform>> _cells = new List<List<Transform>>();

        public void Initialize()
        {
            for (var x = 0; x < _width; x++)
            {
                _cells.Add(new List<Transform>());

                for (var y = 0; y < _height; y++)
                {
                    var instance = Instantiate(_cellPrefab, this.gameObject.transform);
                    instance.transform.position = new Vector3(x, y);

                    _cells[x].Add(instance);
                }
            }
        }
    }
}
