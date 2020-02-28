using UnityEngine;

namespace Components
{
    public class UnitSpawningComponent : MonoBehaviour
    {
        [SerializeField]
        private Transform _unitPrefab;

        private Transform _unitInstance;

        private void Start()
        {
            _unitInstance = Instantiate(_unitPrefab, this.gameObject.transform);
        }
    }
}
