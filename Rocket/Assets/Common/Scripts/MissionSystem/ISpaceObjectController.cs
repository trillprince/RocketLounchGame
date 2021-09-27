namespace Common.Scripts.MissionSystem
{
    public interface ISpaceObjectController
    {
        public void Spawn();

        public void Execute();

        public void Disable();

        public void Enable();
        
        public void DisposeLastObject();

        public void ScopeToNextObject();
    }
}