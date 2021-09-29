using UnityEngine;

namespace Common.Scripts.Infrastructure
{
    public class BootstrapProvider : IAssetProvider
    {
                
        public T InstantiateForComponent <T> (string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab).GetComponent<T>();
        }
    }
}