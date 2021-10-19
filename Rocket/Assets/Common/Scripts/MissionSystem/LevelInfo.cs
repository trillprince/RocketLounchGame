using System;

namespace Common.Scripts.MissionSystem
{
    public class LevelInfo: ILevelInfo
    {
        private int _levelNumber;

        public event Action OnNextLevel;
            
        
        public void NextLevel()
        {
            _levelNumber++;
            OnNextLevel?.Invoke();
        }
        
        public int GetLevelNumber()
        {
            return _levelNumber;
        }
    }
}