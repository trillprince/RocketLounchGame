using Common.Scripts.Cargo;

namespace Common.Scripts.MissionSystem
{
    public interface ISatelliteSystem: IUpdatable
    {
        public void SpawnRandomSideSatellite();
    }
}