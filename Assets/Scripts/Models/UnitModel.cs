using System;
using UnityEngine;
using Views;

namespace Models
{
    public class UnitModel
    {
        public Action<float> HealthChanged = delegate { };
        public Action<SquareView> SquareViewChanged = delegate { };

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

        public SquareView SquareView
        {
            get
            {
                return _squareView;
            }
            private set
            {
                if (_squareView != null &&
                    value != null)
                {
                    Debug.Log($"Unit moved from square with coordinates {_squareView.Position.x}:{_squareView.Position.y}" +
                              $" to square with coordinates {value.Position.x}:{value.Position.y}");
                }

                _squareView = value;

                SquareViewChanged.Invoke(_squareView);
            }
        }

        private float _health;
        private SquareView _squareView;

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

        public void Move(SquareView squareView)
        {
            SquareView = squareView;
        }
    }
}
