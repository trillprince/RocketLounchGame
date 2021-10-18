using UnityEngine;

namespace Common.Scripts.UI
{
    public class GameTimeController: IGameTimeController
    {
        public void Pause()
        {
            Time.timeScale = 0;
        }

        public void UnPause()
        {
            Time.timeScale = 1;
        }
    }
}