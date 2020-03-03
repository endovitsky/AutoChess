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
        public float AttackDamage { get; private set; }

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

                // need to update square too
                if (_squareView != null)
                {
                    _squareView.UnitView = null;
                }
                if (value != null)
                {
                    value.UnitView = this.UnitView;
                }

                _squareView = value;

                SquareViewChanged.Invoke(_squareView);
            }
        }
        public UnitView UnitView;

        private float _health;
        private SquareView _squareView;

        public UnitModel(float health, float attackDamage, string teamName, SquareView squareView, UnitView unitView)
        {
            Health = health;
            AttackDamage = attackDamage;

            TeamName = teamName;

            SquareView = squareView;
            UnitView = unitView;
        }

        public void Move(SquareView squareView)
        {
            SquareView = squareView;
        }

        public void Attack(UnitModel unitModel)
        {
            unitModel.Health -= this.AttackDamage;
        }
    }
}
