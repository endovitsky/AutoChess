using UnityEngine;

namespace Views
{
    public class SquareView : MonoBehaviour
    {
        public UnitView UnitView;

        public Vector2 Position => this.gameObject.transform.localPosition;

        public bool IsWalkable => UnitView == null;

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
