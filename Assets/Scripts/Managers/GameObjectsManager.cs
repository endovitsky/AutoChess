using System.Collections.Generic;
using Extensions;
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
                    instance.gameObject.transform.position = new Vector3(x, y);

                    var size = instance.gameObject.GetComponent<SpriteRenderer>().GetSize();
                    instance.gameObject.transform.position = new Vector3(
                        instance.gameObject.transform.position.x - _width / 2 + size.x/2,
                        instance.gameObject.transform.position.y - _height / 2 + size.y/2);

                    _cells[x].Add(instance);
                }
            }
        }
    }
}
