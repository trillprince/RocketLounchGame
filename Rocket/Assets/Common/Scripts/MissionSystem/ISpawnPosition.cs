using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public interface ISpawnPosition
    {
        public Vector3 GetSpawnPosition(MeshCollider meshCollider);
    }
}