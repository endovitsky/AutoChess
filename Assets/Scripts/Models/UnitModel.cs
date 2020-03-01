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

        public string TeamName { get; private set; }

        public float _health;

        public UnitModel(float health, string teamName)
        {
            Health = health;
            TeamName = teamName;
        }

        public void TakeDamage(float amount)
        {
            Health -= amount;
        }
    }
}
