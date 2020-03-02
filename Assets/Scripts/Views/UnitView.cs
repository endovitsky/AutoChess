using Models;
using UnityEngine;

namespace Views
{
    public class UnitView : MonoBehaviour
    {
        public UnitModel UnitModel { get; private set; }

        public void Initialize(UnitModel unitModel)
        {
            UnitModel = unitModel;

            SquareViewChanged(UnitModel.SquareView);
            UnitModel.SquareViewChanged += SquareViewChanged;
        }

        private void SquareViewChanged(SquareView squareView)
        {
            this.gameObject.transform.parent = squareView.gameObject.transform;
            this.gameObject.transform.localPosition = new Vector3(0, 0, this.gameObject.transform.localPosition.z);
        }
    }
}
