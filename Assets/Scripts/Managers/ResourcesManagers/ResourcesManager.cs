using UnityEngine;

namespace Managers.ResourcesManagers
{
    public class ResourcesManager<T> : MonoBehaviour where T : UnityEngine.Object
    {
        private string _resourcesFolderName = "";

        public virtual void Initialize(string resourcesFolderName)
        {
            _resourcesFolderName = resourcesFolderName;
        }

        public virtual T Get(params string[] pathNames)
        {
            var path = _resourcesFolderName + "/" + string.Join("/", pathNames);

            var resource = Resources.Load<T>(path);

            if (resource == null)
            {
                Debug.LogWarning(string.Format("No object was provided for {0} - loaded resource is empty.", path));
            }

            return resource;
        }

        public virtual T[] GetAll(string additionalPath = "")
        {
            var path = _resourcesFolderName;
            if (additionalPath != "")
            {
                path += "/" + additionalPath;
            }
            var resource = Resources.LoadAll<T>(path);

            if (resource == null)
            {
                Debug.LogWarning(string.Format("No object was provided for {0} - loaded resource is empty.", path));
            }

            return resource;
        }
    }
}