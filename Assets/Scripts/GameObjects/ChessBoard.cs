using System.Collections.Generic;
using UnityEngine;

namespace GameObjects
{
    public class ChessBoard : MonoBehaviour
    {
        [SerializeField]
        public int _height = 6;
        [SerializeField]
        public int _width = 8;
        [SerializeField]
        public Transform _squarePrefab;

        private List<List<Transform>> _squares = new List<List<Transform>>();

        public void Initialize()
        {
            for (var x = 0; x < _width; x++)
            {
                _squares.Add(new List<Transform>());

                for (var y = 0; y < _height; y++)
                {
                    var instance = Instantiate(_squarePrefab, this.gameObject.transform);
                    instance.gameObject.transform.localPosition = new Vector3(x, y);

                    _squares[x].Add(instance);
                }
            }
        }
    }
}
