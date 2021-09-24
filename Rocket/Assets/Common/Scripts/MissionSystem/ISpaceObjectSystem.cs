using Common.Scripts.Cargo;

namespace Common.Scripts.MissionSystem
{
    public interface ISpaceObjectSystem: IUpdatable
    {
        public void SpawnRandomSideSatellite();
    }
}