using Managers;
using UnityEngine;

namespace Components
{
    public class PathsGizmosDrawingComponent : MonoBehaviour
    {
        private void Update()
        {
            foreach (var path in GameManager.Instance.UnitsPathProvidingService.Paths)
            {
                for (var i = 0; i < path.SquareViews.Count - 1; i++)
                {
                    Debug.DrawLine(path.SquareViews[i].gameObject.transform.position,
                        path.SquareViews[i + 1].gameObject.transform.position,
                        Color.green);
                }
            }
        }
    }
}
