using UnityEngine;
using UnityEngine.UI;
using Views;

namespace Components
{
    public class HealthBarComponent : MonoBehaviour
    {
        [SerializeField]
        private Image _healthBar;

        private UnitView _unitView;

        private float _startHealth;

        private void Start()
        {
            _unitView = this.gameObject.GetComponentInParent<UnitView>();
            if (_unitView == null)
            {
                Debug.LogError("No UnitView was found in parent game object");
            }

            _startHealth = _unitView.UnitModel.Health;

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
            _healthBar.fillAmount = health / _startHealth;

            if (health <= 0)
            {
                Destroy(_unitView.gameObject);
            }
        }
    }
}
