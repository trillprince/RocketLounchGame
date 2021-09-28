namespace Common.Scripts.MissionSystem
{
    public interface ISpaceObjectController: IEnabable
    {
        public void Spawn();

        public void Execute();

        public void DisposeLastObject();

        public bool ObjectExist();

        public void ScopeToNextObject();
    }
}