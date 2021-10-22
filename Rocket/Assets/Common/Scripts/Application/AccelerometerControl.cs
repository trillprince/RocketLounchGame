using UnityEngine;
using UnityEngine.InputSystem;

namespace Common.Scripts.Application
{
    public class AccelerometerControl
    {
        private readonly RuntimePlatform _platform;

        public AccelerometerControl(RuntimePlatform platform, ApplicationController applicationController)
        {
            _platform = platform;
            applicationController.ApplicationFocused += CheckForApplicationFocus; 
            CheckForAccelerometer(_platform);
        }

        private void CheckForAccelerometer(RuntimePlatform platform)
        {
            if (platform == RuntimePlatform.Android)
            {
                InputSystem.EnableDevice(Accelerometer.current);
            }
        }
    
        private void CheckForApplicationFocus(bool hasFocus)
        {
            if (_platform == RuntimePlatform.Android)
            {
                if (hasFocus)
                {
                    InputSystem.EnableDevice(Accelerometer.current);
                    return;
                }
                InputSystem.DisableDevice(Accelerometer.current);
            }
        }

    }
}