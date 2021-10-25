namespace Common.Scripts.MissionSystem
{
    public class PlayerDataSaver
    {
        private readonly PlayerData _playerData;

        public PlayerDataSaver(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public void SaveScore(int score)
        {
            if(score <= _playerData.GetHighScore()) return;
            _playerData.SetHighScore(score);
        }

        public int GetScore()
        {
            return _playerData.GetHighScore();
        }
        
    }
}