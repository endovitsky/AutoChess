using Managers;
using UnityEngine;
using Views;

namespace Components
{
    [RequireComponent(typeof(UnitView))]
    public class IsDeadUnitDestroyComponent : MonoBehaviour
    {
        private UnitView _unitView;

        private void Start()
        {
            _unitView = this.gameObject.GetComponentInParent<UnitView>();

            OnHealthChanged(_unitView.UnitModel.Health);
            _unitView.UnitModel.HealthChanged += OnHealthChanged;
        }
        private void OnDestroy()
        {
            if (_unitView.UnitModel != null)
            {
                _unitView.UnitModel.HealthChanged -= OnHealthChanged;
            }
        }

        private void OnHealthChanged(float health)
        {
            if (health <= 0)
            {
                GameManager.Instance.UnitFactory.DestroyUnit(_unitView);
            }
        }
    }
}
