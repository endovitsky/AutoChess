using System;
using System.Collections.Generic;
using Models;
using Debug = UnityEngine.Debug;

namespace Services
{
    public class UnitsStateMonitoringService
    {
        public Action<int> AliveUnitModelsCountChanged = delegate { };

        private List<UnitModel> _unitModels = new List<UnitModel>();
        private List<UnitModel> _aliveUnitModels = new List<UnitModel>();

        public void RegisterUnitForStateMonitoring(UnitModel unitModel)
        {
            _unitModels.Add(unitModel);

            if (!unitModel.IsDead)
            {
                var previousCount = _aliveUnitModels.Count;

                _aliveUnitModels.Add(unitModel);

                Debug.Log($"Alive unit models count changed from {previousCount} to {_aliveUnitModels.Count}");

                AliveUnitModelsCountChanged.Invoke(_aliveUnitModels.Count);
            }

            unitModel.HealthChanged += health =>
            {
                if (unitModel.IsDead)
                {
                    var previousCount = _aliveUnitModels.Count;

                    _aliveUnitModels.Remove(unitModel);

                    Debug.Log($"Alive unit models count changed from {previousCount} to {_aliveUnitModels.Count}");

                    AliveUnitModelsCountChanged.Invoke(_aliveUnitModels.Count);
                }
            };
        }
    }
}
