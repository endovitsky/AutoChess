using Managers;
using UnityEngine;
using Views;

namespace Components
{
    [RequireComponent(typeof(SquareView))]
    public class UnitSpawningComponent : MonoBehaviour
    {
        private void Start()
        {
            var squareView = this.gameObject.GetComponent<SquareView>();

            GameManager.Instance.UnitFactory.TryInstantiateUnit(squareView);
        }
    }
}
