namespace Common.Scripts.MissionSystem
{
    public interface IAsteroidController : ISpaceObjectController
    {
        public void DisposeLastObject();

        public void ScopeToNextObject();
    }
}