using Models;
using UnityEngine;

namespace Views
{
    public class UnitView : MonoBehaviour
    {
        public UnitModel UnitModel;

        public void Initialize(UnitModel unitModel)
        {
            UnitModel = unitModel;
        }
    }
}
