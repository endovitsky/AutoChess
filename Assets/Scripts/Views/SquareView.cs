using Extensions;
using UnityEngine;

namespace Views
{
    public class SquareView : MonoBehaviour
    {
        public Vector2 Position => this.gameObject.transform.localPosition;

        public bool IsWalkable
        {
            get
            {
                bool result;

                var unitModel = this.GetUnitModelForSquareView();
                result = unitModel != null;

                return result;
            }
        }

        public int gCost;
        public int hCost;
        public SquareView parent;

        public int fCost
        {
            get
            {
                return gCost + hCost;
            }
        }
	}
}
