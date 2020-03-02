using Managers;
using UnityEngine;
using Views;

namespace Components
{
    [RequireComponent(typeof(SquareView))]
    public class UnitSpawningComponent : MonoBehaviour
    {
        private void Start()
        {
            //GameManager.Instance.UnitFactory.TryInstantiateUnit(this.gameObject.GetComponent<SquareView>());

            var bluePosition = new Vector3(0, 0, 0);
            var redPosition = new Vector3(2, 2, 0);

            if (this.gameObject.transform.localPosition == bluePosition)
            {
                GameManager.Instance.UnitFactory.InstantiateUnit(this.gameObject.GetComponent<SquareView>(), "Blue");
            }
            if (this.gameObject.transform.localPosition == redPosition)
            {
                GameManager.Instance.UnitFactory.InstantiateUnit(this.gameObject.GetComponent<SquareView>(), "Red");
            }
        }
    }
}
