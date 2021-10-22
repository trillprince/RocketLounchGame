using UnityEngine;

namespace Common.Scripts.Application
{
    public class ScreenControl
    {
        public ScreenControl()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
    }
}