namespace Common.Scripts.MissionSystem
{
    public interface IGameLoopController
    {
        public void EnableGameLoop();
        public void DisableGameLoop();

        public ILevelInfo GetLevelInfo();

    }
}