using UnityEngine;

namespace Views
{
    public class SquareView : MonoBehaviour
    {
        public UnitView UnitView;

        public void Initialize(UnitView unitView)
        {
            _unitView = unitView;
        }
    }
}
