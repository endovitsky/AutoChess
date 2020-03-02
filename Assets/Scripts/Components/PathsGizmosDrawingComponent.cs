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
                for (var i = 0; i < path.Value.Count - 1; i++)
                {
                    Debug.DrawLine(path.Value[i].gameObject.transform.position,
                        path.Value[i + 1].gameObject.transform.position,
                        Color.green);
                }
            }
        }
    }
}
