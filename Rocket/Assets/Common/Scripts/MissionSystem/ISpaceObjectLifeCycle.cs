using Common.Scripts.SpaceObjects;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public interface ISpaceObjectLifeCycle: IEnablable
    {
        public ISpaceObject Spawn(ISpawnPosition spawnPosition,GameObject prefab);

        public void Execute();
        
        public void Dispose(ISpaceObject spaceObject);

    }
}