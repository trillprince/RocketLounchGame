namespace Common.Scripts.MissionSystem
{
    public class GameProgress
    {
        public PlayerDataSaver PlayerDataSaver { get; }

        public GameProgress(PlayerDataSaver playerPlayerDataSaver)
        {
            PlayerDataSaver = playerPlayerDataSaver;
        } 
    }
}