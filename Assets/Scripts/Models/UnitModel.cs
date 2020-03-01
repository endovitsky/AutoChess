using System;
using UnityEngine;
using Views;

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

        public SquareView SquareView { get; private set; }

        public float _health;

        public UnitModel(float health, string teamName, SquareView squareView)
        {
            Health = health;
            TeamName = teamName;
            SquareView = squareView;
        }

        public void TakeDamage(float amount)
        {
            Health -= amount;
        }
    }
}
