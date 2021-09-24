namespace Common.Scripts.MissionSystem
{
    public interface ISatelliteController: ISpaceObjectController
    {
        public void DisposeLastSatellite();

        public void ScopeToNextSatellite();
    }
}