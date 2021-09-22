namespace Common.Scripts.MissionSystem
{
    public interface ISatelliteController
    {
        public void Spawn();

        public void Execute();
        public void DisposeLastSatellite();

        public void ScopeToNextSatellite();

    }
}