using UnityEngine;
using Zenject;

namespace Common.Scripts
{
    public class LounchManager : MonoBehaviour
    {
        public delegate void Station(bool engineEnabled);
        public static event Station RocketLounch;
        public static event Station MiddleEngineEnable;
        public static event Station MiddleEngineDisable;
        

        public void MiddleEngine(bool isEnabled)
        {
            if (isEnabled)
            {
                MiddleEngineEnable(isEnabled);
                return;
            }

            MiddleEngineDisable(isEnabled);
        }
    }
}
