using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public interface ISpawner
    {
        GameObject Spawn();
        void Dispose(GameObject gameObject);
    }
}