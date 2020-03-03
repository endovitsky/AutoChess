using System;
using UnityEngine;
using Views;

namespace Models
{
    public class UnitModel
    {
        public Action<float> HealthChanged = delegate { };
        public Action<Vector2> PositionChanged = delegate { };

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

        public Vector2 Position
        {
            get
            {
                return _position;
            }
            private set
            {
                if (_position != null &&
                    value != null)
                {
                    Debug.Log($"Unit moved from square with coordinates {_position.x}:{_position.y}" +
                              $" to square with coordinates {value.x}:{value.y}");
                }

                _position = value;

                PositionChanged.Invoke(_position);
            }
        }

        public UnitView UnitView;

        private float _health;
        private Vector2 _position;

        public UnitModel(float health, string teamName, Vector2 position, UnitView unitView)
        {
            Health = health;
            TeamName = teamName;
            Position = position;
            UnitView = unitView;
        }

        public void TakeDamage(float amount)
        {
            Health -= amount;
        }

        public void Move(Vector2 position)
        {
            Position = position;
        }
    }
}
