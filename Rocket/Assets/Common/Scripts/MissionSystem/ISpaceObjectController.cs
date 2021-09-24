namespace Common.Scripts.MissionSystem
{
    public interface ISpaceObjectController
    {
        public void Spawn();

        public void Execute();

        public void DisposeAll();
    }
}