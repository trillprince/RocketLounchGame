using UnityEngine;

namespace Common.Scripts.Infrastructure
{
    public interface IAssetProvider
    {
        public T InstantiateForComponent<T>(string path);
    }
}