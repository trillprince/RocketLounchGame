using System.Collections.Generic;
using GooglePlayGames.BasicApi;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class RocketInventory
    {

        public void AddCoinValue(int addValue)
        {
            PlayerPrefs.SetInt("DogCoins",PlayerPrefs.GetInt("DogCoins") + addValue);
        }
    }
}