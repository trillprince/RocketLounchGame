using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public interface ISpaceObjectSpawner
    {
        GameObject Spawn();
        void Dispose(GameObject gameObject);
    }
}