namespace Common.Scripts.MissionSystem
{
    public interface ISpaceObjectController: IEnablable
    {
        public void Spawn();

        public void Execute();

        public void DisposeLastObject();

        public bool ObjectExist();

        public void ScopeToNextObject();
    }
}