using Common.Scripts.SpaceObjects;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public interface ISpaceObjectController: IEnablable
    {
        public ISpaceObject Spawn(ISpawnPosition spawnPosition);

        public void Execute();

        public void DisposeLastObject();
        
    }
}