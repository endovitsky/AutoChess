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

            PositionChanged(UnitModel.Position);
            UnitModel.PositionChanged += PositionChanged;
        }

        private void PositionChanged(Vector2 position)
        {
            this.gameObject.transform.localPosition = new Vector3(
                position.x, 
                position.y, 
                this.gameObject.transform.localPosition.z);
        }
    }
}
