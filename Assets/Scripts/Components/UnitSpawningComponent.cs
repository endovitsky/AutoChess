using Managers;
using UnityEngine;

namespace Components
{
    public class UnitSpawningComponent : MonoBehaviour
    {
        private void Start()
        {
            GameManager.Instance.UnitFactory.TrySpawnUnit(this.gameObject.transform);
        }
    }
}
