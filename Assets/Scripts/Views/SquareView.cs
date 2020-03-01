using UnityEngine;

namespace Views
{
    public class SquareView : MonoBehaviour
    {
        public UnitView _unitView;

        public void Initialize(UnitView unitView)
        {
            _unitView = unitView;
        }
    }
}
