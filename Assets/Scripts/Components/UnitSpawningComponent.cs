﻿using Managers;
using UnityEngine;
using Views;

namespace Components
{
    [RequireComponent(typeof(SquareView))]
    public class UnitSpawningComponent : MonoBehaviour
    {
        private void Start()
        {
            GameManager.Instance.UnitFactory.TryInstantiateUnit(this.gameObject.GetComponent<SquareView>());
        }
    }
}
