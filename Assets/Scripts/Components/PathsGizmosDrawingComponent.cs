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
                for (var i = 0; i < path.Count - 1; i++)
                {
                    Debug.DrawLine(path[i].gameObject.transform.position,
                        path[i + 1].gameObject.transform.position,
                        Color.green);
                }
            }
        }
    }
}
