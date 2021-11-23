using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class PlayerDataSaver
    {

        public PlayerDataSaver()
        {
            
        }

        public void SaveScore(int score)
        {
            if(score <= PlayerPrefs.GetInt("HighScore")) return;
            PlayerPrefs.SetInt("HighScore",score);
        }

        public int GetScore()
        {
            return PlayerPrefs.GetInt("HighScore");
        }

        public void SaveCoins(int coinsValue)
        {
            PlayerPrefs.SetInt("Dog Coins",PlayerPrefs.GetInt("Dog Coins") + coinsValue);
        }

        public int GetCoins()
        {
            return PlayerPrefs.GetInt("Dog Coins");
        }
        
    }
}