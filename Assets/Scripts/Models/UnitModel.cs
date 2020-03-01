using System;

namespace Models
{
    public class UnitModel
    {
        public Action<float> HealthChanged = delegate { };

        public float Health
        {
            get
            {
                return _health;
            }
            private set
            {
                _health = value;

                HealthChanged.Invoke(_health);
            }
        }

        public bool IsDead
        {
            get
            {
                return Health <= 0;
            }
        }

        public float _health;

        public UnitModel(float health)
        {
            Health = health;
        }

        public void TakeDamage(float amount)
        {
            Health -= amount;
        }
    }
}
